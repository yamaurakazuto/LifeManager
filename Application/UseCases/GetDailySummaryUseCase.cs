// 「指定した日付の収支サマリーを取得する」というユースケースを表すクラスです。
//
// なぜ UseCase という単位で切り出すのか:
// ・「日次サマリーの取得」という業務上の操作を 1 クラス 1 責務で表現するため
// ・取得と集計のロジックを ViewModel から追い出すことで、
//   UI なしでビジネスロジックをテストできるようにするため
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
            // 現段階ではダミーデータを返している。
            // 先に「UseCase が DTO を返す」という層の形を固めておけば、
            // 後で Repository 経由の DB 取得に差し替えても呼び出し側（ViewModel）は変更不要。
            var trancactions = new List<TransactionDto>
            {
                new TransactionDto { Date = date, Amount = 100},
                new TransactionDto { Date = date, Amount = -50},
            };

            return new DailySummaryDto
            {
                Transactions = trancactions,

                // 合計はここで計算して DTO に詰める。
                // ViewModel に計算させると「表示」と「集計ルール」が混ざり、
                // 画面ごとに集計ロジックが重複していく原因になるため。
                Total = trancactions.Sum(x => x.Amount)
            };


        }
    }
}
