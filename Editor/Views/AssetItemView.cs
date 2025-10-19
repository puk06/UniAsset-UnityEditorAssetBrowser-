// Copyright (c) 2025 sakurayuki

#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditorAssetBrowser.Interfaces;
using UnityEditorAssetBrowser.Services;
using UnityEngine;

namespace UnityEditorAssetBrowser.Views
{
    /// <summary>
    /// アセットアイテムの表示を管理するビュー
    /// AvatarExplorerとKonoAssetのアイテムを統一的に表示する
    /// </summary>
    public class AssetItemView
    {
        /// <summary>メモのフォールドアウト状態</summary>
        private readonly Dictionary<string, bool> memoFoldouts = new();

        /// <summary>UnityPackageのフォールドアウト状態</summary>
        private readonly Dictionary<string, bool> unityPackageFoldouts = new();

        // 色を循環させる（赤、青、緑、黄、紫、水色）
        private static readonly Color[] LineColors = new Color[]
        {
            new Color(1f, 0f, 0f, 0.5f), // 赤
            new Color(0f, 0f, 1f, 0.5f), // 青
            new Color(0f, 1f, 0f, 0.5f), // 緑
            new Color(1f, 1f, 0f, 0.5f), // 黄
            new Color(1f, 0f, 1f, 0.5f), // 紫
            new Color(0f, 1f, 1f, 0.5f), // 水色
        };

        /// <summary>
        /// AEアバターアイテムの表示
        /// </summary>
        /// <param name="item">表示するアイテム</param>
        public void ShowAvatarItem(IDatabaseItem item)
        {
            GUILayout.BeginVertical(EditorStyles.helpBox);

            var databasePath = item.IsAEDatabase() ? DatabaseService.GetAEDatabasePath() : DatabaseService.GetKADatabasePath();

            DrawItemHeader(
                item.GetTitle(),
                item.GetAuthor(),
                item.GetImagePath(databasePath),
                item.GetItemPath(databasePath),
                item.GetCreatedDate(),
                item.GetCategory(),
                item.GetSupportedAvatars(),
                item.GetTags(),
                item.GetMemo(),
                item.GetBoothId()
            );
            DrawUnityPackageSection(item.GetItemPath(databasePath), item.GetTitle(), item.GetImagePath(databasePath), item.GetCategory());
            GUILayout.EndVertical();
        }

        /// <summary>
        /// アイテムヘッダーの描画
        /// </summary>
        /// <param name="title">タイトル</param>
        /// <param name="author">作者名</param>
        /// <param name="imagePath">画像パス</param>
        /// <param name="itemPath">アイテムパス</param>
        /// <param name="createdDate">作成日（ソート用）</param>
        /// <param name="category">カテゴリ</param>
        /// <param name="supportedAvatars">対応アバター</param>
        /// <param name="tags">タグ</param>
        /// <param name="memo">メモ</param>
        /// <param name="boothItemId">BoothアイテムID</param>
        private void DrawItemHeader(
            string title,
            string author,
            string imagePath,
            string itemPath,
            DateTime createdDate,
            string category,
            string[] supportedAvatars,
            string[] tags,
            string memo,
            int boothItemId = 0
        )
        {
            GUILayout.BeginHorizontal();
            DrawItemImage(imagePath);
            
            GUILayout.BeginVertical();
            DrawItemBasicInfo(title, author);
            DrawItemMetadata(title, category, supportedAvatars, tags, memo);
            DrawItemActionButtons(itemPath, boothItemId);
            GUILayout.EndVertical();
            
            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// アイテムの基本情報（タイトル・作者）を描画
        /// </summary>
        private void DrawItemBasicInfo(string title, string author)
        {
            GUILayout.Label(title, EditorStyles.boldLabel);
            GUILayout.Label($"作者: {author}");
        }

        /// <summary>
        /// アイテムのメタデータ（カテゴリ・対応アバター・タグ・メモ）を描画
        /// </summary>
        private void DrawItemMetadata(string title, string category, string[] supportedAvatars, string[] tags, string memo)
        {
            // カテゴリ
            if (!string.IsNullOrEmpty(category))
            {
                DrawCategory(category);
            }

            // 対応アバター
            if (supportedAvatars.Length > 0)
            {
                DrawSupportedAvatars(supportedAvatars);
            }

            // タグ
            if (tags.Length > 0)
            {
                GUILayout.Label($"タグ: {string.Join(", ", tags)}", EditorStyles.wordWrappedLabel);
            }

            // メモ
            if (!string.IsNullOrEmpty(memo))
            {
                DrawMemo(title, memo);
            }
        }

        /// <summary>
        /// アクションボタン（エクスプローラー・Booth）を描画
        /// </summary>
        private void DrawItemActionButtons(string itemPath, int boothItemId)
        {
            EditorGUILayout.Space(5);
            DrawExplorerOpenButton(itemPath);
            
            if (boothItemId > 0)
            {
                DrawBoothOpenButton(boothItemId);
            }
        }

        /// <summary>
        /// Booth商品ページを開くボタンを描画
        /// </summary>
        private void DrawBoothOpenButton(int boothItemId)
        {
            if (GUILayout.Button("商品ページを開く", GUILayout.Width(150)))
            {
                Application.OpenURL($"https://booth.pm/ja/items/{boothItemId}");
            }
        }

        /// <summary>
        /// カテゴリの描画
        /// </summary>
        /// <param name="title">タイトル</param>
        /// <param name="category">カテゴリ</param>
        private void DrawCategory(string category)
        {
            GUILayout.Label($"カテゴリ: {category}");
        }

        /// <summary>
        /// 対応アバターの描画
        /// </summary>
        /// <param name="supportedAvatars">対応アバターのパス配列</param>
        private void DrawSupportedAvatars(string[] supportedAvatars)
        {
            string supportedAvatarsText = $"対応アバター: {string.Join(", ", supportedAvatars)}";

            GUILayout.Label(supportedAvatarsText, EditorStyles.wordWrappedLabel);
        }

        /// <summary>
        /// メモの描画
        /// </summary>
        /// <param name="title">タイトル</param>
        /// <param name="memo">メモ</param>
        private void DrawMemo(string title, string memo)
        {
            string memoKey = $"{title}_memo";
            if (!memoFoldouts.ContainsKey(memoKey))
            {
                memoFoldouts[memoKey] = false;
            }

            var startRect = EditorGUILayout.GetControlRect(false, 0);
            var startY = startRect.y;
            var boxRect = EditorGUILayout.GetControlRect(false, EditorGUIUtility.singleLineHeight);

            if (Event.current.type == EventType.MouseDown && boxRect.Contains(Event.current.mousePosition))
            {
                memoFoldouts[memoKey] = !memoFoldouts[memoKey];
                GUI.changed = true;
                Event.current.Use();
            }

            string toggleText = memoFoldouts[memoKey] ? "▼メモ" : "▶メモ";
            EditorGUI.LabelField(boxRect, toggleText);

            if (memoFoldouts[memoKey])
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.LabelField(memo ?? string.Empty, EditorStyles.wordWrappedLabel);
                EditorGUI.indentLevel--;
            }

            var endRect = GUILayoutUtility.GetLastRect();
            var endY = endRect.y + endRect.height;
            var frameRect = new Rect(
                startRect.x,
                startY,
                EditorGUIUtility.currentViewWidth - 20,
                endY - startY + 10
            );
            EditorGUI.DrawRect(frameRect, new Color(0.5f, 0.5f, 0.5f, 0.2f));
            GUI.Box(frameRect, "", EditorStyles.helpBox);
        }

        /// <summary>
        /// アイテム画像の描画
        /// </summary>
        /// <param name="imagePath">画像パス</param>
        private void DrawItemImage(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                return;

            if (File.Exists(imagePath))
            {
                var texture = ImageServices.Instance.LoadTexture(imagePath);
                if (texture != null)
                {
                    GUILayout.Label(texture, GUILayout.Width(100), GUILayout.Height(100));
                }
            }
        }

        /// <summary>
        /// "Explorerで開く"ボタンの描画
        /// </summary>
        /// <param name="itemPath">アイテムパス</param>
        private void DrawExplorerOpenButton(string itemPath)
        {
            if (Directory.Exists(itemPath))
            {
                if (GUILayout.Button("Explorerで開く", GUILayout.Width(150)))
                {
                    Process.Start("explorer.exe", itemPath);
                }
            }
        }

        /// <summary>
        /// UnityPackageアイテムの描画
        /// </summary>
        /// <param name="package">パッケージパス</param>
        /// <param name="imagePath">サムネイル画像パス</param>
        /// <param name="category">カテゴリ</param>
        private void DrawUnityPackageItem(string package, string imagePath, string category)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(string.Join("/", Path.GetDirectoryName(package).Split(Path.DirectorySeparatorChar).TakeLast(2)) + "/" + Path.GetFileName(package));

            if (GUILayout.Button("インポート", GUILayout.Width(100)))
            {
                // フォルダサムネイル生成設定を取得
                bool generateFolderThumbnail = EditorPrefs.GetBool(
                    "UnityEditorAssetBrowser_GenerateFolderThumbnail",
                    true
                );

                if (generateFolderThumbnail)
                {
                    // サムネイルも生成する
                    UnityPackageServices.ImportPackageAndSetThumbnails(package, imagePath, category);
                }
                else
                {
                    // 通常のUnityパッケージインポートのみ
                    AssetDatabase.ImportPackage(package, true);
                }
            }

            GUILayout.EndHorizontal();
        }

        private readonly Dictionary<string, string[]> _cachedUnitypackages = new Dictionary<string, string[]>();

        /// <summary>
        /// UnityPackageセクションの描画
        /// </summary>
        /// <param name="itemPath">アイテムパス</param>
        /// <param name="itemName">アイテム名</param>
        /// <param name="imagePath">サムネイル画像パス</param>
        /// <param name="category">カテゴリ</param>
        private void DrawUnityPackageSection(string itemPath, string itemName, string imagePath, string category)
        {
            if (!_cachedUnitypackages.TryGetValue(itemName, out var unityPackages))
            {
                unityPackages = UnityPackageServices.FindUnityPackages(itemPath);
                _cachedUnitypackages.Add(itemName, unityPackages);
            }

            if (!unityPackages.Any()) return;

            // フォールドアウトの状態を初期化（キーが存在しない場合）
            if (!unityPackageFoldouts.ContainsKey(itemName))
            {
                unityPackageFoldouts[itemName] = false;
            }

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            {
                // 行全体をクリック可能にするためのボックスを作成
                var boxRect = EditorGUILayout.GetControlRect(
                    false,
                    EditorGUIUtility.singleLineHeight
                );
                var foldoutRect = new Rect(
                    boxRect.x,
                    boxRect.y,
                    EditorGUIUtility.singleLineHeight,
                    boxRect.height
                );
                var labelRect = new Rect(
                    boxRect.x + EditorGUIUtility.singleLineHeight,
                    boxRect.y,
                    boxRect.width - EditorGUIUtility.singleLineHeight,
                    boxRect.height
                );

                // フォールドアウトの状態を更新
                if (
                    Event.current.type == EventType.MouseDown
                    && boxRect.Contains(Event.current.mousePosition)
                )
                {
                    unityPackageFoldouts[itemName] = !unityPackageFoldouts[itemName];
                    GUI.changed = true;
                    Event.current.Use();
                }

                // フォールドアウトとラベルを描画
                unityPackageFoldouts[itemName] = EditorGUI.Foldout(
                    foldoutRect,
                    unityPackageFoldouts[itemName],
                    ""
                );
                EditorGUI.LabelField(labelRect, "UnityPackage");

                if (unityPackageFoldouts[itemName])
                {
                    EditorGUI.indentLevel++;
                    for (int i = 0; i < unityPackages.Count(); i++)
                    {
                        DrawUnityPackageItem(unityPackages.ElementAt(i), imagePath, category);

                        // 最後のアイテム以外の後に線を描画
                        if (i < unityPackages.Count() - 1)
                        {
                            var lineRect = EditorGUILayout.GetControlRect(false, 1);
                            Color lineColor = LineColors[i % LineColors.Length];
                            EditorGUI.DrawRect(lineRect, lineColor);
                        }
                    }
                    EditorGUI.indentLevel--;
                }

                // 次のアイテムとの間に余白を追加
                EditorGUILayout.Space(5);
            }
            EditorGUILayout.EndVertical();
        }
        
        /// <summary>
        /// Unitypackageのキャッシュをリセットします。
        /// </summary>
        public void ResetUnitypackageCache()
            => _cachedUnitypackages.Clear();
    }
}
