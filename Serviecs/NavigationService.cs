// INavigationService の実装です。ビューのインスタンスを生成し、
// 提供された ViewModel を DataContext に設定してウィンドウを表示する責任を持ちます。
using LifeManager.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeManager.ViewModel;

namespace LifeManager.Services  
{
    public class NavigationService : LifeManager.ViewModel.INavigationService
    {
        /// <summary>
        /// 収支ウィンドウを作成して表示します。引数の ViewModel を DataContext に設定し、
        /// ビューがデータへバインドできるようにします。
        /// </summary>
        /// <param name="vm">DataContext に設定する収支用の ViewModel。</param>
        public void DisplayTransactions(ViewModel.TransactionViewModel vm)
        {
            var view = new TransactionsView();

            view.DataContext = vm;
            view.Show();
        }

        public void DisplayHistory(ViewModel.HistoryViewModel vm)
        {
            var view = new HistoryWindow();
            view.DataContext = vm;
            view.Show();
        }
    }
}
