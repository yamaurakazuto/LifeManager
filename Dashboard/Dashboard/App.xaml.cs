// アプリケーションのエントリポイントおよび起動ロジック。アプリ起動時に
// メインウィンドウやナビゲーションなどのサービスを構成します。
using System.Configuration;
using System.Data;
using System.Windows;

namespace Dashboard
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // ビューモデルがナビゲーションを要求するために使用するナビゲーションサービスを初期化します。
            var navigationService = new Serviecs.NavigationService();

            // メインの ViewModel を作成し、ナビゲーションサービスを注入します。
            // これにより ViewModel は UI フレームワークに直接依存せずにナビゲーションを実行できます。
            var mainViewModel = new ViewModel.MainViewModel(navigationService);

            // メインウィンドウを作成し、DataContext に ViewModel を設定した後に表示します。
            var mainWindow = new MainWindow
            {
                DataContext = mainViewModel
            };

            mainWindow.Show();
        }
    }

}
