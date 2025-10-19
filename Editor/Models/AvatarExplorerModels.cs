// Copyright (c) 2025 sakurayuki
// This code is borrowed from AvatarExplorer(https://github.com/puk06/AvatarExplorer)
// AvatarExplorer is licensed under the MIT License. https://github.com/puk06/blob/main/LICENSE

#nullable enable

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UnityEditorAssetBrowser.Models
{
    #region Database Model
    /// <summary>
    /// AvatarExplorerのデータベースモデル
    /// アセットアイテムのリストを管理する
    /// </summary>
    public class AvatarExplorerDatabase
    {
        /// <summary>
        /// アセットアイテムのリスト
        /// </summary>
        [JsonProperty("Items")]
        public List<AvatarExplorerItem> Items { get; set; } = new List<AvatarExplorerItem>();

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        [JsonConstructor]
        public AvatarExplorerDatabase() { }

        /// <summary>
        /// 配列からデータベースを作成するための変換コンストラクタ
        /// </summary>
        /// <param name="items">アイテムの配列</param>
        public AvatarExplorerDatabase(AvatarExplorerItem[] items)
        {
            Items = new List<AvatarExplorerItem>(items);
        }
    }
    #endregion

    #region Item Model
    /// <summary>
    /// AvatarExplorerのアイテムタイプ
    /// アセットの種類を定義する
    /// </summary>
    public enum AvatarExplorerItemType
    {
        /// <summary>
        /// アバター
        /// </summary>
        Avatar,

        /// <summary>
        /// 衣装
        /// </summary>
        Clothing,

        /// <summary>
        /// テクスチャ
        /// </summary>
        Texture,

        /// <summary>
        /// ギミック
        /// </summary>
        Gimmick,

        /// <summary>
        /// アクセサリー
        /// </summary>
        Accessory,

        /// <summary>
        /// 髪型
        /// </summary>
        HairStyle,

        /// <summary>
        /// アニメーション
        /// </summary>
        Animation,

        /// <summary>
        /// ツール
        /// </summary>
        Tool,

        /// <summary>
        /// シェーダー
        /// </summary>
        Shader,

        /// <summary>
        /// カスタムカテゴリー
        /// </summary>
        Custom,

        /// <summary>
        /// 不明
        /// </summary>
        Unknown,
    }

    /// <summary>
    /// AvatarExplorerのアイテムモデル
    /// アセットの詳細情報を管理する
    /// </summary>
    public class AvatarExplorerItem
    {
        /// <summary>
        /// アイテムのタイトル
        /// </summary>
        public string Title { get; set; } = "";

        /// <summary>
        /// 作者名
        /// </summary>
        public string AuthorName { get; set; } = "";

        /// <summary>
        /// アイテムのメモ
        /// </summary>
        public string ItemMemo { get; set; } = "";

        /// <summary>
        /// アイテムのパス
        /// </summary>
        public string ItemPath { get; set; } = "";

        /// <summary>
        /// 画像のパス
        /// </summary>
        public string ImagePath { get; set; } = "";

        /// <summary>
        /// マテリアルのパス
        /// </summary>
        public string MaterialPath { get; set; } = "";

        /// <summary>
        /// 対応アバターのリスト
        /// </summary>
        public string[] SupportedAvatar { get; set; } = Array.Empty<string>();

        /// <summary>
        /// BOOTHのID
        /// </summary>
        public int BoothId { get; set; } = -1;

        /// <summary>
        /// アイテムのタイプ
        /// </summary>
        public int Type { get; set; } = 0;

        /// <summary>
        /// カスタムカテゴリー
        /// </summary>
        public string CustomCategory { get; set; } = "";

        /// <summary>
        /// 作者のID
        /// </summary>
        public string AuthorId { get; set; } = "";

        /// <summary>
        /// サムネイル画像のURL
        /// </summary>
        public string ThumbnailUrl { get; set; } = "";

        /// <summary>
        /// 作成日時
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.MinValue;

        /// <summary>
        /// アイテムのカテゴリー
        /// </summary>
        [JsonIgnore]
        public string Category => GetAECategoryName();

        /// <summary>
        /// 対応アバターのリスト（エイリアス）
        /// </summary>
        [JsonIgnore]
        public string[] SupportedAvatars => SupportedAvatar;

        /// <summary>
        /// タグのリスト（現在は空配列）
        /// </summary>
        [JsonIgnore]
        public string[] Tags => Array.Empty<string>();

        /// <summary>
        /// アイテムのメモ（エイリアス）
        /// </summary>
        [JsonIgnore]
        public string Memo => ItemMemo;

        /// <summary>
        /// AEアイテムのカテゴリー名を取得
        /// Typeの値に基づいてカテゴリー名を決定する
        /// </summary>
        /// <returns>アイテムのカテゴリー名</returns>
        public string GetAECategoryName()
            => GetCategoryNameByType((AvatarExplorerItemType)Type);

        /// <summary>
        /// タイプに基づいてカテゴリー名を取得
        /// </summary>
        /// <param name="itemType">アイテムのタイプ</param>
        /// <returns>対応するカテゴリー名</returns>
        private string GetCategoryNameByType(AvatarExplorerItemType itemType)
        {
            return itemType switch
            {
                AvatarExplorerItemType.Avatar => "アバター",
                AvatarExplorerItemType.Clothing => "衣装",
                AvatarExplorerItemType.Texture => "テクスチャ",
                AvatarExplorerItemType.Gimmick => "ギミック",
                AvatarExplorerItemType.Accessory => "アクセサリー",
                AvatarExplorerItemType.HairStyle => "髪型",
                AvatarExplorerItemType.Animation => "アニメーション",
                AvatarExplorerItemType.Tool => "ツール",
                AvatarExplorerItemType.Shader => "シェーダー",
                AvatarExplorerItemType.Custom => CustomCategory,
                _ => "不明"
            };
        }
    }
    #endregion
}
