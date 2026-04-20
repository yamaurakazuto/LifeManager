// INavigationService の実装です。ビューのインスタンスを生成し、
// 提供された ViewModel を DataContext に設定してウィンドウを表示する責任を持ちます。
using Dashboard.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Serviecs
{
    internal class NavigationService : ViewModel.INavigationService
    {
        /// <summary>
        /// 収支ウィンドウを作成して表示します。引数の ViewModel を DataContext に設定し、
        /// ビューがデータへバインドできるようにします。
        /// </summary>
        /// <param name="vm">DataContext に設定する収支用の ViewModel。</param>
        public void DisplayTransactions(ViewModel.TrasactionViewModel vm)
        {
            var view = new TransactionsView();

            view.DataContext = vm;
            view.Show();
        }
    }
}
