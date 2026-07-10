// 1件の収支（取引）を表す DTO です。
//
// なぜ Model（DailySummaries）をそのまま UI に渡さず DTO を挟むのか:
// ・UI が必要とする形とデータベースの形は一致しないことが多く、
//   直接渡すと「DB の都合」が画面まで伝染してしまうため
// ・層の境界を DTO で区切っておけば、DB スキーマを変更しても
//   影響を Application 層までで食い止められるため
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeManager.Application.DTOs
{
    public class TransactionDto
    {
        public DateTime Date { get; set; }

        // なぜ decimal か:
        // float/double は 2 進数の近似値で保持するため金額計算で誤差が出る。
        // 金銭を扱うプロパティは 10 進数で正確に表現できる decimal を使う。
        public decimal Amount { get; set; }
    }
}
