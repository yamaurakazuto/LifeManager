// このクラスは「収支画面（TransactionsView）」のためのViewModelです。
// UIが表示・操作するための状態（データ）を保持し、
// UseCaseから受け取った結果を画面に反映する役割を持ちます。
//
// 重要なポイント:
// ・ビジネスロジック（計算やDB操作）はここに書かない
// ・状態を持つことに専念する（表示のためのデータ）
// ・UIとUseCaseの橋渡し役

using LifeManager.Application.DTOs;
using LifeManager.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LifeManager.Commands;
using System.ComponentModel;
using System.ComponentModel;

namespace LifeManager.ViewModel
{

   
    public class TransactionViewModel : INotifyPropertyChanged
    {

        /// <summary>
        /// プロパティ
        /// </summary>

        // 収入
        private Decimal _Income;

        public Decimal Income
        {
            get { return _Income; }
            set
            {
                if (_Income != value)
                {
                    _Income = value;
                    OnPropertyChanged(nameof(Income));
                    OnPropertyChanged(nameof(Total)); // Totalも更新   
                }

            }
        }


        // 支出
        private Decimal _Expense;

        public Decimal Expense
        {
            get { return _Expense; }
            set
            {
                if (_Expense != value)
                {
                    _Expense = value;
                    OnPropertyChanged(nameof(Expense));
                    OnPropertyChanged(nameof(Total)); // Totalも更新
                }
            }
        }

        //合計
        
         public Decimal Total => Income - Expense;
       




        public ICommand LoadCommand { get; }

        private readonly GetDailySummaryUseCase _UseCase;

        // 現在選択されている日付        
        public DateTime SelectDate { get; set; } = DateTime.Today;

        


        //外から変更できないようにprivate setにする
        public IEnumerable<TransactionDto> Transactions { get; private set; } = new List<TransactionDto>();

        public TransactionViewModel()
        {
            _UseCase = new GetDailySummaryUseCase();
            LoadCommand = new RelayCommand(Load);

        }

        public void Load()
        {
            var result = _UseCase.Execute(SelectDate);

            Transactions = result.Transactions;

            //Total = result.Total;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


    }
}
