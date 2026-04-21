// アプリケーションのメイン ViewModel です。このクラスはメインウィンドウ用の
// コマンドや状態を公開し、提供された INavigationService を呼び出して
// ナビゲーションを調整します。
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

        public ICommand NavigateToRirekiCommand { get; }

        /// <summary>
        /// MainViewModel の新しいインスタンスを作成し、ナビゲーションを設定します。
        /// </summary>
        /// <param name="navigationService">ナビゲーション操作を実行するサービス。</param>
        public MainViewModel(INavigationService navigationService)
        {
            NavigateToTransactionsCommand = new RelayCommand(OpenTransactions);
            _navigationService = navigationService;

            NavigateToRirekiCommand = new RelayCommand(OpenRireki);
            _navigationService = navigationService;
        }

        /// <summary>
        /// コマンド実行時の処理。収支用の ViewModel を生成し、
        /// ナビゲーションサービスに対して収支画面の表示を要求します。
        /// </summary>
        private void OpenTransactions()
        {
            var transactionsViewModel = new TrasactionViewModel();
            _navigationService.DisplayTransactions(transactionsViewModel);
        }

        private void OpenRireki()
        {
            var rirekiViewModel = new RirekiViewModel();
            _navigationService.DisplayRireki(rirekiViewModel);
        }
    }
}
