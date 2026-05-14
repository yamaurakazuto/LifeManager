using LifeManager.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace LifeManager.ViewModel
{
    public class HistoryViewModel
    {
        public ObservableCollection<TransactionDto> transactions { get; }
        public HistoryViewModel()
        {
            transactions = new ObservableCollection<TransactionDto>();
        }
    }
}
