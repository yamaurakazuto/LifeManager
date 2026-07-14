// 「1 日分の収支情報」をまとめて表す DTO。
// なぜ一覧と合計を 1 つにまとめるのか:
// 画面が必要とする「取引一覧 + 合計」を UseCase の 1 回の戻り値で渡せるようにし、
// ViewModel が複数の値を別々に取得・整合させる手間を無くすため。
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeManager.Application.DTOs
{
    public class DailySummaryDto
    {
        // 取引一覧（その日付に属するすべての収支データ）。
        // なぜ List ではなく IEnumerable か:
        // 「反復できる一覧」という意味だけを公開し、具体実装（List など）に縛られないため。
        // なぜ TransactionDto の集まりか:
        // 1 件分の DTO を並べることで、ViewModel／View がそのままバインドできる形になるため。
        public IEnumerable<TransactionDto> Transactions { get; set; }


        // 合計金額。
        // なぜ計算済みの値を DTO に持たせるのか:
        // View がそのまま表示でき、ViewModel 側で再計算させずに済むため。
        // 計算の責務は UseCase にあり、UseCase がこのプロパティへ結果を詰める。
        public decimal Total { get; set; }
    }
}
