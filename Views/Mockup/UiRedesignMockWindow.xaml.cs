using System.Windows;

namespace LifeManager.Views.Mockup
{
    // UI リデザイン案のデザイン確認用モック Window。
    // なぜ実画面（MainWindow / TransactionsView / HistoryWindow）と分けるのか:
    // 配色・レイアウトの方向性を合意する前に本番の View を書き換えると差し戻しコストが高い。
    // 独立した Window にしておけば、実データや実フローに影響を与えずに見た目だけ確認できる。
    // Visual Studio のデザイナで開けばアプリを起動せずにプレビューできる（値はハードコード）。
    public partial class UiRedesignMockWindow : Window
    {
        public UiRedesignMockWindow()
        {
            InitializeComponent();
        }
    }
}
