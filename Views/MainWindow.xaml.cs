// メインウィンドウのコードビハインドです。
// このファイルには `MainWindow` の最小限の相互作用ロジックが含まれ、
// 動作は `MainViewModel` へのデータバインディングに依存します。
// 必要に応じて UI イベントハンドラはここに配置します。
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
        /// MainWindow の新しいインスタンスを初期化し、XAML を読み込みます。
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // View が ViewModel の「型」を知って生成するのはここだけに限定する。
            // XAML の {Binding ...} はこの DataContext を参照して動くため、
            // これ以降 View と ViewModel はプロパティ名だけで疎結合につながる。
            var navigationService = new NavigationService();

            DataContext = new MainViewModel(navigationService);

            // ここからの DB 接続は「SQL Server につながるか」を確認するための一時コード。
            // 本来 DB アクセスは Repository 層の責務であり、View に置くと
            // MVVM の層分離が崩れるため、Repository 実装後にそちらへ移動する予定。
            var connectionString = "Server=localhost;Database=LifeManager;Trusted_Connection=True;";

            // using を付ける理由: DB 接続は OS リソースを掴むため、
            // スコープを抜けたら確実に Dispose（切断）させる必要がある。
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            MessageBox.Show("データベースに接続しました。");
        }


    }
}