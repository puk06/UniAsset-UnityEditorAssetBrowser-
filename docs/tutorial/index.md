---
layout: default
title: "使い方"
extra_css: "tutorial/tutorial.css"
---

<section id="overview">
    <h2>基本的な使い方</h2>
    <p>Unity Editor Asset Browserの主要機能と操作方法をマスターしましょう。</p>
    <!-- 画像推奨: 全体的なUIの概要（タブ、検索、リスト表示） -->
    <div class="image-placeholder">
        <p>💡 <strong>画像:</strong> メインウィンドウの全体概要（各UI要素にラベル付き）</p>
    </div>
</section>

<section id="interface">
    <h2>インターフェース概要</h2>
    <div class="interface-grid">
        <div class="interface-item">
            <h3>📑 タブエリア</h3>
            <p>アバター、アバター関連アセット、ワールドアセット、その他の4つのカテゴリタブ。</p>
            <!-- 画像推奨: タブエリアのクローズアップ -->
            <div class="image-placeholder">
                <p>💡 <strong>画像:</strong> 4つのタブが表示されている状態</p>
            </div>
        </div>

        <div class="interface-item">
            <h3>🔍 検索エリア</h3>
            <p>基本検索、詳細検索、ソート機能を含む検索・フィルタリング機能。</p>
            <!-- 画像推奨: 検索エリアの詳細 -->
            <div class="image-placeholder">
                <p>💡 <strong>画像:</strong> 検索ボックスとソートメニューが表示された状態</p>
            </div>
        </div>

        <div class="interface-item">
            <h3>📋 アイテムリスト</h3>
            <p>サムネイル、タイトル、作者、詳細情報、アクションボタンを含むアイテム表示領域。</p>
            <!-- 画像推奨: アイテムリスト表示 -->
            <div class="image-placeholder">
                <p>💡 <strong>画像:</strong> アイテムリストの表示例（複数アイテム）</p>
            </div>
        </div>

        <div class="interface-item">
            <h3>📄 ページネーション</h3>
            <p>大量のアイテムを効率的に閲覧するためのページ切り替え機能。</p>
            <!-- 画像推奨: ページネーション部分 -->
            <div class="image-placeholder">
                <p>💡 <strong>画像:</strong> ページネーション表示とページ数表示</p>
            </div>
        </div>
    </div>
</section>

<section id="basic-search">
    <h2>基本検索</h2>
    <div class="tutorial-content">
        <div class="step-container">
            <div class="step">
                <span class="step-number">1</span>
                <div class="step-content">
                    <h4>検索キーワードを入力</h4>
                    <p>検索ボックスにキーワードを入力します。タイトル、作者名、カテゴリなど複数の項目を対象に検索します。</p>
                    <!-- 画像推奨: 検索ボックスに「ドレス」などのキーワードが入力された状態 -->
                    <div class="image-placeholder">
                        <p>💡 <strong>画像:</strong> 検索ボックスにキーワード入力中</p>
                    </div>
                    <div class="tip">
                        <strong>💡 Tips:</strong> スペース区切りで複数キーワード検索が可能です
                    </div>
                </div>
            </div>

            <div class="step">
                <span class="step-number">2</span>
                <div class="step-content">
                    <h4>検索結果を確認</h4>
                    <p>入力したキーワードに関連するアイテムがリアルタイムで表示されます。</p>
                    <!-- 画像推奨: 検索結果が表示されている状態 -->
                    <div class="image-placeholder">
                        <p>💡 <strong>画像:</strong> 検索結果リスト表示（該当アイテム複数）</p>
                    </div>
                </div>
            </div>

            <div class="step">
                <span class="step-number">3</span>
                <div class="step-content">
                    <h4>タブ間での検索</h4>
                    <p>タブを切り替えると検索欄が自動的にリセットされ、新しいカテゴリで検索を開始できます。</p>
                    <!-- 画像推奨: タブ切り替え前後の比較 -->
                    <div class="image-placeholder">
                        <p>💡 <strong>画像:</strong> タブ切り替えによる検索欄リセット</p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>

<section id="advanced-search">
    <h2>詳細検索</h2>
    <div class="tutorial-content">
        <div class="advanced-search-info">
            <h3>詳細検索フィールド</h3>
            <p>「詳細検索」ボタンをクリックすると、より細かい条件で検索できます。</p>
            <!-- 画像推奨: 詳細検索フィールドが展開された状態 -->
            <div class="image-placeholder">
                <p>💡 <strong>画像:</strong> 詳細検索フィールド展開状態（全項目表示）</p>
            </div>
        </div>

        <div class="search-fields">
            <div class="search-field">
                <h4>📝 タイトル検索</h4>
                <p>アイテム名での絞り込み検索。部分一致で検索できます。</p>
            </div>

            <div class="search-field">
                <h4>👤 作者名検索</h4>
                <p>作者・クリエイター名での検索。複数の作者名をスペース区切りで検索可能。</p>
            </div>

            <div class="search-field">
                <h4>📂 カテゴリ検索</h4>
                <p>衣装、テクスチャ、ギミックなどのカテゴリでの絞り込み。</p>
            </div>

            <div class="search-field">
                <h4>🎭 対応アバター検索</h4>
                <p>特定のアバターに対応したアイテムの検索。</p>
            </div>

            <div class="search-field">
                <h4>🏷️ タグ検索</h4>
                <p>アイテムに付けられたタグでの検索。</p>
            </div>

            <div class="search-field">
                <h4>📝 メモ検索</h4>
                <p>アイテムに関するメモ・説明文での検索。</p>
            </div>
        </div>
    </div>
</section>

<section id="sorting">
    <h2>ソート機能</h2>
    <div class="tutorial-content">
        <div class="sort-info">
            <h3>「▼ 表示順」ボタンでソート</h3>
            <p>検索結果を様々な条件でソートできます。</p>
            <!-- 画像推奨: ソートメニューが開いた状態 -->
            <div class="image-placeholder">
                <p>💡 <strong>画像:</strong> ソートメニュー展開状態（全ソートオプション表示）</p>
            </div>
        </div>

        <div class="sort-options">
            <div class="sort-option">
                <h4>📅 追加順</h4>
                <p><strong>新しい順 / 古い順</strong><br>データベースに追加された日時順でソート</p>
            </div>

            <div class="sort-option">
                <h4>🔤 アセット名</h4>
                <p><strong>A-Z順 / Z-A順</strong><br>アイテム名のアルファベット順でソート</p>
            </div>

            <div class="sort-option">
                <h4>🏪 ショップ名</h4>
                <p><strong>A-Z順 / Z-A順</strong><br>作者・ショップ名のアルファベット順でソート</p>
            </div>

            <div class="sort-option">
                <h4>🛒 BOOTHID順</h4>
                <p><strong>新しい順 / 古い順</strong><br>BOOTH商品IDの数値順でソート</p>
            </div>
        </div>
    </div>
</section>

<section id="item-actions">
    <h2>アイテム操作</h2>
    <div class="tutorial-content">
        <div class="action-info">
            <h3>各アイテムで利用可能なアクション</h3>
            <!-- 画像推奨: アイテム詳細表示（全ボタンが見える状態） -->
            <div class="image-placeholder">
                <p>💡 <strong>画像:</strong> アイテム詳細表示（サムネイル、情報、ボタン類）</p>
            </div>
        </div>

        <div class="actions-grid">
            <div class="action-item">
                <h4>📁 エクスプローラーで開く</h4>
                <p>アイテムのフォルダをWindowsエクスプローラーで開きます。ファイルの確認や直接操作が可能。</p>
                <!-- 画像推奨: エクスプローラーボタンのクローズアップ -->
                <div class="image-placeholder">
                    <p>💡 <strong>画像:</strong> エクスプローラーボタンと開かれたフォルダ</p>
                </div>
            </div>

            <div class="action-item">
                <h4>📦 Unitypackageをインポート</h4>
                <p>ワンクリックでUnitypackageファイルを現在のプロジェクトにインポート。自動的にフォルダサムネイルも設定されます。</p>
                <!-- 画像推奨: インポートボタンとインポート結果 -->
                <div class="image-placeholder">
                    <p>💡 <strong>画像:</strong> インポートボタンとインポート完了後のプロジェクトフォルダ</p>
                </div>
            </div>

            <div class="action-item">
                <h4>🛒 BOOTH商品ページを開く</h4>
                <p>該当する商品のBOOTHページを直接ブラウザで開きます。購入やアップデート確認が簡単。</p>
                <!-- 画像推奨: BOOTH商品ページボタン -->
                <div class="image-placeholder">
                    <p>💡 <strong>画像:</strong> BOOTH商品ページボタンと開かれたブラウザ</p>
                </div>
            </div>
        </div>
    </div>
</section>

<section id="settings">
    <h2>設定機能</h2>
    <div class="tutorial-content">
        <div class="settings-info">
            <h3>右上の「設定」ボタンから各種設定</h3>
            <!-- 画像推奨: 設定ウィンドウ全体 -->
            <div class="image-placeholder">
                <p>💡 <strong>画像:</strong> 設定ウィンドウ全体（全設定項目が見える状態）</p>
            </div>
        </div>

        <div class="settings-categories">
            <div class="settings-category">
                <h4>📂 データベース設定</h4>
                <ul>
                    <li><strong>AE Database Path:</strong> Avatar Explorerデータベースのパス</li>
                    <li><strong>KA Database Path:</strong> KonoAssetデータベースのパス</li>
                    <li><strong>データベース更新:</strong> 最新データの読み込み</li>
                </ul>
            </div>

            <div class="settings-category">
                <h4>🖼️ サムネイル設定</h4>
                <ul>
                    <li><strong>フォルダサムネイル表示:</strong> プロジェクトフォルダにサムネイル表示するかの設定</li>
                    <li><strong>サムネイル付与:</strong> インポート時の自動サムネイル設定</li>
                    <li><strong>除外フォルダ:</strong> サムネイル対象外フォルダの設定</li>
                </ul>
            </div>

            <div class="settings-category">
                <h4>📑 カテゴリ設定</h4>
                <ul>
                    <li><strong>アセットタイプ設定:</strong> 各カテゴリをどのタブに表示するか</li>
                    <li><strong>カテゴリマッピング:</strong> Avatar Explorerカテゴリの分類設定</li>
                </ul>
            </div>
        </div>
    </div>
</section>

<section id="tips">
    <h2>便利な使い方・Tips</h2>
    <div class="tips-grid">
        <div class="tip-item">
            <h3>🚀 効率的な検索</h3>
            <ul>
                <li>複数キーワードをスペース区切りで入力（例：「ドレス 赤 フリル」）</li>
                <li>作者名での検索でそのクリエイターの全作品を見つける</li>
                <li>タグ検索で関連アイテムをまとめて発見</li>
            </ul>
        </div>

        <div class="tip-item">
            <h3>📁 フォルダ管理</h3>
            <ul>
                <li>インポート時の自動サムネイル設定でフォルダ管理が楽になる</li>
                <li>除外フォルダを設定してTempフォルダなどを対象外に</li>
                <li>プロジェクトブラウザでもサムネイル表示される</li>
            </ul>
        </div>

        <div class="tip-item">
            <h3>⚡ パフォーマンス最適化</h3>
            <ul>
                <li>ページネーションで大量データも快適に閲覧</li>
                <li>画像キャッシュにより二回目以降の表示が高速</li>
                <li>タブ切り替えで検索がリセットされてスムーズ</li>
            </ul>
        </div>

        <div class="tip-item">
            <h3>🔗 外部連携</h3>
            <ul>
                <li>BOOTHページから直接アップデート情報確認</li>
                <li>エクスプローラー連携でファイル操作が簡単</li>
                <li>Unity Packageの直接インポートで作業効率アップ</li>
            </ul>
        </div>
    </div>
</section>

<section id="workflow">
    <h2>典型的なワークフロー</h2>
    <div class="workflow-container">
        <div class="workflow-step">
            <span class="workflow-number">1</span>
            <div class="workflow-content">
                <h4>探したいアセットのタブを選択</h4>
                <p>アバター、アバター関連アセット、ワールドアセット、その他から適切なタブを選択。</p>
            </div>
        </div>

        <div class="workflow-step">
            <span class="workflow-number">2</span>
            <div class="workflow-content">
                <h4>検索キーワードを入力</h4>
                <p>基本検索または詳細検索でアイテムを絞り込み。</p>
            </div>
        </div>

        <div class="workflow-step">
            <span class="workflow-number">3</span>
            <div class="workflow-content">
                <h4>ソート機能で並び替え</h4>
                <p>新しい順、人気順、名前順などで見やすく整理。</p>
            </div>
        </div>

        <div class="workflow-step">
            <span class="workflow-number">4</span>
            <div class="workflow-content">
                <h4>アイテムの詳細確認</h4>
                <p>サムネイル、説明、対応アバター、タグ等の情報を確認。</p>
            </div>
        </div>

        <div class="workflow-step">
            <span class="workflow-number">5</span>
            <div class="workflow-content">
                <h4>アクション実行</h4>
                <p>Unitypackageインポート、BOOTHページ確認、フォルダ参照など必要なアクションを実行。</p>
            </div>
        </div>
    </div>
</section>

<section id="help">
    <h2>さらに詳しく</h2>
    <div class="help-content">
        <p>このチュートリアルで基本的な使い方をマスターできましたか？</p>
        <div class="help-links">
            <a href="../intro/" class="btn btn-secondary">導入方法を確認</a>
            <a href="https://github.com/yuki-2525/UEAB-Claude/issues" class="btn btn-secondary">質問・バグ報告</a>
            <a href="../" class="btn btn-primary">ホームに戻る</a>
        </div>
    </div>
</section>