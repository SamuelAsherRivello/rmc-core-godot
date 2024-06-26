using System;
using Godot;
using System.Collections.Generic;

namespace RMC.Core.Utilities
{
    public static class FileAccessUtility
    {
        public static bool IsPathWithinResources(string path)
        {
            return path.Contains("res://");
        }
        
        /// <summary>
        /// Search for a fileName anywhere in the resources of the project.
        /// Will throw error unless exactly ONE instance is found.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string FindFileOnceInResources(string fileName)
        {
            List<string> foundPaths = new List<string>();
            SearchFileRecursive("res://", fileName, foundPaths);
            
            if (foundPaths.Count == 0)
            {
                GD.PrintErr($"LoadAsync() failed. Cannot find '{fileName}' anywhere in 'res://'.");
                return String.Empty;
            }
            else if (foundPaths.Count > 1)
            {
                GD.Print($"LoadAsync() failed. Found {foundPaths.Count} instances of '{fileName}' in 'res://', but requires exactly 1.");
                foreach (string path in foundPaths)
                {
                    GD.PrintErr(path);
                }
                return String.Empty;
            }

            return NormalizePath(foundPaths[0]);
        }

        private static List<string> SearchFile(string rootPath, string fileName)
        {
            List<string> foundPaths = new List<string>();
            SearchFileRecursive(rootPath, fileName, foundPaths);
            return foundPaths;
        }

        public static void SearchFileRecursive(string currentPath, string fileName, List<string> foundPaths)
        {
            var dir = DirAccess.Open(currentPath);
            if (dir == null)
            {
                return;
            }

            dir.ListDirBegin();
            string fileOrDirName;

            while ((fileOrDirName = dir.GetNext()) != "")
            {
                if (fileOrDirName == "." || fileOrDirName == "..")
                {
                    continue;
                }

                string fullPath = $"{currentPath}/{fileOrDirName}";
                if (DirAccess.DirExistsAbsolute(fullPath))
                {
                    // Recursively search subfolders
                    SearchFileRecursive(fullPath, fileName, foundPaths);
                }
                else if (fileOrDirName == fileName)
                {
                    // File found
                    foundPaths.Add(fullPath);
                }
            }

            dir.ListDirEnd();
        }

        private static string NormalizePath(string path)
        {
            while (path.Contains("///"))
            {
                path = path.Replace("///", "//");
            }
            return path;
        }
    }
}
