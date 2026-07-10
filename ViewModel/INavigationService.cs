// このファイルは、ViewModel がナビゲーションを要求するために使用する
// ナビゲーションの契約（インターフェイス）を定義します。
// 実装クラスは、要求に応じて適切なビューを作成・表示する責任を持ちます。
//
// なぜインターフェイスを挟むのか:
// ・ViewModel が Window を直接 new すると View 層への依存が生まれ、
//   MVVM の層分離が崩れてしまうため
// ・「画面を出す」という UI 依存の処理を抽象の向こう側に隔離することで、
//   ViewModel を UI なしで単体テストできるようにするため
//
// なぜ ViewModel 名前空間に置くのか:
// 依存の向きを「実装（Services）→ 抽象（ViewModel 側）」にするため。
// 抽象を利用側に置くことで、ViewModel は Services 層を一切参照せずに済む。
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeManager.ViewModel
{
    public interface INavigationService
    {
        /// <summary>
        /// 収支画面を表示するよう要求します。
        /// 実装は収支ウィンドウを作成またはアクティブにし、
        /// 引数の <see cref="TrasactionViewModel"/> を DataContext に設定してください。
        /// </summary>
        /// <param name="vm">表示する収支画面の ViewModel。</param>
        void DisplayTransactions(TransactionViewModel vm);

        /// <summary>
        /// 収支履歴画面を表示するよう要求します。
        /// 実装は収支履歴ウィンドウを作成またはアクティブにし、
        /// 引数の <see cref="RirekiViewModel"/> を DataContext に設定してください。
        /// </summary>
        /// <param name="vm">表示する収支履歴画面の ViewModel。</param>
        void DisplayHistory(HistoryViewModel vm);
    }
}
