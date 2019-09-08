using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// These interfaces allow us to use multiple classes per section, or not have every function usable.
// Todo: Use partial keyword.

// DO NOT USE - HEAVILY UNDER CONSTRUCTION - DO NOT USE - HEAVILY UNDER CONSTRUCTION - DO NOT USE - HEAVILY UNDER CONSTRUCTION - DO NOT USE - HEAVILY UNDER CONSTRUCTION - DO NOT USE //

namespace Shell.Core // Encore
{
    interface IShellCore
    {
        int[] GetVersion();

        bool VerifyExistence(string path);

        bool CreateFileEx(string path, string initialContent = null, bool verifyThatNotAlreadyExist = false);

        bool DeleteFileEx(string path, bool askUserConsoleOnly = false, string confirmationMessage = "Are you sure you want to delete");

        bool CopyFileEx(string oldpath, string newpath, bool operationSuccessfulMessageConsoleOnly = false, string operationSuccessfulMessage = "The file was successfully copied to");

        bool MoveFileEx(string oldpath, string newpath, bool operationSuccessfulMessageConsoleOnly = false, string operationSuccessfulMessage = "The file was successfully moved to");

        int BaseMath(int operationId, int numOfArguments, int[] args);

        double Sqrt(int num);

        double Sin(int num);

        double Cos(int num);

        double Tan(int num);

        void Exit(int ExitCode);
    }


}

// DO NOT USE - HEAVILY UNDER CONSTRUCTION - DO NOT USE - HEAVILY UNDER CONSTRUCTION - DO NOT USE - HEAVILY UNDER CONSTRUCTION - DO NOT USE - HEAVILY UNDER CONSTRUCTION - DO NOT USE //