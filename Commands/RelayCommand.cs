// ICommand の簡易実装です。ビュー（ボタンのクリックなど）からの
// アクションを ViewModel のメソッドに転送するために使用します。
// MVVM パターンで XAML にバインドするコマンドとしてよく利用されます。
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
            // この簡易実装では常に実行可能とします。より高度な実装では
            // 実行可否を判定する述語を受け取り、その変化時に CanExecuteChanged
            // を発火させることができます。
            return true;
        }

        public void Execute(object parameter)
        {
            // コマンド実行時に渡されたアクションを呼び出します。
            _execute();
        }
    }
}
