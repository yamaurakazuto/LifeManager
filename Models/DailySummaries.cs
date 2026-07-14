using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace LifeManager.Models
{
    // DB の DailySummaries テーブル 1 行に対応するドメインモデル。
    // ViewModel が扱う表示用の DTO とは別に、永続化される「日次の実データ」を表す。
    //
    // なぜ「日次スナップショット」をテーブルで持つのか:
    // 現在残高を 1 つ持つだけでは「先月末時点でいくらだったか」を再現できない。
    // 日ごとの状態を行として残すことで、履歴追跡・月次集計・収支分析を後からでも行える。
    public class DailySummaries
    {
        public int ID { get; set; }

        // どの日のスナップショットかを示すキー。
        // なぜ登録日時と別に「対象日」を持つのか: 過去日の入力・修正を許すため。
        public DateTime TargetDate { get; set; }

        // なぜ Income と Expense を相殺せず別々に保持するのか:
        // Total だけ保存すると「収入 10 万・支出 10 万の日」と「収支ゼロの日」を
        // 区別できず、内訳に基づく収支分析ができなくなるため。
        public decimal Income { get; set; }
        public decimal Expense { get; set; }

        public decimal Total { get; set; }

        // 同じ対象日のデータを上書きしたときの追跡用。
        // その数字が「いつ時点のものか」を後から確認できるようにする。
        public DateTime UpdateDate { get; set; }


        
           
        
    }
}
