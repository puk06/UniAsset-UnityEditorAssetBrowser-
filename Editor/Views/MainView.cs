// Copyright (c) 2025 sakurayuki

#nullable enable

using System.Collections.Generic;
using UnityEditor;
using UnityEditorAssetBrowser.Models;
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
            PaginationView paginationView,
            AvatarExplorerDatabase? aeDatabase
        )
        {
            _assetBrowserViewModel = assetBrowserViewModel;
            _searchViewModel = searchViewModel;
            _paginationViewModel = paginationViewModel;
            _searchView = searchView;
            _paginationView = paginationView;
            _assetItemView = new AssetItemView(aeDatabase);

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

        private List<object>? _cachedItems = null;

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
                EditorGUILayout.LabelField($"検索結果: {_cachedItems.Count}件");
                EditorGUILayout.Space(10);

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

        /// <summary>
        /// コンテンツエリアの描画
        /// </summary>
        private void DrawContentArea(List<object> totalItems)
        {
            GUILayout.BeginVertical();
            DrawScrollView(totalItems);
            _paginationView.DrawPaginationButtons();
            GUILayout.EndVertical();
        }

        /// <summary>
        /// スクロールビューの描画
        /// </summary>
        private void DrawScrollView(List<object> totalItems)
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
        private void DrawCurrentTabContent(List<object> totalItems)
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
                if (item is AvatarExplorerItem aeItem)
                {
                    _assetItemView.ShowAvatarItem(aeItem);
                }
                else if (item is KonoAssetAvatarItem kaItem)
                {
                    _assetItemView.ShowKonoAssetItem(kaItem);
                }
            }

            // switch (_paginationViewModel.SelectedTab)
            // {
            //     case 0:
            //         ShowAvatarsContent(totalItems);
            //         break;
            //     case 1:
            //         ShowItemsContent(totalItems);
            //         break;
            //     case 2:
            //         ShowWorldObjectsContent(totalItems);
            //         break;
            //     case 3:
            //         ShowOthersContent(totalItems);
            //         break;
            // }
        }

        /// <summary>
        /// アバターコンテンツの表示
        /// </summary>
        private void ShowAvatarsContent(List<object> totalItems)
        {
            // var filteredSortedItems = _assetBrowserViewModel.GetFilteredAvatars();
            // var sortedItems = _assetBrowserViewModel.SortItems(filteredSortedItems);
            var pageItems = _paginationViewModel.GetCurrentPageItems(totalItems);

            // 表示前に必要な画像のみ読み込み
            ImageServices.Instance.UpdateVisibleImages(
                pageItems,
                _cachedAEDatabasePath ?? string.Empty,
                _cachedKADatabasePath ?? string.Empty
            );

            foreach (var item in pageItems)
            {
                if (item is AvatarExplorerItem aeItem)
                {
                    _assetItemView.ShowAvatarItem(aeItem);
                }
                else if (item is KonoAssetAvatarItem kaItem)
                {
                    _assetItemView.ShowKonoAssetItem(kaItem);
                }
            }
        }

        /// <summary>
        /// アバター関連アセットコンテンツの表示
        /// </summary>
        private void ShowItemsContent(List<object> totalItems)
        {
            // var filteredItems = _assetBrowserViewModel.GetFilteredItems();
            // var sortedItems = _assetBrowserViewModel.SortItems(filteredItems);
            var pageItems = _paginationViewModel.GetCurrentPageItems(totalItems);

            // 表示前に必要な画像のみ読み込み
            ImageServices.Instance.UpdateVisibleImages(
                pageItems,
                _cachedAEDatabasePath ?? string.Empty,
                _cachedKADatabasePath ?? string.Empty
            );

            foreach (var item in pageItems)
            {
                if (item is AvatarExplorerItem aeItem)
                {
                    _assetItemView.ShowAvatarItem(aeItem);
                }
                else if (item is KonoAssetWearableItem kaItem)
                {
                    _assetItemView.ShowKonoAssetWearableItem(kaItem);
                }
            }
        }

        /// <summary>
        /// ワールドアセットコンテンツの表示
        /// </summary>
        private void ShowWorldObjectsContent(List<object> totalItems)
        {
            // var filteredSortedItems = _assetBrowserViewModel.GetFilteredWorldObjects();
            // var sortedItems = _assetBrowserViewModel.SortItems(filteredItems);
            var pageItems = _paginationViewModel.GetCurrentPageItems(totalItems);

            // 表示前に必要な画像のみ読み込み
            ImageServices.Instance.UpdateVisibleImages(
                pageItems,
                _cachedAEDatabasePath ?? string.Empty,
                _cachedKADatabasePath ?? string.Empty
            );

            foreach (var item in pageItems)
            {
                if (item is AvatarExplorerItem aeItem)
                {
                    _assetItemView.ShowAvatarItem(aeItem);
                }
                else if (item is KonoAssetWorldObjectItem worldItem)
                {
                    _assetItemView.ShowKonoAssetWorldObjectItem(worldItem);
                }
            }
        }

        /// <summary>
        /// その他コンテンツの表示
        /// </summary>
        private void ShowOthersContent(List<object> totalItems)
        {
            // var filteredSortedItems = _assetBrowserViewModel.GetFilteredOthers();
            // var sortedItems = _assetBrowserViewModel.SortItems(filteredItems);
            var pageItems = _paginationViewModel.GetCurrentPageItems(totalItems);

            // 表示前に必要な画像のみ読み込み
            ImageServices.Instance.UpdateVisibleImages(
                pageItems,
                _cachedAEDatabasePath ?? string.Empty,
                _cachedKADatabasePath ?? string.Empty
            );

            foreach (var item in pageItems)
            {
                if (item is AvatarExplorerItem aeItem)
                {
                    _assetItemView.ShowAvatarItem(aeItem);
                }
                else if (item is KonoAssetOtherAssetItem otherItem)
                {
                    _assetItemView.ShowKonoAssetOtherAssetItem(otherItem);
                }
            }
        }
    }
}
