// このクラスは「収支画面（TransactionsView）」のためのViewModelです。
// UIが表示・操作するための状態（データ）を保持し、
// UseCaseから受け取った結果を画面に反映する役割を持ちます。
//
// 重要なポイント:
// ・ビジネスロジック（計算やDB操作）はここに書かない
// ・状態を持つことに専念する（表示のためのデータ）
// ・UIとUseCaseの橋渡し役

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
    // WPF のバインディングは PropertyChanged イベントを監視して画面を再描画する。
    // これがないと、ViewModel の値を変えても画面に反映されない。
    //
    // なぜ IDataErrorInfo を実装するのか:
    // 入力エラーの「判定ルール」を ViewModel 側に置くため。
    // View 側は ValidatesOnDataErrors=True と書くだけでよく、
    // ルールの追加・変更が XAML に波及しない。
    public class TransactionViewModel : INotifyPropertyChanged, IDataErrorInfo
    {

        /// <summary>
        /// プロパティ
        /// </summary>
        ///

        // オブジェクト全体としてのエラーは扱わないため null を返す。
        // （プロパティ単位の検証はインデクサ側で行う）
        public string Error => null;

        // IDataErrorInfo のインデクサ。
        // なぜ switch でプロパティ名を分岐するのか:
        // WPF はバインド対象のプロパティ名を使って this["Income"] のように
        // 問い合わせてくるため、プロパティごとの検証ルールをここに集約できる。
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






        // 収入
        private Decimal _Income;

        public Decimal Income
        {
            get { return _Income; }
            set
            {
                // なぜ値が変わったときだけ処理するのか:
                // 同じ値で PropertyChanged を発火させると無駄な再描画が起き、
                // バインディングの相互更新でループする可能性もあるため。
                if (_Income != value)
                {
                    if (value < 0)
                    {
                        return; // 0未満の値は無視する（エラーを表示するなどの処理も考えられる）
                    }
                    _Income = value;
                    OnPropertyChanged(nameof(Income));
                    // Total は Income から算出される読み取り専用プロパティなので、
                    // 自分では変更を検知できない。元の値が変わったここで通知してやる必要がある。
                    OnPropertyChanged(nameof(Total)); // Totalも更新
                }

            }
        }


        // 支出
        private Decimal _Expense;

        public Decimal Expense
        {
            get { return _Expense; }
            set
            {
                if (_Expense != value)
                {
                    if (value < 0)
                    {
                        return; // 0未満の値は無視する（エラーを表示するなどの処理も考えられる）
                    }
                    _Expense = value;
                    OnPropertyChanged(nameof(Expense));
                    OnPropertyChanged(nameof(Total)); // Totalも更新
                }
            }
        }

        //合計
        // なぜ計算プロパティ（=>）にするのか:
        // Total を独立したフィールドに持つと Income/Expense と食い違う
        // 「二重管理」のリスクが生まれる。常に導出すれば矛盾が起きない。
         public Decimal Total => Income - Expense;





        // なぜメソッド呼び出しではなく ICommand で公開するのか:
        // View からは Command="{Binding LoadCommand}" とバインドするだけで済み、
        // コードビハインドにクリックハンドラを書かずに済むため。
        public ICommand LoadCommand { get; }

        // データ取得ロジックは UseCase に委譲する。
        // ViewModel 自身が計算や DB アクセスを持つと責務が肥大化し、
        // 画面と無関係なロジックのテストに UI が必要になってしまうため。
        private readonly GetDailySummaryUseCase _UseCase;

        // 現在選択されている日付        
        public DateTime SelectDate { get; set; } = DateTime.Today;

        


        //外から変更できないようにprivate setにする
        // なぜ private set か:
        // 一覧の差し替えは Load() 経由に限定したい。外部から自由に代入できると
        // 「いつ・どこでデータが変わったのか」を追えなくなるため。
        public IEnumerable<TransactionDto> Transactions { get; private set; } = new List<TransactionDto>();

        public TransactionViewModel()
        {
            _UseCase = new GetDailySummaryUseCase();
            LoadCommand = new RelayCommand(Load);

        }

        // 「取得の仕方」は UseCase が知っていればよく、ViewModel は
        // 結果を画面用の状態に反映するだけ。この分担により、
        // 将来 UseCase の中身がダミーデータから DB 取得に変わっても ViewModel は無変更で済む。
        public void Load()
        {
            var result = _UseCase.Execute(SelectDate);

            Transactions = result.Transactions;

            //Total = result.Total;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // 各プロパティの setter から直接 PropertyChanged を触らせず
        // このメソッドに集約する。null チェック（?.）を毎回書かずに済み、
        // 通知の仕組みを変えるときも修正箇所が 1 か所で済むため。
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


    }
}
