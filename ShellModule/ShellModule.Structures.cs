using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell.Module
{
    public enum Extends { ShellCore, ShellUI } // Additional DLLs may be added in the future
    public struct Module
    {
        public string Name { get; set; } // The name of the module.
        public string Copyright { get; set; } // The copyright information of the module
        public string Version { get; set; } // The version of the module.
        public string Author { get; set; } // The author of the module.
        public string Website { get; set; } // The website of the module.
        public string Dll { get; set; } // The module DLL
        public Extends Extends { get; set; } // What Shell DLL the module extends.

    }
}
