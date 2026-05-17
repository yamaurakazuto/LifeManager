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

## 現在のアーキテクチャ

現在の構成は以下のようになっています。

- WPF UI レイヤー
- ViewModel
- Repository レイヤー
- SQL Server
- 日次スナップショットモデル

段階的にレイヤードアーキテクチャへ発展させています。

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

---

## 今後やりたいこと

- TransactionHistory 実装
- 月次集計
- グラフ表示
- Navigation 設計改善
- Dependency Injection
- 非同期 DB 処理
- Validation
- Web API 連携

---

## 技術スタック

- C#
- .NET
- WPF
- XAML
- SQL Server
- MVVM