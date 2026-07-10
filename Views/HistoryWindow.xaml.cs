using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// HistoryWindow.xaml の相互作用ロジック
    /// </summary>
    // コードビハインドが InitializeComponent だけなのは意図的。
    // 表示するデータは NavigationService が DataContext に設定する
    // HistoryViewModel が持ち、View はバインディングで表示するだけにする。
    // ロジックをここに書かないことで、画面の動作を ViewModel 側でテストできる。
    public partial class HistoryWindow : Window
    {
        public HistoryWindow()
        {
            InitializeComponent();
        }
    }
}
