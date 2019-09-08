using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shell.Core;
using Shell.UI;
// Test App
namespace Shell.Test
{
    class ShellCoretest
    {
        static void Main(string[] args)
        {
            ShellCore ShellCore = new ShellCore();
            ShellUI ShellUI = new ShellUI(); // initalize shellui
            List<int> arguments = new List<int> { 4, 3 }; // V5.5+
            //int[] arguments = new int[2] { 3, 4 }; // initalize our arguments 


            int result = ShellCore.BaseMath(0, 1, arguments);
            int result2 = ShellCore.BaseMath(1, 1, arguments);
            int result3 = ShellCore.BaseMath(2, 1, arguments);
            int result4 = ShellCore.BaseMath(3, 1, arguments);
            int result5 = ShellCore.BaseMath(4, 1, arguments);
            int result6 = ShellCore.BaseMath(5, 1, arguments);
            int result7 = ShellCore.BaseMath(6, 1, arguments);
            int result8 = ShellCore.BaseMath(7, 1, arguments);

            Console.WriteLine($"Basic Math arg 0 result: {result}");
            Console.WriteLine($"Basic Math arg 1 result: {result2}");
            Console.WriteLine($"Basic Math arg 2 result: {result3}");
            Console.WriteLine($"Basic Math arg 3 result: {result4}");
            Console.WriteLine($"Basic Math arg 4 result: {result5}");
            Console.WriteLine($"Basic Math arg 5 result: {result6}");
            Console.WriteLine($"Basic Math arg 6 result: {result7}");
            Console.WriteLine($"Basic Math arg 7 result: {result8}");
            Console.ReadKey();
            ShellCore.CreateFileEx("dummy.tmp");
            Console.ReadKey();
            ShellCore.CopyFileEx("dummy.tmp","dummy2.tmp");
            Console.ReadKey();
            ShellCore.MoveFileEx("dummy2.tmp", "dummy3.tmp");
            Console.ReadKey();
            Console.WriteLine("Now testing ShellUI. Press any key to continue.");
            Console.ReadKey();
            ShellUI.UiDrawMultiple('C',8);
            ShellUI.UiDrawBlankSpace(16);
            Console.Write("test");
            ShellUI.UiSetConsoleColour(ConsoleColor.Black, ConsoleColor.White);
            Console.WriteLine("Now testing ShellCore ExceptionsLite");
            Console.ReadKey();
            ShellCore.ElmInitExceptionManager();
            ShellCore.ElmRegisterExceptionP("ErrorTestException", 10001, "This is a test.", 0);
            ShellCore.ElmThrowException(10001);

            Console.ReadKey();
            // clean up
            ShellCore.DeleteFileEx("dummy.tmp");
            ShellCore.DeleteFileEx("dummy2.tmp");
            ShellCore.DeleteFileEx("dummy3.tmp");
  
            Environment.Exit(0xD15EA5E);
        }
    }
}
