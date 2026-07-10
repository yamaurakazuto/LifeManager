using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using LifeManager.Models;   

namespace LifeManager.Repositories
{
    // DailySummaries テーブルへのアクセスを担当する Repository（実装予定）。
    //
    // なぜ Repository 層を設けるのか:
    // ・SQL や接続文字列といった「DB の都合」をこのクラスに閉じ込め、
    //   UseCase や ViewModel に SqlConnection を持ち込ませないため
    // ・データの保存先が変わっても（別 DB・Web API など）、
    //   修正範囲をこの層だけに限定するため
    class DailySummaryRepository
    {
    }
}
