// このクラスは「1日分の収支情報」をまとめて表現するDTOです。
// UIが必要とする「一覧 + 合計」といった複数の情報を1つのまとまりとして扱うためのクラスです。

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeManager.Application.DTOs
{
    public class DailySummaryDto
    {
        /// <summary>
        // 取引一覧（その日付に属するすべての収支データ）
        // なぜIEnumerableか:
        // ・「一覧」であることを表現するため
        // ・Listなどの具体実装に依存しないため（柔軟性を保つ）
        // なぜTransactionDtoか:
        // ・1件の収支データを表すDTOを複数まとめるため
        // ・UIにそのままバインドできる形にするため
        /// </summary>  

        public IEnumerable<TransactionDto> Transactions { get; set; }


        /// <summary> 
        // 合計金額 
        // なぜここにあるか: 
        //・UIでそのまま表示するため 
        // ・計算結果をViewModel側で再計算させないため 
        // 計算はどこでやるか: 
        //・UseCaseで計算して、このプロパティにセットする 
        ///  </summary>
        public decimal Totalt { get; set; }
    }
}
