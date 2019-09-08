//using Shell;
using Shell.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace Shell.UI // Dynamic Text UI
{

    public partial class ShellUI : IShell  // Internal only.
    {
        public ShellCore ShellCore { get; set; }

        internal enum Days { Sunday, Monday, Tuesday, Wednesday, Thursday, Friday };

        public ShellXML ShellXML { get; set; }

        public ShellUI()
        {
            ShellCore = new ShellCore();
            ShellCore.ElmInitExceptionManager();
           
        }

        public List<int> GetVersion()
        {
            List<int> Version = new List<int> { 0, 4, 5, 0 };

            return Version;

        }
        public void UiDrawMultiple(char Character, int amount)
        {
            int i = 0;

            for (i = 0; i < amount; i++)
            {
                Console.Write(Character);

            }
            return;
        }

        public void UiDrawBlankSpace(int amount)
        {
            UiDrawMultiple(' ', amount);
        }

        // just makes it quicker
        public int UiSetConsoleColour(ConsoleColor foregroundColour, ConsoleColor backgroundColour)
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

            return 0;
       
        }



    }
    
}
