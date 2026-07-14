// INavigationService の実装。View（Window）を生成し、渡された ViewModel を
// DataContext に設定して表示することが責務。
//
// なぜこのクラスだけが View を new してよいのか:
// 「どの Window を使うか」という View の知識をここへ閉じ込めれば、
// ViewModel 側は INavigationService という抽象しか知らずに済む。
// View への依存をアプリ全体へ散らばらせない「境界役」がこのクラス。
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
        /// 収支ウィンドウを生成して表示する。渡された ViewModel を DataContext に設定し、
        /// View がその状態へバインドできるようにする。
        /// </summary>
        /// <param name="vm">DataContext に設定する収支用の ViewModel。</param>
        public void DisplayTransactions(ViewModel.TransactionViewModel vm)
        {
            var view = new TransactionsView();

            // なぜ DataContext に ViewModel を差すのか:
            // XAML の {Binding ...} はこのオブジェクトのプロパティを起点に表示を組み立てる。
            // View と ViewModel はこの 1 行だけで結び付き、互いの実装は知らないまま疎結合を保てる。
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
