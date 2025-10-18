// Copyright (c) 2025 sakurayuki

#nullable enable

using System;
using UnityEditorAssetBrowser.Helper;

namespace UnityEditorAssetBrowser.Models
{
    /// <summary>
    /// アセットアイテムの情報を管理するクラス
    /// 様々な形式のアセットアイテムから共通の情報を取得する機能を提供する
    /// </summary>
    public class AssetItem
    {
        /// <summary>
        /// ワールドカテゴリーの日本語名
        /// </summary>
        private const string WORLD_CATEGORY_JP = "ワールド";

        /// <summary>
        /// ワールドカテゴリーの英語名
        /// </summary>
        private const string WORLD_CATEGORY_EN = "world";

        /// <summary>
        /// アイテムのカテゴリー名を取得
        /// </summary>
        /// <param name="item">カテゴリー名を取得するアイテム</param>
        /// <returns>アイテムのカテゴリー名。取得できない場合は空文字列</returns>
        public string GetItemCategoryName(object item)
        {
            if (item is AvatarExplorerItem aeItem)
            {
                return aeItem.GetAECategoryName();
            }
            else if (item is KonoAssetWearableItem wearableItem)
            {
                return wearableItem.Category;
            }
            else if (item is KonoAssetWorldObjectItem worldItem)
            {
                return worldItem.Category;
            }
            else if (item is KonoAssetOtherAssetItem otherItem)
            {
                return otherItem.Category;
            }

            return string.Empty;
        }

        /// <summary>
        /// アイテムのカテゴリー名を取得（エイリアス）
        /// </summary>
        /// <param name="item">アイテム</param>
        /// <returns>カテゴリー名</returns>
        public string GetAECategoryName(object item)
        {
            return GetItemCategoryName(item);
        }

        /// <summary>
        /// アイテムのタイトルを取得
        /// </summary>
        /// <param name="item">タイトルを取得するアイテム</param>
        /// <returns>アイテムのタイトル。取得できない場合は空文字列</returns>
        public string GetTitle(object item)
        {
            if (item is AvatarExplorerItem aeItem)
            {
                return aeItem.Title;
            }
            else if (item is KonoAssetAvatarItem kaItem)
            {
                return kaItem.Description.Name;
            }
            else if (item is KonoAssetWearableItem wearableItem)
            {
                return wearableItem.Description.Name;
            }
            else if (item is KonoAssetWorldObjectItem worldItem)
            {
                return worldItem.Description.Name;
            }
            else if (item is KonoAssetOtherAssetItem otherItem)
            {
                return otherItem.Description.Name;
            }

            return string.Empty;
        }

        /// <summary>
        /// アイテムの作者名を取得
        /// </summary>
        /// <param name="item">作者名を取得するアイテム</param>
        /// <returns>アイテムの作者名。取得できない場合は空文字列</returns>
        public string GetAuthor(object item)
        {
            if (item is AvatarExplorerItem aeItem)
            {
                return aeItem.AuthorName;
            }
            else if (item is KonoAssetAvatarItem kaItem)
            {
                return kaItem.Description.Creator;
            }
            else if (item is KonoAssetWearableItem wearableItem)
            {
                return wearableItem.Description.Creator;
            }
            else if (item is KonoAssetWorldObjectItem worldItem)
            {
                return worldItem.Description.Creator;
            }
            else if (item is KonoAssetOtherAssetItem otherItem)
            {
                return otherItem.Description.Creator;
            }

            return string.Empty;
        }

        /// <summary>
        /// アイテムの作成日を取得
        /// </summary>
        /// <param name="item">作成日を取得するアイテム</param>
        /// <returns>アイテムの作成日（UnixTimeMilliseconds）。取得できない場合は0</returns>
        public long GetCreatedDate(object item)
        {
            if (item is AvatarExplorerItem aeItem)
            {
                if (aeItem.CreatedDate == default)
                    return 0;

                // 日付文字列をUTCのDateTimeに変換
                var utcDateTime = CustomDateTimeConverter.GetDate(aeItem.CreatedDate.ToString());

                // UTCのDateTimeをUnixTimeMillisecondsに変換
                return new DateTimeOffset(utcDateTime, TimeSpan.Zero).ToUnixTimeMilliseconds();
            }
            else if (item is KonoAssetAvatarItem kaItem)
            {
                return kaItem.Description.CreatedAt;
            }
            else if (item is KonoAssetWearableItem wearableItem)
            {
                return wearableItem.Description.CreatedAt;
            }
            else if (item is KonoAssetWorldObjectItem worldItem)
            {
                return worldItem.Description.CreatedAt;
            }
            else if (item is KonoAssetOtherAssetItem otherItem)
            {
                return otherItem.Description.CreatedAt;
            }

            return 0;
        }

        /// <summary>
        /// アイテムのメモを取得
        /// </summary>
        /// <param name="item">メモを取得するアイテム</param>
        /// <returns>アイテムのメモ。取得できない場合は空文字列</returns>
        public string GetMemo(object item)
        {
            if (item is AvatarExplorerItem aeItem)
            {
                return aeItem.Memo ?? string.Empty;
            }
            else if (item is KonoAssetAvatarItem kaItem)
            {
                return kaItem.Description.Memo ?? string.Empty;
            }
            else if (item is KonoAssetWearableItem wearableItem)
            {
                return wearableItem.Description.Memo ?? string.Empty;
            }
            else if (item is KonoAssetWorldObjectItem worldItem)
            {
                return worldItem.Description.Memo ?? string.Empty;
            }
            else if (item is KonoAssetOtherAssetItem otherItem)
            {
                return otherItem.Description.Memo ?? string.Empty;
            }

            return string.Empty;
        }

        /// <summary>
        /// カテゴリーがワールド関連かどうかを判定
        /// </summary>
        /// <param name="category">判定するカテゴリー名</param>
        /// <returns>ワールド関連のカテゴリーの場合はtrue、それ以外はfalse</returns>
        public bool IsWorldCategory(string category)
        {
            return category.Contains(WORLD_CATEGORY_JP, StringComparison.OrdinalIgnoreCase)
                || category.Contains(WORLD_CATEGORY_EN, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// アイテムのBooth Item IDを取得
        /// </summary>
        /// <param name="item">Booth IDを取得するアイテム</param>
        /// <returns>Booth Item ID。取得できない場合や0以下/nullの場合は0</returns>
        public int GetBoothItemId(object item)
        {
            if (item is AvatarExplorerItem aeItem)
            {
                return (aeItem.BoothId > 0) ? aeItem.BoothId : 0;
            }
            else if (item is KonoAssetAvatarItem kaItem)
            {
                return (
                    kaItem.Description.BoothItemId.HasValue
                    && kaItem.Description.BoothItemId.Value > 0
                )
                    ? kaItem.Description.BoothItemId.Value
                    : 0;
            }
            else if (item is KonoAssetWearableItem wearableItem)
            {
                return (
                    wearableItem.Description.BoothItemId.HasValue
                    && wearableItem.Description.BoothItemId.Value > 0
                )
                    ? wearableItem.Description.BoothItemId.Value
                    : 0;
            }
            else if (item is KonoAssetWorldObjectItem worldItem)
            {
                return (
                    worldItem.Description.BoothItemId.HasValue
                    && worldItem.Description.BoothItemId.Value > 0
                )
                    ? worldItem.Description.BoothItemId.Value
                    : 0;
            }
            else if (item is KonoAssetOtherAssetItem otherItem)
            {
                return (
                    otherItem.Description.BoothItemId.HasValue
                    && otherItem.Description.BoothItemId.Value > 0
                )
                    ? otherItem.Description.BoothItemId.Value
                    : 0;
            }
            
            return 0;
        }
    }
}
