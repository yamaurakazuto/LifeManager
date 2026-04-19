using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.ViewModel
{
    public interface INavigationService
    {
        void DisplayTransactions(TrasactionViewModel vm);
    }
}
