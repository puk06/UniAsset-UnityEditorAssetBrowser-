# 画像フォルダ

このフォルダにはUnity Editor Asset Browserのドキュメントで使用する画像を配置します。

## 必要な画像一覧

### トップページ (index.html)
- `main-window.png` - メインウィンドウのスクリーンショット（フルサイズ、複数タブが見える状態）

### 導入方法 (intro/index.html)
- `vcc-startup.png` - VCC起動画面とプロジェクト選択画面
- `vcc-add-repository.png` - VCCのAdd Repository画面とURL入力
- `vcc-package-install.png` - VCCパッケージ一覧でのUEAB選択とインストール
- `unity-package-manager.png` - UnityのWindow メニューからPackage Manager選択
- `package-manager-git-url.png` - Package ManagerのGit URL入力画面
- `avatar-explorer-startup.png` - Avatar Explorer起動画面とデータダウンロード状況
- `unity-window-menu.png` - UnityのWindowメニューでのUEAB選択
- `ueab-settings-button.png` - UEABウィンドウの設定ボタン位置
- `settings-database-paths.png` - 設定ウィンドウでのデータベースパス入力
- `database-update-button.png` - データベース更新ボタンと読み込み状況

### 使い方 (tutorial/index.html)
- `ui-overview-labeled.png` - メインウィンドウの全体概要（各UI要素にラベル付き）
- `tabs-area.png` - 4つのタブが表示されている状態
- `search-area.png` - 検索ボックスとソートメニューが表示された状態
- `item-list.png` - アイテムリストの表示例（複数アイテム）
- `pagination.png` - ページネーション表示とページ数表示
- `search-input.png` - 検索ボックスにキーワード入力中
- `search-results.png` - 検索結果リスト表示（該当アイテム複数）
- `tab-switch-reset.png` - タブ切り替えによる検索欄リセット
- `advanced-search-expanded.png` - 詳細検索フィールド展開状態（全項目表示）
- `sort-menu-open.png` - ソートメニュー展開状態（全ソートオプション表示）
- `item-details.png` - アイテム詳細表示（サムネイル、情報、ボタン類）
- `explorer-button.png` - エクスプローラーボタンと開かれたフォルダ
- `import-button.png` - インポートボタンとインポート完了後のプロジェクトフォルダ
- `booth-button.png` - BOOTH商品ページボタンと開かれたブラウザ
- `settings-window.png` - 設定ウィンドウ全体（全設定項目が見える状態）

## 画像の要件

### 技術的要件
- **形式**: PNG推奨（透明背景が必要な場合）、またはJPG
- **解像度**: 最低1280x720px、高DPI対応のため1920x1080px推奨
- **ファイルサイズ**: ウェブ表示のため1MB以下推奨

### 内容要件
- **UI要素**: 重要なボタンやフィールドが明確に見える
- **テキスト**: 日本語UIの場合は日本語で表示
- **データ**: 実際のアセットデータが表示されている状態
- **状態**: 各機能が動作している状態を撮影

### 撮影のコツ
1. **明るいテーマ**: Unityの明るいテーマを使用
2. **クリーンな状態**: 不要なウィンドウは閉じる
3. **適切なズーム**: 文字が読める程度にズーム
4. **実データ**: 実際のアバター・アセットデータを使用
5. **一貫性**: 同じプロジェクト・設定で撮影

## ファイル命名規則
- 小文字とハイフンを使用: `main-window.png`
- 内容が分かりやすい名前
- 連番が必要な場合: `step-01.png`, `step-02.png`