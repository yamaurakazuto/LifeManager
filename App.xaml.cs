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
        // 【デザインモック確認用の暫定措置】
        // App.xaml の StartupUri を Views/Mockup/UiRedesignMockWindow.xaml へ向けており、
        // デバッグ実行（F5）でリデザイン案のモック画面がそのまま立ち上がる。
        // 本番フローに戻すときは StartupUri を Views/MainWindow.xaml に戻し、
        // 下記の組み立て処理のコメントアウトを解除すること。
        //
        // なお下の MainWindow 生成は元々 Show されない未使用コードだが、
        // MainWindow のコンストラクタが DB 接続を試みるため、モック確認時に
        // SQL Server が無いと例外になる。確認の妨げになるので一時的に無効化する。
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // var navigationService = new NavigationService();
            // var mainViewModel = new ViewModel.MainViewModel(navigationService);
            // var mainWindow = new MainWindow
            // {
            //     DataContext = mainViewModel
            // };
        }
    }

}
