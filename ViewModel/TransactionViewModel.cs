// 収支画面（TransactionsView）用の ViewModel。
// View がバインドして表示・編集する「画面の状態」を保持し、
// 業務ロジックは UseCase に委ね、その結果を画面用の状態へ反映する。
//
// なぜここに計算や DB アクセスを書かないのか:
// ・集計や永続化まで抱えると ViewModel が肥大化し、UI 無しでテストしづらくなるため。
// ・ViewModel の責務は「View と UseCase の橋渡し」と「表示状態の保持」に絞り、
//   取得・集計は UseCase、永続化は Repository へ役割を分ける。
using LifeManager.Application.DTOs;
using LifeManager.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LifeManager.Commands;
using System.ComponentModel;
using System.ComponentModel;

namespace LifeManager.ViewModel
{

   
    // なぜ INotifyPropertyChanged を実装するのか:
    // WPF のバインディングは PropertyChanged 通知を監視して表示を更新する。
    // これが無いと ViewModel の値を変えても View に反映されず、
    // 「状態の変化を View へ知らせる」という ViewModel の役割が果たせないため。
    //
    // なぜ IDataErrorInfo を実装するのか:
    // 入力値の検証ルールを View ではなく ViewModel 側に集約するため。
    // View は ValidatesOnDataErrors=True と書くだけでよく、
    // ルールの追加・変更が XAML に波及せず、検証を UI 無しでテストできる。
    public class TransactionViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        // なぜ Error は常に null なのか:
        // オブジェクト全体としてのエラーは扱わず、検証はプロパティ単位で行う方針のため。
        // 実際の判定は下のインデクサ（プロパティ名で問い合わされる）に集約する。
        public string Error => null;

        // IDataErrorInfo のインデクサ。
        // なぜ switch でプロパティ名を分岐するのか:
        // WPF は検証時にバインド対象のプロパティ名で this["Income"] のように問い合わせるため、
        // ここで名前ごとに分岐すれば、プロパティ別の検証ルールを一箇所へまとめられる。
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Income):
                        if (Income < 0)
                        {
                            return "収入は0以上でなければなりません。";
                        }
                        break;
                    case nameof(Expense):
                        if (Expense < 0)
                        {
                            return "支出は0以上でなければなりません。";
                        }
                        break;
                }
                return null;
            }
        }






        // 収入。View の TextBox と双方向バインドされる表示状態。
        private Decimal _Income;

        public Decimal Income
        {
            get { return _Income; }
            set
            {
                // なぜ値が変わったときだけ通知するのか:
                // 同じ値で PropertyChanged を発火させると無駄な再描画が起き、
                // 双方向バインディングの相互更新で無限ループに陥る恐れもあるため。
                if (_Income != value)
                {
                    if (value < 0)
                    {
                        return; // 負値は状態として受け付けない（表示上の検証は IDataErrorInfo が担当）
                    }
                    _Income = value;
                    OnPropertyChanged(nameof(Income));
                    // なぜここで Total も通知するのか:
                    // Total は Income から導出される読み取り専用プロパティで自力では変化を検知できない。
                    // 元になった値が変わったこの場で通知しないと、画面の合計が更新されないため。
                    OnPropertyChanged(nameof(Total));
                }

            }
        }


        // 支出。Income と同じく View と双方向バインドされる表示状態。
        private Decimal _Expense;

        public Decimal Expense
        {
            get { return _Expense; }
            set
            {
                // Income と同じ理由: 変化時のみ通知して無駄な再描画とループを避ける。
                if (_Expense != value)
                {
                    if (value < 0)
                    {
                        return; // 負値は受け付けない（検証表示は IDataErrorInfo に委ねる）
                    }
                    _Expense = value;
                    OnPropertyChanged(nameof(Expense));
                    OnPropertyChanged(nameof(Total)); // 導出元が変わったので合計も通知
                }
            }
        }

        // 合計。
        // なぜフィールドに持たず計算プロパティ（=>）にするのか:
        // Total を別フィールドで持つと Income/Expense と値が食い違う「二重管理」を招く。
        // 常に導出すれば真実の源が一つに保たれ、矛盾が起きない。
         public Decimal Total => Income - Expense;





        // なぜメソッド公開ではなく ICommand なのか:
        // View は Command="{Binding LoadCommand}" とバインドするだけで操作を届けられ、
        // コードビハインドにクリックハンドラを書かずに済むため。
        public ICommand LoadCommand { get; }

        // データ取得は UseCase に委譲する。
        // なぜ ViewModel が UseCase を経由するのか:
        // 取得・集計を ViewModel が直接抱えると責務が肥大化し、
        // 画面と無関係なロジックのテストに UI が必要になってしまうため。
        private readonly GetDailySummaryUseCase _UseCase;

        // 取得対象として選択中の日付（View の DatePicker とバインドされる表示状態）。
        public DateTime SelectDate { get; set; } = DateTime.Today;




        // なぜ private set にするのか:
        // 一覧の差し替えを Load() 経由だけに限定するため。外部から自由に代入できると
        // 「いつ・どこで表示データが変わったのか」を追えなくなり、状態管理が崩れる。
        public IEnumerable<TransactionDto> Transactions { get; private set; } = new List<TransactionDto>();

        public TransactionViewModel()
        {
            _UseCase = new GetDailySummaryUseCase();
            LoadCommand = new RelayCommand(Load);

        }

        // なぜ ViewModel は Execute を呼ぶだけなのか:
        // 「どう取得するか」は UseCase の責務で、ViewModel は結果を表示状態へ写すだけにする。
        // この分担により、UseCase の中身がダミーデータから DB 取得へ変わっても
        // ViewModel は無変更で済み、変更の影響が層をまたがない。
        public void Load()
        {
            var result = _UseCase.Execute(SelectDate);

            Transactions = result.Transactions;

            //Total = result.Total;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // なぜ通知処理をこのメソッドに集約するのか:
        // 各 setter から直接 PropertyChanged を触ると null チェック（?.）が散らばり、
        // 通知の仕組みを変えるとき修正箇所が増える。窓口を一つにして重複と修正コストを抑える。
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


    }
}
