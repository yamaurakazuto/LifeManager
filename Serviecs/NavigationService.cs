// INavigationService の実装です。ビューのインスタンスを生成し、
// 提供された ViewModel を DataContext に設定してウィンドウを表示する責任を持ちます。
//
// なぜこのクラスだけが View（Window）を new してよいのか:
// 「どの Window クラスを使うか」という View の知識をここに閉じ込めることで、
// ViewModel 側は INavigationService という抽象しか知らずに済む。
// View への依存をアプリ全体に散らばらせないための「境界役」がこのクラス。
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

            // DataContext に ViewModel を設定するのは、XAML 側の {Binding ...} が
            // このオブジェクトのプロパティを参照して表示を組み立てるため。
            // View と ViewModel はこの 1 行だけで結び付き、互いのコードは知らないままでいられる。
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
