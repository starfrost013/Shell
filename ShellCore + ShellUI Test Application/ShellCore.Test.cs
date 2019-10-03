using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shell.Core;
using Shell.UI;

// Test App
namespace Shell.Test
{
    class ShellTest
    {
        [STAThread]
        static void Main(string[] args)
        {
            ShellTest ShellTest = new ShellTest();
            ShellCore ShellCore = new ShellCore();
            ShellUI ShellUI = new ShellUI(); // initalize shellui
            List<double> arguments = new List<double> { 4, 3 }; // v5.5+


            FileVersionInfo shellcore_ver = ShellCore.GetVersion();

            if (shellcore_ver.ProductMajorPart < 8) // version check
            {
                ShellTest.SorryIncompatible();
            }
            else if (shellcore_ver.ProductMajorPart < 8 & shellcore_ver.ProductBuildPart < 50)
            {
                ShellTest.SorryIncompatible();
            }

            ShellCore.InitShellCore();
            ShellCore.ElmInitExceptionManager();
            Console.Title = "Shell Test Application";
            Console.WriteLine("This is an automated testing application designed for testing Shell. This will bring up standard Windows dialogs and create registry keys to verify the functionality of the Shell DLLs. Press enter to continue.");
            Console.ReadKey();


            // V2+, last modified v7.0 build 24 iirc
            double result = ShellCore.BaseMath(ShellCore.BaseMathOperation.Add, 1, arguments);
            double result2 = ShellCore.BaseMath(ShellCore.BaseMathOperation.Subtract, 1, arguments);
            double result3 = ShellCore.BaseMath(ShellCore.BaseMathOperation.Divide, 1, arguments);
            double result4 = ShellCore.BaseMath(ShellCore.BaseMathOperation.Multiply, 1, arguments);
            double result5 = ShellCore.BaseMath(ShellCore.BaseMathOperation.Modulus, 1, arguments);
            double result6 = ShellCore.BaseMath(ShellCore.BaseMathOperation.Sqrt, 1, arguments);
            double result7 = ShellCore.BaseMath(ShellCore.BaseMathOperation.Sin, 1, arguments);
            double result8 = ShellCore.BaseMath(ShellCore.BaseMathOperation.Cos, 1, arguments);
            double result9 = ShellCore.BaseMath(ShellCore.BaseMathOperation.Tan, 1, arguments);
            double result10 = ShellCore.BaseMath(ShellCore.BaseMathOperation.Asin, 1, arguments);
            double result11 = ShellCore.BaseMath(ShellCore.BaseMathOperation.Acos, 1, arguments);
            double result12 = ShellCore.BaseMath(ShellCore.BaseMathOperation.Atan, 1, arguments);
            double result13 = ShellCore.Power(4, 3);
            List<double> result14 = ShellCore.Ratio(6, 4, 3); // v7+

            Console.WriteLine($"Basic Math arg 0 result: {result}");
            Console.WriteLine($"Basic Math arg 1 result: {result2}");
            Console.WriteLine($"Basic Math arg 2 result: {result3}");
            Console.WriteLine($"Basic Math arg 3 result: {result4}");
            Console.WriteLine($"Basic Math arg 4 result: {result5}");
            Console.WriteLine($"Basic Math arg 5 result: {result6}");
            Console.WriteLine($"Basic Math arg 6 result: {result7}");
            Console.WriteLine($"Basic Math arg 7 result: {result8}");
            Console.WriteLine($"Basic Math arg 8 result: {result9}");
            Console.WriteLine($"Basic Math arg 9 result: {result10}");
            Console.WriteLine($"Basic Math arg 10 result: {result11}");
            Console.WriteLine($"Basic Math arg 11 result: {result12}");
            Console.WriteLine($"Power result: {result13}");
            Console.WriteLine($"Ratio result: {result14[0]}, {result14[1]}");

            ShellCore.WinMsgBox("Shell", "This is a message box!"); // v6+
            string openResult = ShellCore.WinOpenDialog("ShellCore Version 6 open dialog",".txt", "Text documents (.txt)|*.txt"); // v6+
            
            if (openResult == null)
            {
                Console.WriteLine("No file selected.");
            }
            else
            {
                Console.WriteLine(openResult);
            }

            string saveResult = ShellCore.WinSaveDialog("ShellCore Version 6 save dialog", ".txt", "Text documents (.txt)|*.txt"); // v6+

            if (saveResult == null)
            {
                Console.WriteLine("No file selected.");
            }

            ShellCore.ShlBeep(1000, 400);
            ShellCore.CreateFileEx("dummy.tmp");
            ShellCore.CopyFileEx("dummy.tmp", "dummy2.tmp");
            ShellCore.MoveFileEx("dummy2.tmp", "dummy3.tmp");
            ShellCore.CreateFileEx("dummy4.tmp");
            ShellCore.SetFileHiddenEx("dummy4.tmp"); // v7+
            Console.WriteLine("Now testing ShellUI...");
            ShellUI.UiDrawMultiple('T',8);
            ShellUI.UiDrawBlankSpace(16);
            ShellUI.UiSetConsoleColour(ConsoleColor.Black, ConsoleColor.White);
            ShellUI.UiSetWindowSize(25, 10);
            ShellUI.UiSetWindowSize(100, 63);
            ShellUI.UiSetCursorPosition(24, 24);
            ShellUI.UiSetCursorSize(5); 
            ShellCore.WinPlaySound("tada.wav"); // v8.0.50+
            ShellUI.UiSetWindowTitle("ddddddddddddddddddddddddd");
            Console.WriteLine("Now testing ShellCore ExceptionsLite");

            ShellCore.ShlWriteLog("ShellLog.txt", "This is a test"); // v6+
            ShellCore.ShlCloseLog(); // v6+
            ShellCore.WinCreateRegKey(RegistryHive.CurrentUser, "ShellTest"); // v7+
            ShellCore.WinCreateRegKey(RegistryHive.CurrentUser, "ShellTest2"); // v7+
            RegistryKey Reg = ShellCore.WinOpenRegKey(RegistryHive.CurrentUser, "ShellTest"); // v7+
            
            ShellCore.WinSetRegValue(Reg, "Test", "TestThis"); // v7+
            ShellCore.WinSetRegValue(Reg, "Test3", "This"); // v7+
            ShellCore.WinDeleteRegValue(Reg, "Test3"); // v7+
            ShellCore.WinDeleteRegKey(RegistryHive.CurrentUser, "ShellTest2"); // v7+
            ShellCore.DownloadFileEx("http://shell.x10.mx/dummy.txt", "dummy2.txt"); // v8.0.62+
            ShellCore.ElmRegisterExceptionP("ErrorTestException", 10001, "This is a test.", 0); // v4+
            ShellCore.ElmThrowException(10001); // v4+

            // clean up
            ShellCore.DeleteFileEx("dummy.tmp"); // v4+
            ShellCore.DeleteFileEx("dummy2.tmp"); // v4+
            ShellCore.DeleteFileEx("dummy2.txt"); // v4+
            ShellCore.DeleteFileEx("dummy3.tmp"); // v4+ 
            ShellCore.DeleteFileEx("dummy4.tmp"); // v4+

            Console.WriteLine("Testing complete. If it didn't crash or display any exceptions (excluding the test exception triggered, #10001), that means it's all good. Press enter to exit.");
            Console.ReadKey();
            Environment.Exit(0xD15EA5E);
        }

        internal void SorryIncompatible()
        {
            Console.WriteLine("The Test Application is only compatible with ShellCore 8.0 build 50 and later. If you need to use ShellCore 7.0, use version 1.8.5 of the application. If you need to use the ShellCore 7.0 alpha, use version 1.7.3 of the application. If you need to use ShellCore 5.5, 5.6, 5.7, or 6.0, use version 1.7.2 of the application.");
            Console.ReadKey();
            Environment.Exit(0xCCCCCCC);
            return;
        }
    }
}
