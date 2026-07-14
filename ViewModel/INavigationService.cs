// ViewModel が画面遷移を依頼するための契約（インターフェイス）。
// 実装クラスが、依頼に応じて適切な View を生成・表示する責任を負う。
//
// なぜ抽象を挟むのか:
// ・ViewModel が Window を直接 new すると View 層への依存が生まれ、MVVM の層分離が崩れるため。
// ・「画面を出す」という UI 依存の処理を抽象の向こう側へ隔離すれば、
//   ViewModel を UI 無しで単体テストできる（この抽象をモックに差し替えられる）。
//
// なぜ実装側（Services）ではなく ViewModel 名前空間に置くのか:
// 依存の向きを「実装（Services）→ 抽象（ViewModel 側）」に保つため（依存性逆転）。
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
        /// 収支画面の表示を依頼する。
        /// 実装は対応する View を生成し、渡された ViewModel を DataContext に設定する。
        /// なぜ ViewModel を引数で受け取るのか:
        /// 表示する状態は呼び出し側（ViewModel）が用意し、Service は View への配線だけを担うため。
        /// </summary>
        /// <param name="vm">表示する収支画面の ViewModel。</param>
        void DisplayTransactions(TransactionViewModel vm);

        /// <summary>
        /// 収支履歴画面の表示を依頼する。
        /// 実装は対応する View を生成し、渡された ViewModel を DataContext に設定する。
        /// </summary>
        /// <param name="vm">表示する収支履歴画面の ViewModel。</param>
        void DisplayHistory(HistoryViewModel vm);
    }
}
