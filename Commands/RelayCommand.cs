// ICommand の簡易実装です。ビュー（ボタンのクリックなど）からの
// アクションを ViewModel のメソッドに転送するために使用します。
// MVVM パターンで XAML にバインドするコマンドとしてよく利用されます。
//
// なぜこのクラスが必要なのか:
// WPF の Button.Command は ICommand しか受け取れないが、コマンドごとに
// 専用クラスを作るのは冗長。Action（メソッド参照）を包んで ICommand に
// 変換する汎用クラスを 1 つ用意すれば、ViewModel は
// new RelayCommand(メソッド) と書くだけで済む。
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LifeManager.Commands
{
    public class RelayCommand : ICommand
    {

        // readonly にする理由: コマンドの中身が生成後にすり替わることを防ぎ、
        // 「このコマンド = このメソッド」という対応を不変にするため。
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
