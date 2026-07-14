// メインウィンドウのコードビハインド。
// なぜここを薄く保つのか:
// 画面の振る舞いは MainViewModel へのバインディングに委ね、コードビハインドには
// ロジックを持たせない。こうすることで動作を ViewModel 側でテストでき、
// View は表示だけに専念できる（MVVM の基本方針）。
using LifeManager.Services;
using LifeManager.ViewModel;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LifeManager.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// MainWindow を初期化する。XAML を読み込み、バインドの起点となる
        /// DataContext（MainViewModel）をここで一度だけ設定する。
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // なぜ View が ViewModel を生成するのがここだけなのか:
            // XAML の {Binding ...} はこの DataContext を起点に解決される。
            // 生成をコンストラクタの 1 か所に限定すれば、以降 View と ViewModel は
            // プロパティ名だけで結び付き、互いの実装を知らない疎結合を保てる。
            var navigationService = new NavigationService();

            DataContext = new MainViewModel(navigationService);

            // 【暫定コード】DB 接続の疎通確認。
            // 本来 DB アクセスは Repository 層の責務で、View に置くと MVVM の層分離が崩れる。
            // Repository 実装後にこの処理はそちらへ移し、View から取り除く予定。
            var connectionString = "Server=localhost;Database=LifeManager;Trusted_Connection=True;";

            // なぜ using を付けるのか:
            // DB 接続は OS リソースを掴むため、スコープを抜けたら確実に Dispose（切断）して
            // 接続の解放漏れを防ぐ必要がある。
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            MessageBox.Show("データベースに接続しました。");
        }


    }
}