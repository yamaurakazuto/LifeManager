using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace LifeManager.Models
{
    class DailySummaries
    {
        public int ID { get; set; }

        public DateTime TargetDate { get; set; }

        public decimal Income { get; set; } 
        public decimal Expense { get; set; }  
        
        public decimal Total { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
