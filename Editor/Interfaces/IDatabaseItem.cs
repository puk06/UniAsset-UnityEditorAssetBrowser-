// Copyright (c) 2025 sakurayuki
// Code Written by puk06

using System;

namespace UnityEditorAssetBrowser.Interfaces
{
    public interface IDatabaseItem
    {
        public string GetTitle();
        public string GetAuthor();
        public string GetMemo();
        public string GetItemPath(string databasePath);
        public string GetImagePath(string databasePath);
        public string[] GetSupportedAvatars();
        public int GetBoothId();
        public string GetCategory();
        public string[] GetTags();
        public DateTime GetCreatedDate();
        public bool IsAEDatabase();
    }
}
