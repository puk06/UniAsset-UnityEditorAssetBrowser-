// Copyright (c) 2025 sakurayuki

#nullable enable

namespace UnityEditorAssetBrowser.Models
{
    /// <summary>
    /// アセットタイプの定数定義クラス
    /// EditorPrefsで使用するアセットタイプの値を統一管理する
    /// </summary>
    public enum AssetTypeConstants
    {
        /// <summary>アバタータイプ</summary>
        AVATAR,
        
        /// <summary>アバター関連タイプ</summary>
        AVATAR_RELATED,
        
        /// <summary>ワールドタイプ</summary>
        WORLD,
        
        /// <summary>その他タイプ</summary>
        OTHER
    }
}
