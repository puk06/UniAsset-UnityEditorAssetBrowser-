# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Unity Editor Asset Browser is a Unity Editor extension that integrates Avatar Explorer and KonoAsset databases for browsing and importing Unity assets. It's written in C# using Unity's EditorWindow API and follows MVVM architecture pattern.

## Development Commands

This is a Unity package with no build scripts or test frameworks. Development is done directly through Unity Editor:

- **Testing**: Open Unity Editor and navigate to "Window > Unity Editor Asset Browser" to test the extension
- **Package Installation**: Import the package into a Unity project via Unity Package Manager
- **Development**: Code changes are automatically compiled by Unity when files are saved

### Auto-Commit Settings
**IMPORTANT**: When making code changes, automatically commit them to git with descriptive messages. Always include Claude Code attribution in commits:

```
🤖 Generated with Claude Code
Co-Authored-By: Claude <noreply@anthropic.com>
```

**Auto-commit behavior**: After every significant code change or file modification, immediately run:
1. `git add .`
2. `git commit -m "[descriptive message about changes]` followed by Claude attribution
3. This ensures all development progress is tracked automatically

**Version Updates**: When updating package version:
1. Update `package.json` version field
2. Add corresponding entry to `CHANGELOG.md` following the standardized format
3. Commit both changes together

## Architecture Overview

The project follows MVVM pattern with clear separation of concerns:

### Core Components
- **Main Entry Point**: `Editor/UnityEditorAssetBrowser.cs` - Main EditorWindow accessible via "Window > Unity Editor Asset Browser"
- **Assembly Definition**: `Editor/com.github.yuki-2525.unityeditorassetbrowser.editor.asmdef` - Editor-only assembly

### Directory Structure
- **Models/**: Data structures for AssetItem, AvatarExplorerModels, KonoAssetModels, SearchCriteria, PaginationInfo
- **Views/**: UI rendering classes (MainView, SearchView, PaginationView, AssetItemView, SettingsView)
- **ViewModels/**: Business logic (AssetBrowserViewModel, SearchViewModel, PaginationViewModel, SearchCriteriaManager)
- **Services/**: Data access and business services (DatabaseService, ImageService, ItemSearchService, UnityPackageService, ExcludeFolderService)
- **Helper/**: Utility classes (AEDatabaseHelper, KADatabaseHelper, JsonSettings, GUIStyleManager, CustomDateTimeConverter)
- **Windows/**: Additional editor windows (SettingsWindow)

### Key Patterns
- **Database Integration**: Reads JSON databases from Avatar Explorer and KonoAsset tools
- **Image Caching**: Manages texture loading and caching for asset thumbnails
- **EditorPrefs**: Stores user settings and database paths persistently
- **Pagination**: Handles large datasets with configurable page sizes
- **Search/Filter**: Multi-criteria search across asset metadata
- **Tab System**: Categorizes assets into Avatar, Avatar-related, World, and Other tabs

## Important Implementation Details

### Database Paths
- Avatar Explorer database: Stored in EditorPrefs with key "UnityEditorAssetBrowser_AEDatabasePath"
- KonoAsset database: Stored in EditorPrefs with key "UnityEditorAssetBrowser_KADatabasePath"

### Asset Categorization
- Uses both automatic categorization and user-configurable category mappings
- World assets identified by keywords "ワールド" or "world" in category names
- Category asset types stored in EditorPrefs with pattern "UnityEditorAssetBrowser_CategoryAssetType_[category]"

### Code Style
- Uses nullable reference types (`#nullable enable`)
- Japanese comments mixed with English code
- Constants defined at class level with descriptive names
- EditorWindow lifecycle methods (OnEnable, OnDisable, OnGUI)
- Magic numbers replaced with named constants in dedicated constant classes
- Methods decomposed into single-responsibility functions for maintainability

## External Dependencies

This project integrates with and borrows code from:
- [AE-Tools](https://github.com/puk06/AE-Tools) - MIT License
- [AssetLibraryManager](https://github.com/MAIOTAchannel/AssetLibraryManager) - Used with permission

## Development Notes

### Database Configuration
- First-time setup requires configuring database paths in settings
- AE Database Path: Avatar Explorer's Datas folder (e.g., `C:\VRC-Avatar-Explorer\Datas`)
- KA Database Path: KonoAsset's data folder (e.g., `C:\KonoAssetData`)

### Important Constants & Keys
- Database path keys: `UnityEditorAssetBrowser_AEDatabasePath`, `UnityEditorAssetBrowser_KADatabasePath`
- Category mapping pattern: `UnityEditorAssetBrowser_CategoryAssetType_[category]`
- World category detection: Keywords "ワールド" or "world" in category names
- Asset type constants: Located in `Models/AssetTypeConstants.cs` (AVATAR=0, AVATAR_RELATED=1, WORLD=2, OTHER=3)

### Performance Considerations
- Image cache is cleared on hierarchy changes (`OnHierarchyChanged`)
- Pagination helps manage large datasets
- Database reloading triggers on scene changes
- FolderIconDrawer uses 3-tier caching system (directory, folder icons, textures)
- AssetBrowserViewModel uses cached AssetItem helper for sorting operations

## Package Management

The project uses Unity Package Manager format:
- `package.json` contains package metadata
- Version managed manually in package.json  
- Changes tracked in CHANGELOG.md

### CHANGELOG.md Format
CHANGELOG.md follows a standardized format with three main sections:
- **追加** (Added): New features and functionality
- **変更** (Changed): Changes to existing features or behavior
- **修正** (Fixed): Bug fixes, performance improvements, optimizations, and refactoring