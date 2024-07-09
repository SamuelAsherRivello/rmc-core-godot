using System.Collections.Concurrent;
using System.Collections.Generic;
using Godot;

namespace RMC.Core.Utilities
{
    public static class FileAccessUtility
    {
        public static bool EnableLogging = false;
        public static HashSet<string> BlacklistFolders = new HashSet<string> { "res://addons", "res://.godot" };

        private static readonly ConcurrentDictionary<string, string> _fileCache = new ConcurrentDictionary<string, string>();
        private static bool _isInitialized = false;

        private static void Initialize()
        {
            if (_isInitialized) return;

            if (EnableLogging) GD.Print($"[FileAccessUtility] Starting initialization at {Time.GetTicksMsec()}");
            IndexAllFiles("res://");
            _isInitialized = true;
            if (EnableLogging) GD.Print($"[FileAccessUtility] Initialization complete at {Time.GetTicksMsec()} with {_fileCache.Count} files indexed.");
        }

        public static bool IsPathWithinResources(string path)
        {
            return path.Contains("res://");
        }

        public static string FindFileOnceInResources(string fileName)
        {
            if (!_isInitialized)
            {
                Initialize();
            }

            if (EnableLogging) GD.Print($"[FileAccessUtility] Starting search for {fileName} at {Time.GetTicksMsec()}");

            if (_fileCache.TryGetValue(fileName, out string cachedPath))
            {
                if (EnableLogging) GD.Print($"[FileAccessUtility] Cache hit for {fileName} at {Time.GetTicksMsec()}");
                return cachedPath;
            }

            if (EnableLogging) GD.PrintErr($"[FileAccessUtility] Failed to find {fileName} at {Time.GetTicksMsec()}");
            return string.Empty;
        }

        private static void IndexAllFiles(string rootPath)
        {
            var directoriesToSearch = new Queue<string>();
            directoriesToSearch.Enqueue(rootPath);

            while (directoriesToSearch.Count > 0)
            {
                string currentPath = directoriesToSearch.Dequeue();

                if (IsBlacklisted(currentPath)) continue;

                var dir = DirAccess.Open(currentPath);

                if (dir == null)
                {
                    if (EnableLogging) GD.PrintErr($"[FileAccessUtility] Failed to open directory: {currentPath}");
                    continue;
                }

                dir.ListDirBegin();
                string fileOrDirName;

                while ((fileOrDirName = dir.GetNext()) != "")
                {
                    if (fileOrDirName == "." || fileOrDirName == "..")
                    {
                        continue;
                    }

                    string fullPath = currentPath.PathJoin(fileOrDirName);

                    if (DirAccess.DirExistsAbsolute(fullPath))
                    {
                        directoriesToSearch.Enqueue(fullPath);
                    }
                    else
                    {
                        _fileCache[fileOrDirName] = fullPath;
                        if (EnableLogging) GD.Print($"[FileAccessUtility] Indexed {fullPath}");
                    }
                }

                dir.ListDirEnd();
            }
        }

        private static bool IsBlacklisted(string path)
        {
            foreach (var blacklistedFolder in BlacklistFolders)
            {
                if (path.StartsWith(blacklistedFolder))
                {
                    return true;
                }
            }
            return false;
        }

        private static string NormalizePath(string path)
        {
            return path.Replace("///", "//").Replace("//", "/");
        }
    }
}
