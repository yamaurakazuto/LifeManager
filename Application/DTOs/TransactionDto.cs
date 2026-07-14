// 1 件の収支（取引）を表す DTO。UseCase から ViewModel へ渡し、View が表示する単位。
//
// なぜ Model（DailySummaries）をそのまま UI へ渡さず DTO を挟むのか:
// ・UI が求める形と DB の形は一致しないことが多く、Model を直接渡すと
//   「DB の都合」が画面まで伝染してしまうため。
// ・層の境界を DTO で区切っておけば、DB スキーマを変えても影響を
//   Application 層で食い止められ、ViewModel／View に波及させずに済むため。
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
        // float/double は値を 2 進数の近似で保持するため金額計算で誤差が出る。
        // 金銭は 10 進数で正確に表せる decimal を使い、集計時の誤差を防ぐ。
        public decimal Amount { get; set; }
    }
}
