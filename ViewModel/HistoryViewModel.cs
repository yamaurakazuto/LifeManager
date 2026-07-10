using LifeManager.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace LifeManager.ViewModel
{
    // 収支履歴画面（HistoryWindow）用の ViewModel。
    // 履歴一覧という「表示のための状態」を保持することが責務。
    public class HistoryViewModel
    {
        // なぜ List ではなく ObservableCollection なのか:
        // 要素の追加・削除時に CollectionChanged イベントが発火し、
        // バインドされた DataGrid が自動で更新されるため。
        // List だとコレクションを丸ごと差し替えて再通知しない限り画面に反映されない。
        public ObservableCollection<TransactionDto> transactions { get; }
        public HistoryViewModel()
        {
            // コンストラクタで生成して以後差し替えない（get のみ）ことで、
            // バインディングの参照先が途中で無効になる事故を防ぐ。
            transactions = new ObservableCollection<TransactionDto>();
        }
    }
}
