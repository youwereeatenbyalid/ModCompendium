using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ModCompendiumLibrary.ModSystem.Builders.Utilities
{
    public static class UltraISOUtility
    {
        private const string EXE_BASE_PATH_PARENT = "Dependencies";
        private const string EXE_BASE_PATH_CHILD = "UltraISO";


        private static readonly string sExePath;

        public static bool Available => sExePath != null;

        static UltraISOUtility()
        {

            string[] catcherfiles;
            catcherfiles = System.IO.Directory.GetFiles($"{EXE_BASE_PATH_PARENT}{Path.DirectorySeparatorChar}{EXE_BASE_PATH_CHILD}", "*.lnk");
            if (catcherfiles.Length > 0) 
            {
                if (File.Exists(catcherfiles[0]))
                {
                    sExePath = ShortcutResolver.ResolveShortcut(catcherfiles[0]);
                    if (!File.Exists(sExePath))
                        sExePath = null;
                }
            }
            catcherfiles = System.IO.Directory.GetFiles($"{EXE_BASE_PATH_PARENT}{Path.DirectorySeparatorChar}{EXE_BASE_PATH_CHILD}", "*.exe");
            if (catcherfiles.Length > 0)
            {
                if (sExePath == null && File.Exists(catcherfiles[0]))
                {
                    sExePath = catcherfiles[0];
                }
            }

        }

        public static void ModifyIso(string inIsoPath, string outIsoPath, IEnumerable<string> files)
        {
            // Build arguments
            var arguments = new StringBuilder();




            // Must delete the file if it exists, otherwise the program will fail
            if (File.Exists(outIsoPath))
                File.Delete(outIsoPath);

            // Set up parameters
            ProcessStartInfo processStartInfo;
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows)) 
            {
                arguments.Append($"-input \"{inIsoPath}\" ");

                foreach (var file in files)
                    arguments.Append($"-file \"{file}\" ");
                arguments.Append($"-output {outIsoPath}");
                processStartInfo = new ProcessStartInfo(sExePath, arguments.ToString())
                    {
                        UseShellExecute = false,
                     //   CreateNoWindow = true
                    };

                // Run program

            }
            else
            {
                arguments.Append($"'{sExePath}' ");
                arguments.Append($"'{inIsoPath}' ");
                arguments.Append($"'{outIsoPath}' ");
                foreach (var file in files)
                    arguments.Append($"'{file}' ");

                processStartInfo = new ProcessStartInfo("Dependencies/UltraISO.sh", arguments.ToString());
            }
            var process = Process.Start(processStartInfo);

            process?.WaitForExit();
        }
    }
}
