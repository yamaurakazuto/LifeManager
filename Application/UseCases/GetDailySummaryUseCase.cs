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
            var trancactions = new List<TransactionDto>
            {
                new TransactionDto { Date = date, Amount = 100},
                new TransactionDto { Date = date, Amount = -50},
            };

            return new DailySummaryDto
            {
                Transactions = trancactions,

                Total = trancactions.Sum(x => x.Amount)
            };


        }
    }
}
