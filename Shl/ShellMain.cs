using Shell.Core;
using Shell.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Shell
{
    class Shell
    {
        public static Shell Shl { get; set; } 
        public static ShellCore ShellCore { get; set; }

        public static ShellUI ShellUI { get; set; }

        public string DefaultMenuPath { get; set; }

        // Main Shell code. Nothing here yet.
        static void Main(string[] args) // shell main
        {
            Shl = new Shell();


             // initalize exceptionslite

            int init_result = Shl.Init();
            if (init_result == 1)
            {
                Console.WriteLine("Error Code CRITICAL-001: ShellCore/ShellUI Initalization Failed. Shell cannot start. Press any key to exit.");
                Console.ReadKey();
                Environment.Exit(0xD15EA5E); 

            }
           
        }
        
        internal int Init() // initalizes the shell
        {
            ShellCore = new ShellCore();
            ShellUI = new ShellUI();
            ShellCore.ElmInitExceptionManager();

            if (ShellCore == null || ShellUI == null)
            {
                return 1;
            }

            Console.WriteLine("Cosmo's Shell");
            Console.WriteLine("Version: 0.2.0.0\n");
            Console.WriteLine("Dependency Versions:");

            List<int> shellcore_ver = ShellCore.GetVersion();
            List<int> shellui_ver = ShellUI.GetVersion();

            Console.WriteLine($"ShellCore Version: {shellcore_ver[0]}.{shellcore_ver[1]}.{shellcore_ver[2]}.{shellcore_ver[3]}");
            Console.WriteLine($"ShellUI Version: {shellui_ver[0]}.{shellui_ver[1]}.{shellui_ver[2]}.{shellui_ver[3]}\n");

            Console.WriteLine("*** SHELLCORE WARNING: In Version 5.5, functions have been changed from using arrays of strings to collections of strings. Please update any applications that may be using ShellCore 2.x, 3.x, 4.x, or 5.0. ***\n");

            ShellUI.XmlParseScript("TestMenu.xml");

            foreach (ShxmlVariable ShxmlVar in ShellUI.ShellXML.Varlist)
            {
                switch (ShxmlVar.Type)
                {
                    case 0:
                        Console.WriteLine($"Variable: Name = {ShxmlVar.Name}. Value = {ShxmlVar.varint}. Type = Int.");
                        continue;
                    case 1:
                        Console.WriteLine($"Variable: Name = {ShxmlVar.Name}. Value = {ShxmlVar.varstring}. Type = String.");
                        continue;
                    case 2:
                        Console.WriteLine($"Variable: Name = {ShxmlVar.Name}. Value = {ShxmlVar.varchar}. Type = Char.");
                        continue;
                    case 3:
                        Console.WriteLine($"Variable: Name = {ShxmlVar.Name}. Value = {ShxmlVar.varchar}. Type = Double.");
                        continue;
                    case 4:
                        Console.WriteLine($"Variable: Name = {ShxmlVar.Name}. Value = {ShxmlVar.varchar}. Type = Float.");
                        continue;
                    case 5:
                        Console.WriteLine($"Variable: Name = {ShxmlVar.Name}. Value = {ShxmlVar.varchar}. Type = Boolean.");
                        continue;

                }

            }

            Console.ReadKey();
            return 0;

        }

    }
}
