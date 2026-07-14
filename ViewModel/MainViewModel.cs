// メインウィンドウ用の ViewModel。
// 各画面へ遷移するためのコマンドを公開し、遷移そのものは INavigationService に委ねる。
//
// なぜ画面遷移を ViewModel が担当するのか:
// ・「どのボタンでどの画面へ進むか」はアプリの振る舞いであり、
//   コードビハインドに書くと UI 起動なしでテストできなくなるため。
// ・ただし Window を直接 new して Show() すると ViewModel が View に依存してしまう。
//   そこで INavigationService という抽象へ「表示して」と依頼するに留め、
//   ViewModel は View の具体型を知らないまま遷移を指示できるようにする。
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LifeManager.Commands;

namespace LifeManager.ViewModel
{
    public class MainViewModel
    {
        // なぜ実装クラスではなくインターフェイス型で保持するのか:
        // 実装（NavigationService）を直接持つと View 層への参照が発生し、
        // 「ViewModel は View を知らない」という MVVM の原則が崩れるため。
        // 抽象にだけ依存させ、実体は起動時に外から注入する。
        private readonly INavigationService _navigationService;

        // なぜ Click イベントではなく ICommand で公開するのか:
        // XAML の Command="{Binding ...}" でバインドすれば、コードビハインドを介さずに
        // ボタン操作を ViewModel へ届けられ、操作の処理を UI 抜きでテストできるため。
        public ICommand NavigateToTransactionsCommand { get; }

        public ICommand NavigateToHistoryCommand { get; }

        /// <summary>
        /// MainViewModel を生成し、遷移コマンドと依存サービスを組み立てる。
        /// なぜ依存をコンストラクタで受け取るのか（コンストラクタインジェクション）:
        /// この ViewModel が何に依存しているかが型として外から見え、
        /// テスト時には INavigationService のモックへ差し替えられるため。
        /// </summary>
        /// <param name="navigationService">画面遷移を実行するサービス。</param>
        public MainViewModel(INavigationService navigationService)
        {
            NavigateToTransactionsCommand = new RelayCommand(OpenTransactions);
            _navigationService = navigationService;

            NavigateToHistoryCommand = new RelayCommand(OpenHistory);
        }

        /// <summary>
        /// 収支画面への遷移コマンドの実体。
        /// 表示する ViewModel を生成し、その表示を Service に依頼するだけに留める。
        /// なぜ生成を呼び出し側で行うのか:
        /// 「画面に載せる状態（ViewModel）を用意する」のは遷移を指示する側の責務であり、
        /// Service は「渡されたものを表示する」ことだけに専念させ、責務を混ぜないため。
        /// </summary>
        private void OpenTransactions()
        {
            var transactionsViewModel = new TransactionViewModel();
            _navigationService.DisplayTransactions(transactionsViewModel);
        }

        // 履歴画面への遷移コマンドの実体。OpenTransactions と同じ役割分担で、
        // ViewModel をここで用意し、表示は Service へ委ねる。
        private void OpenHistory()
        {
            var historyViewModel = new HistoryViewModel();
            _navigationService.DisplayHistory(historyViewModel);
        }
    }
}
