// Copyright (c) 2025 sakurayuki

#nullable enable

using System.Collections.Generic;
using UnityEditor;
using UnityEditorAssetBrowser.Interfaces;
using UnityEditorAssetBrowser.Services;
using UnityEditorAssetBrowser.ViewModels;
using UnityEngine;

namespace UnityEditorAssetBrowser.Views
{
    /// <summary>
    /// メインウィンドウの表示を管理するビュー
    /// アバター、アバター関連アセット、ワールドアセット、その他のタブ切り替えと表示を制御する
    /// </summary>
    public class MainView
    {
        /// <summary>アセットブラウザーのViewModel</summary>
        private readonly AssetBrowserViewModel _assetBrowserViewModel;

        /// <summary>検索のViewModel</summary>
        private readonly SearchViewModel _searchViewModel;

        /// <summary>ページネーションのViewModel</summary>
        private readonly PaginationViewModel _paginationViewModel;

        /// <summary>検索ビュー</summary>
        private readonly SearchView _searchView;

        /// <summary>ページネーションビュー</summary>
        private readonly PaginationView _paginationView;

        /// <summary>アセットアイテムビュー</summary>
        private readonly AssetItemView _assetItemView;

        /// <summary>スクロール位置</summary>
        private Vector2 scrollPosition;

        /// <summary>タブのラベル</summary>
        private static readonly string[] Tabs =
        {
            "アバター",
            "アバター関連アセット",
            "ワールドアセット",
            "その他",
        };

        /// <summary>キャッシュされたAEデータベースパス</summary>
        private string? _cachedAEDatabasePath;

        /// <summary>キャッシュされたKAデータベースパス</summary>
        private string? _cachedKADatabasePath;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="assetBrowserViewModel">アセットブラウザーのViewModel</param>
        /// <param name="searchViewModel">検索のViewModel</param>
        /// <param name="paginationViewModel">ページネーションのViewModel</param>
        /// <param name="searchView">検索ビュー</param>
        /// <param name="paginationView">ページネーションビュー</param>
        /// <param name="aeDatabase">AEデータベース</param>
        public MainView(
            AssetBrowserViewModel assetBrowserViewModel,
            SearchViewModel searchViewModel,
            PaginationViewModel paginationViewModel,
            SearchView searchView,
            PaginationView paginationView
        )
        {
            _assetBrowserViewModel = assetBrowserViewModel;
            _searchViewModel = searchViewModel;
            _paginationViewModel = paginationViewModel;
            _searchView = searchView;
            _paginationView = paginationView;
            _assetItemView = new AssetItemView();

            // データベースパスをキャッシュ
            RefreshDatabasePaths();
        }

        /// <summary>
        /// データベースパスを更新してキャッシュ
        /// </summary>
        private void RefreshDatabasePaths()
        {
            _cachedAEDatabasePath = DatabaseService.GetAEDatabasePath();
            _cachedKADatabasePath = DatabaseService.GetKADatabasePath();
        }

        private List<IDatabaseItem>? _cachedItems = null;

        /// <summary>
        /// メインウィンドウの描画
        /// </summary>
        public void DrawMainWindow()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space(10);

            _searchView.DrawDatabaseButtons();
            DrawTabBar();
            _searchView.DrawSearchField();

            if (Event.current.type == EventType.Used || _cachedItems == null)
            {
                _cachedItems = _searchView.GetSearchResult();
            }

            if (_cachedItems != null)
            {
                DrawSearchResult(_cachedItems);
                DrawContentArea(_cachedItems);
            }

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// タブバーの描画
        /// </summary>
        private void DrawTabBar()
        {
            var newTab = GUILayout.Toolbar(_paginationViewModel.SelectedTab, Tabs);
            if (newTab != _paginationViewModel.SelectedTab)
            {
                _paginationViewModel.SelectedTab = newTab;
                _paginationViewModel.ResetPage();
                _searchViewModel.SetCurrentTab(newTab);
                EditorWindow.focusedWindow?.Repaint();
            }
            EditorGUILayout.Space(10);
        }

        private void DrawSearchResult(List<IDatabaseItem> totalItems)
        {
            EditorGUILayout.LabelField($"検索結果: {totalItems.Count}件");
            EditorGUILayout.Space(10);
        }

        /// <summary>
        /// コンテンツエリアの描画
        /// </summary>
        private void DrawContentArea(List<IDatabaseItem> totalItems)
        {
            GUILayout.BeginVertical();
            DrawScrollView(totalItems);
            _paginationView.DrawPaginationButtons();
            GUILayout.EndVertical();
        }

        /// <summary>
        /// スクロールビューの描画
        /// </summary>
        private void DrawScrollView(List<IDatabaseItem> totalItems)
        {
            scrollPosition = EditorGUILayout.BeginScrollView(
                scrollPosition,
                GUILayout.ExpandHeight(true)
            );
            DrawCurrentTabContent(totalItems);
            EditorGUILayout.EndScrollView();
        }

        /// <summary>
        /// 現在のタブのコンテンツを描画
        /// </summary>
        private void DrawCurrentTabContent(List<IDatabaseItem> totalItems)
            => ShowContents(totalItems);

        /// <summary>
        /// アバターコンテンツの表示
        /// </summary>
        private void ShowContents(List<IDatabaseItem> totalItems)
        {
            var pageItems = _paginationViewModel.GetCurrentPageItems(totalItems);

            // 表示前に必要な画像のみ読み込み
            ImageServices.Instance.UpdateVisibleImages(
                pageItems,
                _cachedAEDatabasePath ?? string.Empty,
                _cachedKADatabasePath ?? string.Empty
            );

            foreach (var item in pageItems)
            {
                _assetItemView.ShowAvatarItem(item);
            }
        }
    }
}
