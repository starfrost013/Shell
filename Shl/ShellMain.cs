using Shell.Core;
using Shell.UI;
using Shell.Module;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public static ShellModule ShellModule { get; set; }
        public string DefaultMenuPath { get; set; }

        // Main Shell code. Nothing here yet.
        [STAThread] // unfortunately we have to use this
        static void Main(string[] args) // shell main
        {
            Shl = new Shell();
            
            bool? init_result = Shl.Init(args);
            if (init_result == null)
            {
                Console.WriteLine("Error Code CRITICAL-001: ShellCore/ShellUI Initalization Failed. Shell cannot start. Press any key to exit.");
                Console.ReadKey();
                ShellCore.Exit(0); 

            }
           
        }
        
        internal bool? Init(string[] args) // initalizes the shell
        {
            ShellCore = new ShellCore();
            ShellUI = new ShellUI();
            ShellModule = new ShellModule();
            ShellCore.InitShellCore();
            ShellCore.CheckCompatibility();
            ShellCore.ElmInitExceptionManager();
            ShellCore.ShlWriteLog("Test.txt","test.");
            ShellUI.InitShellUI();
            ShellModule.InitShellModule(ShellCore);
            Console.Clear(); // so we can do stuff
            

            ShellCore.ShlHandleCmdArguments(args); // universal arguments (config - user name...)
            // SHELL MAIN ARGUMENTS
            string arg2 = "";
                if (args.Length > 0)
                {
                    for (int i = 0; i < args.Length; i++)
                    {
                        string thearg = args[i];
                        if (args.Length - i > 0)
                        {
                        arg2 = args[i + 1];
                        }
                        switch (thearg)
                        {
                            default:
                                ShellUI.ShXML.XmlParseScript(args[0]);
                                Console.WriteLine("Script has completed. Press enter to exit.");
                                Console.ReadKey();
                                ShellCore.Exit(0);
                                return null;
                            case "installmodule":
                                ShellModule.InstallModule(false, arg2);
                                Console.WriteLine("Module successfully installed. Press any key to exit Shell.");
                                Console.ReadKey();
                                return true;

                        }

                    }

                }

            if (ShellCore == null || ShellUI == null)
            {
                return null;
            }

            Console.WriteLine("Shell Development Release");
            Console.WriteLine("DLL Versions:");

            FileVersionInfo shellcore_ver = ShellCore.GetVersion();
            FileVersionInfo shellui_ver = ShellUI.GetVersion();
            FileVersionInfo shellmodule_ver = ShellModule.GetVersion();

            ShellCore.ShlSetWindowTitle($"Shell - core ver {shellcore_ver.FileMajorPart}.{shellcore_ver.FileMinorPart}.{shellcore_ver.FileBuildPart}.{shellcore_ver.FilePrivatePart}.");

            Console.WriteLine($"ShellCore Version: {shellcore_ver.FileMajorPart}.{shellcore_ver.FileMinorPart}.{shellcore_ver.FileBuildPart}.{shellcore_ver.FilePrivatePart}");
            Console.WriteLine($"ShellUI Version: {shellui_ver.FileMajorPart}.{shellui_ver.FileMinorPart}.{shellui_ver.FileBuildPart}.{shellui_ver.FilePrivatePart}\n");
            Console.WriteLine($"ShellModule Version: {shellmodule_ver.FileMajorPart}.{shellmodule_ver.FileMinorPart}.{shellmodule_ver.FileBuildPart}.{shellmodule_ver.FilePrivatePart}\n");

            ShellUI.ShXML.XmlParseScript("Whatsyourname.xml");

            Console.WriteLine("DEBUG: Variables created");

            foreach (ShxmlVariable ShxmlVar in ShellUI.ShXML.Varlist)
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
                        Console.WriteLine($"Variable: Name = {ShxmlVar.Name}. Value = {ShxmlVar.vardouble}. Type = Double.");
                        continue;
                    case 4:
                        Console.WriteLine($"Variable: Name = {ShxmlVar.Name}. Value = {ShxmlVar.varfloat}. Type = Float.");
                        continue;
                    case 5:
                        Console.WriteLine($"Variable: Name = {ShxmlVar.Name}. Value = {ShxmlVar.varbool}. Type = Boolean.");
                        continue;

                }

            }
            
            Console.ReadKey();
            return true;

        }

    }
}
