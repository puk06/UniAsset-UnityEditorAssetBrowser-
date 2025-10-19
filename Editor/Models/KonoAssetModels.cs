// Copyright (c) 2025 sakurayuki
// This code is borrowed from AETools(https://github.com/puk06/AE-Tools)
// AETools is licensed under the MIT License. https://github.com/puk06/AE-Tools/blob/master/LICENSE.txt

#nullable enable

using System;
using System.IO;
using Newtonsoft.Json;
using UnityEditorAssetBrowser.Interfaces;

namespace UnityEditorAssetBrowser.Models
{
    #region Base Database Models
    /// <summary>
    /// KonoAssetの基本データベースモデル
    /// データベースのバージョンとアイテムリストを管理する
    /// </summary>
    public class KonoAssetDatabase
    {
        /// <summary>
        /// データベースのバージョン
        /// </summary>
        [JsonProperty("version")]
        public int Version { get; set; }

        /// <summary>
        /// アイテムのリスト
        /// </summary>
        [JsonProperty("data")]
        public object[] Data { get; set; } = Array.Empty<object>();
    }
    #endregion

    #region Specific Database Models
    /// <summary>
    /// アバター用データベース
    /// アバターアイテムのリストを管理する
    /// </summary>
    public class KonoAssetAvatarsDatabase : KonoAssetDatabase
    {
        /// <summary>
        /// アバターアイテムのリスト
        /// </summary>
        public new KonoAssetAvatarItem[] Data { get; set; } = Array.Empty<KonoAssetAvatarItem>();
    }

    /// <summary>
    /// ウェアラブル用データベース
    /// ウェアラブルアイテムのリストを管理する
    /// </summary>
    public class KonoAssetWearablesDatabase : KonoAssetDatabase
    {
        /// <summary>
        /// ウェアラブルアイテムのリスト
        /// </summary>
        public new KonoAssetWearableItem[] Data { get; set; } =
            Array.Empty<KonoAssetWearableItem>();
    }

    /// <summary>
    /// ワールドオブジェクト用データベース
    /// ワールドオブジェクトアイテムのリストを管理する
    /// </summary>
    public class KonoAssetWorldObjectsDatabase : KonoAssetDatabase
    {
        /// <summary>
        /// ワールドオブジェクトアイテムのリスト
        /// </summary>
        public new KonoAssetWorldObjectItem[] Data { get; set; } =
            Array.Empty<KonoAssetWorldObjectItem>();
    }

    /// <summary>
    /// その他アセット用データベース
    /// その他アセットアイテムのリストを管理する
    /// </summary>
    public class KonoAssetOtherAssetsDatabase : KonoAssetDatabase
    {
        /// <summary>
        /// その他アセットアイテムのリスト
        /// </summary>
        public new KonoAssetOtherAssetItem[] Data { get; set; } =
            Array.Empty<KonoAssetOtherAssetItem>();
    }
    #endregion

    #region Item Models
    /// <summary>
    /// ウェアラブルアイテムモデル
    /// 衣装やアクセサリーなどのアイテム情報を管理する
    /// </summary>
    public class KonoAssetWearableItem : IDatabaseItem
    {
        /// <summary>
        /// アイテムのID
        /// </summary>

        [JsonProperty("id")]
        public string Id { get; set; } = "";

        /// <summary>
        /// アイテムの詳細情報
        /// </summary>
        [JsonProperty("description")]
        public KonoAssetDescription Description { get; set; } = new KonoAssetDescription();

        /// <summary>
        /// アイテムのカテゴリー
        /// </summary>
        [JsonProperty("category")]
        public string Category { get; set; } = "";

        /// <summary>
        /// 対応アバターのリスト
        /// </summary>
        [JsonProperty("supportedAvatars")]
        public string[] SupportedAvatars { get; set; } = Array.Empty<string>();

        public string GetTitle()
            => Description.Name;
        public string GetAuthor()
            => Description.Creator;
        public string GetMemo()
            => Description.Memo ?? "";
        public string GetItemPath(string databasePath)
            => Path.Combine(databasePath, "data", Id);
        public string GetImagePath(string databasePath)
            => Path.Combine(databasePath, "images", Id);
        public string[] GetSupportedAvatars()
            => SupportedAvatars;
        public int GetBoothId()
            => Description.BoothItemId ?? -1;
        public string GetCategory()
            => Category;
        public string[] GetTags()
            => Description.Tags;
        public DateTime GetCreatedDate()
            => DateTimeOffset.FromUnixTimeMilliseconds(Description.CreatedAt).DateTime;
    }

    /// <summary>
    /// アバターアイテムモデル
    /// アバターの情報を管理する
    /// </summary>
    public class KonoAssetAvatarItem : IDatabaseItem
    {
        /// <summary>
        /// アバターのID
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = "";

        /// <summary>
        /// アバターの詳細情報
        /// </summary>
        [JsonProperty("description")]
        public KonoAssetDescription Description { get; set; } = new KonoAssetDescription();

        public string GetTitle()
            => Description.Name;
        public string GetAuthor()
            => Description.Creator;
        public string GetMemo()
            => Description.Memo ?? "";
        public string GetItemPath(string databasePath)
            => Path.Combine(databasePath, "data", Id);
        public string GetImagePath(string databasePath)
            => Path.Combine(databasePath, "images", Id);
        public string[] GetSupportedAvatars()
            => Array.Empty<string>();
        public int GetBoothId()
            => Description.BoothItemId ?? -1;
        public string GetCategory()
            => "";
        public string[] GetTags()
            => Description.Tags;
        public DateTime GetCreatedDate()
            => DateTimeOffset.FromUnixTimeMilliseconds(Description.CreatedAt).DateTime;

    }

    /// <summary>
    /// ワールドオブジェクトアイテムモデル
    /// ワールドオブジェクトの情報を管理する
    /// </summary>
    public class KonoAssetWorldObjectItem : IDatabaseItem
    {
        /// <summary>
        /// オブジェクトのID
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = "";

        /// <summary>
        /// オブジェクトの詳細情報
        /// </summary>
        [JsonProperty("description")]
        public KonoAssetDescription Description { get; set; } = new KonoAssetDescription();

        /// <summary>
        /// オブジェクトのカテゴリー
        /// </summary>
        [JsonProperty("category")]
        public string Category { get; set; } = "";

        public string GetTitle()
            => Description.Name;
        public string GetAuthor()
            => Description.Creator;
        public string GetMemo()
            => Description.Memo ?? "";
        public string GetItemPath(string databasePath)
            => Path.Combine(databasePath, "data", Id);
        public string GetImagePath(string databasePath)
            => Path.Combine(databasePath, "images", Id);
        public string[] GetSupportedAvatars()
            => Array.Empty<string>();
        public int GetBoothId()
            => Description.BoothItemId ?? -1;
        public string GetCategory()
            => Category;
        public string[] GetTags()
            => Description.Tags;
        public DateTime GetCreatedDate()
            => DateTimeOffset.FromUnixTimeMilliseconds(Description.CreatedAt).DateTime;

    }

    /// <summary>
    /// その他アセットアイテムモデル
    /// その他アセットの情報を管理する
    /// </summary>
    public class KonoAssetOtherAssetItem : IDatabaseItem
    {
        /// <summary>
        /// アセットのID
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = "";

        /// <summary>
        /// アセットの詳細情報
        /// </summary>
        [JsonProperty("description")]
        public KonoAssetDescription Description { get; set; } = new KonoAssetDescription();

        /// <summary>
        /// アセットのカテゴリー
        /// </summary>
        [JsonProperty("category")]
        public string Category { get; set; } = "";

        public string GetTitle()
            => Description.Name;
        public string GetAuthor()
            => Description.Creator;
        public string GetMemo()
            => Description.Memo ?? "";
        public string GetItemPath(string databasePath)
            => Path.Combine(databasePath, "data", Id);
        public string GetImagePath(string databasePath)
            => Path.Combine(databasePath, "images", Id);
        public string[] GetSupportedAvatars()
            => Array.Empty<string>();
        public int GetBoothId()
            => Description.BoothItemId ?? -1;
        public string GetCategory()
            => Category;
        public string[] GetTags()
            => Description.Tags;
        public DateTime GetCreatedDate()
            => DateTimeOffset.FromUnixTimeMilliseconds(Description.CreatedAt).DateTime;
    }
    #endregion

    #region Description Model
    /// <summary>
    /// KonoAssetアイテムの詳細情報モデル
    /// アイテムの基本情報を管理する
    /// </summary>
    public class KonoAssetDescription
    {
        /// <summary>
        /// アイテムの名前
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = "";

        /// <summary>
        /// 作者名
        /// </summary>
        [JsonProperty("creator")]
        public string Creator { get; set; } = "";

        /// <summary>
        /// 画像ファイル名
        /// </summary>
        [JsonProperty("imageFileName")]
        public string ImageFilename { get; set; } = "";

        /// <summary>
        /// タグのリスト
        /// </summary>
        [JsonProperty("tags")]
        public string[] Tags { get; set; } = Array.Empty<string>();

        /// <summary>
        /// メモ
        /// </summary>
        [JsonProperty("memo")]
        public string? Memo { get; set; }

        /// <summary>
        /// BOOTHのアイテムID
        /// </summary>
        [JsonProperty("boothItemId")]
        public int? BoothItemId { get; set; }

        /// <summary>
        /// 依存アイテムのリスト
        /// </summary>
        [JsonProperty("dependencies")]
        public string[] Dependencies { get; set; } = Array.Empty<string>();

        /// <summary>
        /// 作成日時（UnixTimeMilliseconds）
        /// </summary>
        [JsonProperty("createdAt")]
        public long CreatedAt { get; set; }

        /// <summary>
        /// 公開日時（UnixTimeMilliseconds）
        /// </summary>
        [JsonProperty("publishedAt")]
        public long? PublishedAt { get; set; }
    }
    #endregion
}
