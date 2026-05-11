// 収支画面ウィンドウのコードビハインドです。
// このファイルには TransactionsView.xaml の相互作用ロジックが含まれます。
// ビューの DataContext は NavigationService によって提供される
// `TrasactionViewModel` のインスタンスであることを想定しています。
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
    /// <summary>
    /// TransactionsView.xaml の相互作用ロジック
    /// </summary>
    public partial class TransactionsView : Window
    {
        /// <summary>
        /// TransactionsView ウィンドウの新しいインスタンスを初期化します。
        /// コンストラクタは InitializeComponent を呼び出して XAML を読み込みます。
        /// </summary>
        public TransactionsView()
        {
            InitializeComponent();
            DataContext = new TransactionViewModel();
        }

        private void NumberOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }

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
