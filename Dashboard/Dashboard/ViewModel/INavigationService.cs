using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.ViewModel
{
    internal interface INavigationService
    {
        void DisplayTransactions(TrasactionViewModel vm);
    }
}
