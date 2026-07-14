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
    // 履歴一覧という「表示のための状態」を保持することだけが責務で、
    // 取得や集計といった業務ロジックはここに置かない。
    public class HistoryViewModel
    {
        // なぜ List ではなく ObservableCollection なのか:
        // 要素の追加・削除時に CollectionChanged が発火し、バインドされた DataGrid が自動更新される。
        // List では、コレクションごと差し替えて通知し直さない限り画面に反映されず、
        // 「状態の変化を View へ伝える」という ViewModel の役割を満たせないため。
        public ObservableCollection<TransactionDto> transactions { get; }
        public HistoryViewModel()
        {
            // なぜコンストラクタで一度だけ生成し、以後差し替えない（get のみ）のか:
            // View は起動時にこのインスタンスへバインドする。参照先を後から入れ替えると
            // バインディングが古い実体を指したままになり表示が更新されない事故を招くため。
            transactions = new ObservableCollection<TransactionDto>();
        }
    }
}
