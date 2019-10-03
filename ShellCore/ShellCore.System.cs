using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Shell.Core
{
    partial class ShellCore
    {
        public int Debug { get; set; }

        public string LogPath { get; set; }

        public FileStream logStream { get; set; }

        public StreamWriter streamWriter { get; set; }

        public void InitShellCore()
        {
            this.ElmInitialized = 0;
            this.Pi = Math.PI;
            this.E = Math.E;
            this.C = 299792458;
            this.Debug = 1;
        }

        public void ExecuteFileEx(string executePath, List<string> arg)
        {
            foreach (string argument in arg)
            {
                if (argument.Contains('\'')) 
                {
                    ElmThrowException(9); // Exception 9 - Shell does not support that character in strings as it could be confused for \n.
                }

            }

            StringBuilder FinalShutdownString = new StringBuilder(executePath);

            foreach (string argument in arg)
            {
                FinalShutdownString.Append($" {argument}");
            }

            System.Diagnostics.Process.Start(FinalShutdownString.ToString()) ;


        }

        public void Exit(int exitCode)
        {
            ShlCloseLog();
            Environment.Exit(exitCode);
        }


        public FileVersionInfo GetVersion()
        { 
            Assembly ShlCore = Assembly.GetExecutingAssembly();
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(ShlCore.Location);
            return fileVersionInfo;
        }


        public void CheckCompatibility()
        {
            if (System.Environment.OSVersion.Platform != PlatformID.Win32NT)
            {
                ShlHardError("Shell is not compatible with non-Windows NT based operating systems at this time, due to currently upcoming integration with the Windows API. Wine may work, although it has not been tested.", 4);
            }
        }

        public void PrintVersion() // 5.7+ (modified in 7.0 build 5 to use new version of GetVersion()).
        {
            FileVersionInfo version = GetVersion();
            Console.WriteLine(version.FileVersion);
        }

        public void ShlBeep(Nullable<int> frequency = null, Nullable<int> length = null) // 6.0+ - plays a beep noise. Is not compatible with Windows XP x64 Edition and 64-bit Windows Vista.
        {
            if (frequency == null || length == null)
            {
                Console.Beep();
                return;
            }

            Console.Beep(Convert.ToInt32(frequency), Convert.ToInt32(length)); // convert from int32? to int32. 
            return;
        }


        //public void 
        public void ShlHandleCmdArguments(string[] args)
        {
            if (args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    string thearg = args[i];
                    
                    switch (thearg)
                    {
                        case "configlocation":
                            ElmThrowException(24);
                            continue;
                        case "debug":
                            Debug DebugWindow = new Debug();
                            DebugWindow.ShowDialog();
                            return;
                            
                        default:
                            continue;
                    }

                }

             }
             return;

        }

        internal int ShlHardError(string Message, int errorCode) // Used for critical errors where we can't use ExceptionsLite.
        {
            Console.WriteLine("Critical Error\n");
            Console.WriteLine($"CRITICAL-{errorCode}. {Message}. Shell must exit. There is a bug in the Shell which has forced it into a critical unfixable state. You must update or reinstall Shell. ");
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            Environment.Exit(0xD15EA5E + errorCode);
            return 0;
        }

        public void ShlSetWindowTitle(string WindowTitle) //set the window title
        {
            Console.Title = WindowTitle;
            return;
        }


        public void ShlWriteLog(string path, string text)
        {

            if (!File.Exists(path) & Debug == 1)
            {
                try
                {
                    File.Create(path);
                }
                catch (DirectoryNotFoundException)
                {
                    ElmThrowException(32);
                    return;
                }
            }

            else if (File.Exists(path) & Debug == 1) // if debug is on and the file exists
            {
                try
                {
                    this.logStream = File.Open(path, FileMode.Open); // open the file
                    this.streamWriter = new StreamWriter(logStream);
                    streamWriter.WriteLine(text);
                    ShlCloseLog();
                    LogPath = path;
                }
                catch (IOException)
                {
                    ElmThrowException(29);
                    return;
                }
            }
        }


        public void ShlCloseLog()
        {
            this.streamWriter.Close(); // close the stream writer
            this.logStream.Close(); // close the stream
        }

        //TODO: Change from string arrays to collections of strings for these functions in version 5.5. This will be the last function created with it. 
        public void ShlPowerControl(List<string> arg) // power control utilizing ExecuteFileEx
        {
            // FOR LATER USE // List<string> args = new List<string>(); // FOR LATER USE //

            foreach (string argument in arg)
            {
                switch (argument)
                {
                    // Special handling for arguments.
                    case "-i":
                        if (arg[0] != "-i")
                        {
                            ElmThrowException(8);
                        }
                        continue;

                }

            }

            ExecuteFileEx("shutdown", arg);
        }

        


    }
}
