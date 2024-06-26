using System.Diagnostics;
using System.IO;
using Godot;
using RMC.Core.Debug;

namespace RMC.Core.Utilities
{
    public static class DotNetBuildUtility
    {
        //
        private const bool IsLoggerEnabled = true;
        private static readonly ILogger _logger;

        static DotNetBuildUtility()
        {
            _logger = new Logger(IsLoggerEnabled);
            _logger.Prefix = "[DotNetBuildUtility]";
        }
        
        public static void RebuildDotNetProject()
        {
            // Get the path to the res:// directory
            string godotResPath = ProjectSettings.GlobalizePath("res://");
            // Get the parent directory of the res:// directory
            string godotProjectPath = new DirectoryInfo(godotResPath).FullName;
            string godotProjectName = ProjectSettings.GetSetting("application/config/name").ToString();
            string godotSolutionPath = Path.Combine(godotProjectPath, godotProjectName + ".sln");
            
            _logger.Print($"BuildDotNetProject({godotProjectName})");
            

            if (!File.Exists(godotSolutionPath))
            {
                GD.PrintErr($"The solution must exist before building. Create at '{godotSolutionPath}'.");
                return;
            }
            
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = $"build {godotSolutionPath}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process())
            {
                process.StartInfo = startInfo;
                
                //Show logging
                //process.OutputDataReceived += (sender, args) => _logger.GDPrint(args.Data);
                
                //Show error logging
                process.ErrorDataReceived += (sender, args) =>
                {
                    if (args.Data != null && args.Data.Length > 0)
                    {
                        _logger.PrintErr(args.Data);
                    }
                
                };
                
                //
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();
            }
        }
    }
}
