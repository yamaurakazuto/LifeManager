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

namespace LifeManager.ViewModel
{

   
    public class TransactionViewModel
    {
        public ICommand LoadCommand { get; }

        private readonly GetDailySummaryUseCase _UseCase;

        // 現在選択されている日付        
        public DateTime SelectDate { get; set; } = DateTime.Today;

        // 合計金額
        public Decimal Total { get; set; }


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

            Total = result.Total;
        }


    }
}
