// アプリケーションのエントリポイントおよび起動ロジック。アプリ起動時に
// メインウィンドウやナビゲーションなどのサービスを構成します。
using System.Configuration;
using System.Data;
using System.Windows;
using LifeManager.ViewModel;
using LifeManager.Services; 
using LifeManager.Views;    

namespace LifeManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        // なぜ OnStartup で組み立てるのか:
        // アプリの起動地点は「誰が誰に依存するか」を決める組み立て場所
        // （Composition Root）としてふさわしく、依存の生成をここに
        // 集めておくと将来 DI コンテナ導入時もこの 1 か所の変更で済むため。
        //
        // 注意: 現在は App.xaml の StartupUri が MainWindow を起動し、
        // MainWindow 側のコンストラクタでも DataContext を設定しているため、
        // ここで組み立てた mainWindow は実際には使われていない（Show していない）。
        // 将来的には StartupUri を外し、この OnStartup に一本化する予定。
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // ビューモデルがナビゲーションを要求するために使用するナビゲーションサービスを初期化します。
            var navigationService = new NavigationService();

            // メインの ViewModel を作成し、ナビゲーションサービスを注入します。
            // これにより ViewModel は UI フレームワークに直接依存せずにナビゲーションを実行できます。
            var mainViewModel = new ViewModel.MainViewModel(navigationService);

            // メインウィンドウを作成し、DataContext に ViewModel を設定した後に表示します。
            var mainWindow = new MainWindow
            {
                DataContext = mainViewModel
            };


        }
    }

}
