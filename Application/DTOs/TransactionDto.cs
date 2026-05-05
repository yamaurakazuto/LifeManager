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

        public decimal Amount { get; set; }
    }
}
