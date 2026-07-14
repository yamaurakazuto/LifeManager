// 「指定日の収支サマリーを取得する」という業務操作を表す UseCase。
//
// なぜ UseCase という単位で切り出すのか:
// ・業務上の 1 操作を「1 クラス 1 責務」で表し、呼び出し側（ViewModel）から
//   手順の詳細を隠すため。
// ・取得と集計のロジックを ViewModel の外へ出すことで、UI 無しで業務ロジックを
//   テストでき、画面が変わってもロジックを再利用できるようにするため。
using LifeManager.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeManager.Application.UseCases
{
    public class GetDailySummaryUseCase
    {
        public DailySummaryDto Execute(DateTime date)
        {
            // なぜ今はダミーデータなのか:
            // 先に「UseCase が DTO を返す」という層の境界を固めておけば、
            // 後で中身を Repository 経由の DB 取得へ差し替えても、
            // 戻り値の形（DTO）が同じなら呼び出し側（ViewModel）は無変更で済むため。
            var trancactions = new List<TransactionDto>
            {
                new TransactionDto { Date = date, Amount = 100},
                new TransactionDto { Date = date, Amount = -50},
            };

            return new DailySummaryDto
            {
                Transactions = trancactions,

                // なぜ合計を UseCase 側で計算して DTO に詰めるのか:
                // 集計を ViewModel にやらせると「表示」と「集計ルール」が混ざり、
                // 画面が増えるたびに同じ集計ロジックが重複していくため。
                // 集計ルールは UseCase に一本化し、ViewModel は結果を受け取るだけにする。
                Total = trancactions.Sum(x => x.Amount)
            };


        }
    }
}
