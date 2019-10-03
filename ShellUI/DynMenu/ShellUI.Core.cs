//using Shell;
using Shell.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Shell.UI // Dynamic Text UI
{

    public partial class ShellUI : IShell  // Internal only.
    {
        public ShellCore ShellCore { get; set; }

        internal enum Days { Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday };

        public ShellUI()
        {
            ShellCore = new ShellCore();
            ShellCore.ElmInitExceptionManager();
            
        }

        public void InitShellUI()
        {

            this.ShXML = new ShellXML();
            ShXML.OperandList = new List<string>();
            ShXML.OpList = new List<char>();

        }

        public FileVersionInfo GetVersion()
        {
            Assembly ShlUi = Assembly.GetExecutingAssembly();
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(ShlUi.Location);
            return fileVersionInfo;
        }

        public void PrintVersion() // 0.5.3+ (modified in v0.5 build 11)
        {
            FileVersionInfo version = GetVersion();
            Console.WriteLine(version.FileVersion);
        }

        // just makes it quicker
        public void UiDrawBlankSpace(int amount)
        {
            UiDrawMultiple(' ', amount);
        }

        public void UiDrawMultiple(char Character, int amount)
        {
            int i = 0;

            if (amount < 0)
            {
                ShellCore.ElmThrowException(46);
            }
            for (i = 0; i < amount; i++)
            {
                Console.Write(Character);
            }

            return;
        }
         
        public void UiSetCursorPosition(int x, int y)
        {
            if (x < 0 | y < 0)
            {
                ShellCore.ElmThrowException(46);
            }
            Console.CursorLeft = x;
            Console.CursorTop = y; // sets the console position
        }

        
        public bool UiSetConsoleColour(ConsoleColor foregroundColour, ConsoleColor backgroundColour)
        {
            ConsoleColor[] Colours = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor)); // enumerate through everything

            foreach (ConsoleColor colour in Colours)
            {

                Console.WriteLine(colour);

                if (colour == foregroundColour)
                {
                    Console.ForegroundColor = colour;
                }

                if (colour == backgroundColour)
                {
                    Console.BackgroundColor = colour;
                }

            }

            return true;
       
        }

        public void UiSetCursorSize(int height)
        {
            if (height < 0)
            {
                ShellCore.ElmThrowException(46);
            }
            Console.CursorSize = height;
            return;
        }

        public void UiSetCursorVisibility(bool visible)
        {
            Console.CursorVisible = visible;
            return;
        }

        public void UiSetWindowPosition(int x, int y)
        {
            if (x < 0 | y < 0)
            {
                ShellCore.ElmThrowException(46);
            }
            else if (x + Console.WindowWidth > Console.BufferWidth | y + Console.WindowHeight > Console.BufferHeight)
            {
                ShellCore.ElmThrowException(47);
            }

            Console.WindowLeft = x;
            Console.WindowTop = y;
        }

        public void UiSetWindowTitle(string title)
        {

            try
            {
                Console.Title = title;
            }
            catch (ArgumentException)
            {
                ShellCore.ElmThrowException(49);
            }
            

        }

        public void UiSetWindowSize(int x, int y)
        {
            if (x < 0 | y < 0)
            {
                ShellCore.ElmThrowException(46);
            }
            else if (x > Console.BufferWidth | y > Console.BufferHeight)
            {
                ShellCore.ElmThrowException(47);
            }
            else if (y > 63)
            {
                ShellCore.ElmThrowException(48);
            }
            Console.WindowWidth = x;
            Console.WindowHeight = y;
        }


    }
    
}
