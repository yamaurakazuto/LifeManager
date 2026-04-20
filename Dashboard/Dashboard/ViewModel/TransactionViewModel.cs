// このファイルは収支画面用の ViewModel を定義します。
// 画面が必要とするプロパティや状態を保持します。
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.ViewModel
{
    public class TrasactionViewModel
    {
        /// <summary>
        /// 収支画面に表示するタイトルです。
        /// サンプルの単純なプロパティであり、実際のアプリケーションでは
        /// コレクションやコマンドなどもこの ViewModel で公開します。
        /// </summary>
        public string Title { get; set; } = "Transactions";
    }
}
