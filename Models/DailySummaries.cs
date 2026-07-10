using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace LifeManager.Models
{
    // DB の DailySummaries テーブル 1 行に対応するモデルクラスです。
    //
    // なぜ「日次スナップショット」をテーブルとして持つのか:
    // 現在残高だけを 1 つ持つ設計だと「先月末時点でいくらだったか」が
    // 再現できない。日ごとの累積状態を行として残すことで、
    // 履歴追跡・月次集計・収支分析が後からでも可能になる。
    public class DailySummaries
    {
        public int ID { get; set; }

        // どの日のスナップショットかを表すキー。
        // 登録日時ではなく「対象日」を別に持つのは、過去日の入力・修正を許すため。
        public DateTime TargetDate { get; set; }

        // Income と Expense を相殺せず別々に保持する理由:
        // Total だけ保存すると「収入 10 万・支出 10 万」と「収支ゼロの日」が
        // 区別できなくなり、収支分析ができなくなるため。
        public decimal Income { get; set; }
        public decimal Expense { get; set; }

        public decimal Total { get; set; }

        // 同じ日のデータを上書き更新したときの追跡用。
        // いつ時点の数字なのかを後から確認できるようにする。
        public DateTime UpdateDate { get; set; }


        
           
        
    }
}
