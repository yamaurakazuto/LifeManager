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
    // なぜコードビハインドが InitializeComponent だけなのか（意図的）:
    // 表示データは NavigationService が DataContext に差す HistoryViewModel が持ち、
    // View はバインディングで表示するだけにする。ロジックをここへ書かないことで、
    // 画面の動作を UI 抜きに ViewModel 側でテストできる（MVVM の理想形）。
    public partial class HistoryWindow : Window
    {
        public HistoryWindow()
        {
            InitializeComponent();
        }
    }
}
