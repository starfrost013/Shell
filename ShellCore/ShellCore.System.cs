using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell.Core
{
    partial class ShellCore
    {

        public List<int> GetVersion()
        {
            List<int> Version = new List<int> { 5, 5, 0, 0 };

            return Version;
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

        public void ExecuteFileEx(string executePath, List<string> arg)
        {
            foreach (string argument in arg)
            {
                if (argument.Contains('\''));
                {
                    ElmThrowException(9); // Exception 9 - Shell does not support that character in strings as it could be confused for \n.
                }

            }

            StringBuilder FinalShutdownString = new StringBuilder(executePath);

            foreach (string argument in arg)
            {
                FinalShutdownString.Append($" {argument}");
            }


            Console.WriteLine(FinalShutdownString);


        }

        public void Exit(int exitCode)
        {
            Environment.Exit(exitCode);
        }


    }
}
