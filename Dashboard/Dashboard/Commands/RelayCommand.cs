using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dashboard.Commands
{
    public class RelayCommand : ICommand
    {

        private readonly Action _execute;
        public RelayCommand(Action execute)
        {
            _execute = execute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true; // 常に実行可能
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }
}
