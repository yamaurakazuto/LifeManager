using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Dashboard.Commands;

namespace Dashboard.ViewModel
{
    public class MainViewModel
    {
        private readonly INavigationService _navigationService;
        public ICommand NavigateToTransactionsCommand { get; }

        public MainViewModel(INavigationService navigationService)
        {
            NavigateToTransactionsCommand = new RelayCommand(OpenTransactions);
            _navigationService = navigationService;
        }

        private void OpenTransactions()
        {
            var transactionsViewModel = new TrasactionViewModel();
            _navigationService.DisplayTransactions(transactionsViewModel);
        }
    }
}
