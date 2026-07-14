// ICommand の汎用実装。View のボタン操作を ViewModel のメソッドへ橋渡しする。
//
// なぜこのクラスが必要なのか:
// WPF の Button.Command は ICommand しか受け取れないが、コマンドごとに専用クラスを
// 作るのは冗長。Action（メソッド参照）を ICommand に包む汎用クラスを一つ用意すれば、
// ViewModel は new RelayCommand(メソッド) と書くだけで操作を公開でき、
// コードビハインドにイベントハンドラを置かずに済む（MVVM の要となる部品）。
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

        // なぜ readonly か:
        // 生成後に実行対象がすり替わるのを防ぎ、「このコマンド = このメソッド」という
        // 対応を不変に保つため。バインド先が途中で変わる予期せぬ挙動を避ける。
        private readonly Action _execute;
        public RelayCommand(Action execute)
        {
            _execute = execute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            // この実装では常に実行可能として扱う。
            // 実行可否を切り替えたい場合は、判定用の述語を受け取り、
            // その結果が変わったときに CanExecuteChanged を発火させて View に再評価させる。
            return true;
        }

        public void Execute(object parameter)
        {
            // View から実行要求が来たら、包んでいる ViewModel のメソッドを呼び出す。
            _execute();
        }
    }
}
