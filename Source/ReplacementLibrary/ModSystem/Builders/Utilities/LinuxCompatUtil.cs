using System;
using System.Diagnostics;
using System.IO;
namespace ReplacementLibrary.ModSystem.Builders.Utilities
{
    public static class LinuxCompatUtil
    {

        public static int LinuxRunner(string args) {
            ProcessStartInfo bashinfo = new ProcessStartInfo("Dependencies/LinuxRunner.sh", args);
            Process bashstart = Process.Start(bashinfo);
            bashstart.WaitForExit();
            return bashstart.ExitCode;
        }
    }
}
