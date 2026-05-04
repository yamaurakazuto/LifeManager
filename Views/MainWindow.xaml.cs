// メインウィンドウのコードビハインドです。
// このファイルには `MainWindow` の最小限の相互作用ロジックが含まれ、
// 動作は `MainViewModel` へのデータバインディングに依存します。
// 必要に応じて UI イベントハンドラはここに配置します。
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dashboard
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
        }

       
    }
}