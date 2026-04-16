using Dashboard.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Serviecs
{
    internal class NavigationService : ViewModel.INavigationService
    {
        public void DisplayTransactions(ViewModel.TrasactionViewModel vm)
        {
            // Implementation for displaying transactions
             var view = new TransactionsView(); // Assuming you have a TransactionView defined

            view.DataContext = vm; // Set the DataContext to the provided ViewModel
            view.Show(); // Display the view
        }
    }
}
