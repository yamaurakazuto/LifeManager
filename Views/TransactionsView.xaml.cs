// 収支画面のコードビハインド。
// 表示する状態は TransactionViewModel が持ち、View はバインディングで表示するだけに保つ。
// ここに置くのは「数字だけ入力させる」といった純粋な UI の振る舞いに限り、
// 業務ルール（収入は 0 以上 等）は ViewModel 側へ寄せることで層の責務を混ぜない。
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
using LifeManager.ViewModel;
using System.Text.RegularExpressions;


namespace LifeManager.Views
{
    public partial class TransactionsView : Window
    {
        /// <summary>
        /// TransactionsView を初期化する。XAML を読み込み、DataContext に
        /// TransactionViewModel を設定してバインディングの起点を用意する。
        /// </summary>
        public TransactionsView()
        {
            InitializeComponent();
            DataContext = new TransactionViewModel();
        }

        // なぜこの入力制限だけコードビハインドに置いてよいのか:
        // 「数字以外のキー入力を弾く」のは業務ルールではなく純粋な UI の振る舞いのため。
        // 「収入は 0 以上」のような業務ルールは ViewModel（IDataErrorInfo）側に置く。
        // PreviewTextInput を使うのは、値がバインディングへ渡る前のキー入力段階で
        // 不正文字を止められるから。
        private void NumberOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }

        // なぜ貼り付けを別に塞ぐのか:
        // PreviewTextInput は貼り付け（Ctrl+V）経由の入力を通してしまうため、
        // 貼り付け経路は Pasting イベントで別途チェックしないと数字以外が混入するから。
        private void NumberOnly_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                var text = (string)e.DataObject.GetData(typeof(string));
                if (!Regex.IsMatch(text, "^[0-9]+$"))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {

        }
    }
}
