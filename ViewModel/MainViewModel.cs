// アプリケーションのメイン ViewModel です。このクラスはメインウィンドウ用の
// コマンドや状態を公開し、提供された INavigationService を呼び出して
// ナビゲーションを調整します。
//
// なぜ ViewModel が画面遷移を担当するのか:
// ・「どのボタンでどの画面に進むか」はアプリの振る舞い（ロジック）であり、
//   コードビハインドに書くとテストできなくなるため
// ・ただし Window を直接 new して Show() すると ViewModel が View に依存してしまうので、
//   INavigationService という抽象に「表示して」と依頼するだけに留める
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
        // インターフェイス型で保持する理由:
        // 実装（NavigationService）に直接依存すると View 層への参照が発生し、
        // MVVM の「ViewModel は View を知らない」という原則が崩れるため。
        private readonly INavigationService _navigationService;

        // なぜクリックイベントではなく ICommand なのか:
        // XAML の Command="{Binding ...}" でバインドすれば、
        // コードビハインドを経由せずにボタン操作を ViewModel に届けられるため。
        public ICommand NavigateToTransactionsCommand { get; }

        public ICommand NavigateToHistoryCommand { get; }

        /// <summary>
        /// MainViewModel の新しいインスタンスを作成し、ナビゲーションを設定します。
        /// なぜコンストラクタで受け取るのか（コンストラクタインジェクション）:
        /// 依存が外から見えるようになり、テスト時にモックへ差し替えられるため。
        /// </summary>
        /// <param name="navigationService">ナビゲーション操作を実行するサービス。</param>
        public MainViewModel(INavigationService navigationService)
        {
            NavigateToTransactionsCommand = new RelayCommand(OpenTransactions);
            _navigationService = navigationService;

            NavigateToHistoryCommand = new RelayCommand(OpenHistory);
        }

        /// <summary>
        /// コマンド実行時の処理。収支用の ViewModel を生成し、
        /// Processing during command execution. Generate a ViewModel for the balance,
        /// ナビゲーションサービスに対して収支画面の表示を要求します。
        /// Request the display of the balance screen to the navigation service.
        /// </summary>
        private void OpenTransactions()
        {
            var transactionsViewModel = new TransactionViewModel();
            _navigationService.DisplayTransactions(transactionsViewModel);
        }

        // なぜ ViewModel をここで生成して渡すのか:
        // 「画面に表示するデータ（ViewModel）を用意する」のは呼び出し側の責務で、
        // NavigationService は「渡されたものを表示する」ことだけに専念させたいため。
        private void OpenHistory()
        {
            var historyViewModel = new HistoryViewModel();
            _navigationService.DisplayHistory(historyViewModel);
        }
    }
}
