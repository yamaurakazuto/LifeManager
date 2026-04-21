// このファイルは、ビューがナビゲーションを要求するために使用する
// ナビゲーションの契約（インターフェイス）を定義します。
// 実装クラスは、要求に応じて適切なビューを作成・表示する責任を持ちます。
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.ViewModel
{
    public interface INavigationService
    {
        /// <summary>
        /// 収支画面を表示するよう要求します。
        /// 実装は収支ウィンドウを作成またはアクティブにし、
        /// 引数の <see cref="TrasactionViewModel"/> を DataContext に設定してください。
        /// </summary>
        /// <param name="vm">表示する収支画面の ViewModel。</param>
        void DisplayTransactions(TrasactionViewModel vm);

        /// <summary>
        /// 収支履歴画面を表示するよう要求します。
        /// 実装は収支履歴ウィンドウを作成またはアクティブにし、
        /// 引数の <see cref="RirekiViewModel"/> を DataContext に設定してください。
        /// </summary>
        /// <param name="vm">表示する収支履歴画面の ViewModel。</param>
        void DisplayRireki(RirekiViewModel vm);
    }
}
