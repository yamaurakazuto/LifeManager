# LifeManager

LifeManager は C#, WPF, MVVM, SQL Server を使用して開発している家計簿・ライフマネジメントアプリです。

単なる UI 開発だけではなく、ソフトウェア設計・責務分離・データベース設計を意識しながら開発しています。

---

## 目的

このプロジェクトでは以下を学習・実践することを目的としています。

- MVVM アーキテクチャ
- WPF アプリケーション設計
- SQL Server 連携
- Repository パターン
- 責務分離
- ドメイン指向設計の考え方
- 金融データモデリング

---

## アーキテクチャ（MVVM + レイヤード）

MVVM を軸に、Application 層（UseCase / DTO）と Repository 層を加えた構成です。

```
┌─────────────────────────────────────────────┐
│ View (Views/*.xaml, *.xaml.cs)              │  画面の見た目・純粋な UI 制御のみ
│   MainWindow / TransactionsView / History   │
└──────────────┬──────────────────────────────┘
               │ DataBinding / ICommand（コードビハインドは書かない）
┌──────────────▼──────────────────────────────┐
│ ViewModel (ViewModel/*.cs)                  │  画面用の状態と操作を公開
│   MainViewModel / TransactionViewModel      │  INotifyPropertyChanged で変更通知
│   HistoryViewModel                          │  IDataErrorInfo で入力検証
└──────────────┬──────────────────────────────┘
               │ UseCase 呼び出し（DTO を受け取る）
┌──────────────▼──────────────────────────────┐
│ Application (Application/UseCases, DTOs)    │  業務の操作単位（ユースケース）と
│   GetDailySummaryUseCase                    │  層をまたぐデータの受け渡し形
└──────────────┬──────────────────────────────┘
               │
┌──────────────▼──────────────────────────────┐
│ Repository / Model (Repositories, Models)   │  DB アクセスの隔離・テーブル対応モデル
│   DailySummaryRepository / DailySummaries   │
└──────────────┬──────────────────────────────┘
               │ SqlConnection
┌──────────────▼──────────────────────────────┐
│ SQL Server（DailySummaries テーブル）         │
└─────────────────────────────────────────────┘
```

### 各層の責務と「なぜ分けるのか」

| 層 | 責務 | なぜ必要か |
|---|---|---|
| View | XAML による画面定義。数字のみ入力可にする等の純粋な UI 制御 | ロジックを持たせないことで、画面の動作を ViewModel 側で完結・テスト可能にする |
| ViewModel | 表示用の状態保持、`ICommand` の公開、変更通知、入力検証 | View と 1:1 で対応しつつ View を参照しない。UI なしで振る舞いを検証できる |
| UseCase | 「日次サマリーを取得する」等、業務の操作 1 つを表す | ViewModel からビジネスロジックを追い出し、責務の肥大化を防ぐ |
| DTO | 層をまたいで受け渡すデータの形 | DB スキーマの都合を UI に伝染させない防波堤 |
| Repository | SQL・接続文字列など DB の都合を閉じ込める | 保存先の変更（別 DB / Web API）の影響をこの層だけに留める |
| Model | DB テーブル 1 行に対応するクラス | テーブル構造をコード上で型として表現する |

### MVVM を支える仕組み

- **DataBinding** — View は `{Binding プロパティ名}` で ViewModel とつながる。互いのクラスを直接参照しないため疎結合になる
- **`INotifyPropertyChanged`** — ViewModel の値変更を View に通知し画面を再描画させる。これがないと値を変えても画面が更新されない
- **`ICommand` (RelayCommand)** — ボタンのクリックを Click イベントではなくコマンドで ViewModel に届ける。コードビハインドを空に保つための要
- **`IDataErrorInfo`** — 「収入は 0 以上」等の入力検証ルールを ViewModel 側に集約。View は `ValidatesOnDataErrors=True` と書くだけ
- **`INavigationService`** — 画面遷移の抽象。ViewModel が Window を直接 new すると View 依存が生まれるため、抽象への依頼に留める。実装（`NavigationService`）だけが View を知る

---

## ディレクトリ構成

```
LifeManager/
├── App.xaml / App.xaml.cs        # エントリポイント・依存の組み立て（Composition Root）
├── Views/                        # View 層（XAML + 最小限のコードビハインド）
│   ├── MainWindow.xaml           # メインメニュー
│   ├── TransactionsView.xaml     # 収支入力画面
│   └── HistoryWindow.xaml        # 収支履歴画面
├── ViewModel/                    # ViewModel 層
│   ├── MainViewModel.cs          # 画面遷移コマンドを公開
│   ├── TransactionViewModel.cs   # 収支入力の状態・検証・合計計算
│   ├── HistoryViewModel.cs       # 履歴一覧の状態
│   └── INavigationService.cs     # 画面遷移の抽象（依存の向きを保つため ViewModel 側に配置）
├── Commands/
│   └── RelayCommand.cs           # Action を ICommand に変換する汎用コマンド
├── Serviecs/
│   └── NavigationService.cs      # INavigationService 実装。View を new してよい唯一の場所
├── Application/
│   ├── UseCases/                 # 業務操作（GetDailySummaryUseCase）
│   └── DTOs/                     # 層間のデータ受け渡し（TransactionDto, DailySummaryDto）
├── Models/
│   └── DailySummaries.cs         # DailySummaries テーブル対応モデル
└── Repositories/
    └── DailySummaryRepository.cs # DB アクセス層（実装中）
```

---

## データの流れ（収支画面の例）

1. `MainWindow` のボタンが `NavigateToTransactionsCommand` を実行
2. `MainViewModel` が `TransactionViewModel` を生成し、`INavigationService.DisplayTransactions()` に表示を依頼
3. `NavigationService` が `TransactionsView` を生成し、DataContext に ViewModel を設定して表示
4. ユーザーが「表示」ボタンを押すと `LoadCommand` → `GetDailySummaryUseCase.Execute()` が実行される
5. UseCase が `DailySummaryDto`（一覧 + 合計）を返し、ViewModel が状態として保持
6. `INotifyPropertyChanged` の通知により、バインドされた DataGrid / 合計表示が自動更新される

ViewModel は View を一切参照せず、View はプロパティ名（バインディング）だけで ViewModel とつながる — これが本プロジェクトの MVVM の核です。

---

## DailySummary 設計

このアプリでは日次の収支スナップショットを保存しています。

単純に現在の残高だけを計算するのではなく、
「その日時点での累積状態」を履歴として保持する設計にしています。

これにより以下を可能にしています。

- 履歴追跡
- 月次集計
- 収支分析
- スナップショットベースの状態管理

なお Income / Expense を相殺せず別カラムで保持するのは、
「収入 10 万・支出 10 万の日」と「収支ゼロの日」を区別し、収支分析を可能にするためです。

---

## Database

現在のメインテーブル:

### DailySummaries

| Column | Type |
|---|---|
| Id | INT |
| SummaryDate | DATE |
| Income | DECIMAL(18,2) |
| Expense | DECIMAL(18,2) |
| Total | DECIMAL(18,2) |
| CreatedAt | DATETIME2 |
| UpdatedAt | DATETIME2 |

金額カラムに DECIMAL を使うのは、浮動小数点（FLOAT）では金銭計算に誤差が生じるためです。

---

## 実装状況

- [x] MVVM の基本構成（View / ViewModel 分離、DataBinding、RelayCommand）
- [x] 画面遷移の抽象化（INavigationService / NavigationService）
- [x] 入力検証（IDataErrorInfo + 数字のみ入力の UI 制御）
- [x] UseCase / DTO 層の骨格（現在はダミーデータを返却）
- [x] SQL Server への接続確認
- [ ] Repository 実装（UseCase からの DB 取得・保存）
- [ ] SaveCommand による収支データの永続化
- [ ] 履歴画面のデータ表示

---

## 今後やりたいこと

- TransactionHistory 実装
- 月次集計
- グラフ表示
- Navigation 設計改善
- Dependency Injection（Composition Root の一本化）
- 非同期 DB 処理
- Validation の拡充
- Web API 連携

---

## 技術スタック

- C#
- .NET 8
- WPF
- XAML
- Material Design In XAML
- SQL Server
- MVVM
