using System;
using System.Collections.Generic;
using System.IO;
using Godot;
using RMC.Core.Debug;

namespace RMC.Core.Utilities
{
    public static class DotNetCompilerDefinesUtility
    {
        private static readonly string DefinesFilePathBase = "res://addons/RMC Mingleton/Library/3rdParty/RMC Core (Candidate)/Library/Scripts/Generated/";
        private static readonly string DefinesFilename = "CompilerDefines.cs";
        private static readonly string DefinesFilePath;
        
        //
        private const bool IsLoggerEnabled = false;
        private static readonly ILogger _logger;
        
        static DotNetCompilerDefinesUtility()
        {
            _logger = new Logger(IsLoggerEnabled);
            _logger.Prefix = "[DotNetCompilerDefinesUtility]";
            
            // Use Godot's ProjectSettings to determine the project directory
            var projectDir = ProjectSettings.GlobalizePath(DefinesFilePathBase);
            DefinesFilePath = Path.Combine(projectDir, DefinesFilename);
        }

        public static void SetDefine(string symbolName, bool isEnabled)
        {
            _logger.Print($"SetDefine() {symbolName} to {isEnabled}");
            
            var defines = File.Exists(DefinesFilePath) ? File.ReadAllLines(DefinesFilePath) : Array.Empty<string>();
            var newDefines = isEnabled
                ? AddDefine(defines, symbolName)
                : RemoveDefine(defines, symbolName);

            File.WriteAllLines(DefinesFilePath, newDefines);
            DotNetBuildUtility.RebuildDotNetProject();
        }


        public static bool HasDefine2(string symbolName)
        {
            if (!File.Exists(DefinesFilePath))
            {
                return false;
            }

            var defines = File.ReadAllLines(DefinesFilePath);
            foreach (var line in defines)
            {
                if (line.Trim() == $"#define {symbolName}")
                {
                    return true;
                }
            }

            return false;
        }

        
        private static string[] AddDefine(string[] symbolNames, string define)
        {
            foreach (var line in symbolNames)
            {
                if (line.Trim() == $"#define {define}")
                {
                    return symbolNames; // Already defined
                }
            }

            var newDefines = new string[symbolNames.Length + 1];
            symbolNames.CopyTo(newDefines, 0);
            newDefines[symbolNames.Length] = $"#define {define}";
            return newDefines;
        }

        
        private static string[] RemoveDefine(string[] defines, string define)
        {
            var newDefines = new List<string>();
            foreach (var line in defines)
            {
                if (line.Trim() != $"#define {define}")
                {
                    newDefines.Add(line);
                }
            }

            return newDefines.ToArray();
        }
    }
}
