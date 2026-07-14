// アプリの起動口（エントリポイント）。
// なぜここで各サービスを組み立てるのか:
// MVVM では「View / ViewModel / Service の誰が誰に依存するか」を一箇所で決めたい。
// 起動直後のここを唯一の組み立て場所（Composition Root）にすることで、
// 依存関係の配線がアプリ内に散らばらず、DI 導入時もこの 1 か所の変更で済む。
using System.Configuration;
using System.Data;
using System.Windows;
using LifeManager.ViewModel;
using LifeManager.Services; 
using LifeManager.Views;    

namespace LifeManager
{
    /// <summary>
    /// App.xaml のコードビハインド。アプリ起動時の依存の組み立て（Composition Root）を担う。
    /// </summary>
    public partial class App : System.Windows.Application
    {
        // なぜ起動処理をここに集約するのか:
        // 依存の生成順（Service → ViewModel → View）をこの 1 メソッドで表現し、
        // 「View が ViewModel を new する」「ViewModel が Service を new する」といった
        // 依存の逆流を防ぐため。組み立ての全体像がこの場所だけで追えるようにする。
        //
        // 既知の重複: 現状は App.xaml の StartupUri が MainWindow を直接起動し、
        // MainWindow 側でも DataContext を設定しているため、ここで組んだ mainWindow は
        // Show されず未使用のまま。将来 StartupUri を外し、配線をこの OnStartup へ一本化する。
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // なぜ最初に Service を作るのか:
            // ViewModel は「画面を出す」手段を自前で持たず Service に委ねる設計なので、
            // 注入する相手（NavigationService）を先に用意しておく必要がある。
            var navigationService = new NavigationService();

            // なぜコンストラクタで注入するのか:
            // ViewModel は具体的な Window ではなく INavigationService という抽象にだけ依存させたい。
            // 外から実装を渡す形にすることで、View 層に触れずに ViewModel を単体テストできる。
            var mainViewModel = new ViewModel.MainViewModel(navigationService);

            // なぜ DataContext に ViewModel を入れるのか:
            // XAML の {Binding ...} はこの DataContext を起点に解決される。
            // これにより View は表示だけを担い、状態とロジックは ViewModel 側に置ける。
            var mainWindow = new MainWindow
            {
                DataContext = mainViewModel
            };


        }
    }

}
