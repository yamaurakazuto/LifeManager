// このクラスは「収支画面（TransactionsView）」のためのViewModelです。
// UIが表示・操作するための状態（データ）を保持し、
// UseCaseから受け取った結果を画面に反映する役割を持ちます。
//
// 重要なポイント:
// ・ビジネスロジック（計算やDB操作）はここに書かない
// ・状態を持つことに専念する（表示のためのデータ）
// ・UIとUseCaseの橋渡し役

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeManager.Application.DTOs;

namespace Dashboard.ViewModel
{
    public class TrasactionViewModel
    {

       
        /// 現在選択されている日付        
        /// なぜ必要か:
        /// ・ユーザーがどの日付の収支を見たいかを表す入力値
        /// ・この値をUseCaseに渡して、その日のデータを取得する
        public DateTime SelectDate { get; set; }

        /// <summary>
        /// 合計金額
        /// </summary>
        public Decimal Total { get; set; }

        /// <summary>
        // 取引一覧
        // なぜIEnumerableか:
        // ・「一覧として扱う」ことを表現したい（コレクションの抽象化） 
        // ・内部実装（Listなど）に依存させないため 
        // なぜprivate setか: /// ・外部（View）から直接書き換えさせないため 
        // ・更新はViewModel内部 or UseCaseの結果のみで行う /// /// なぜDTOを使うか: 
        // ・UseCaseから受け取るデータ形式をそのままUIに渡すため 
        // ・UIとビジネスロジックの依存を切る
        /// </summary>
        public IEnumerable<TransactionDto> Transactions { get; private set; } = new List<TransactionDto>();



    }
}
