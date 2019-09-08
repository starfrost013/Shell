using Shell.Core;
//using Shell.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    public interface IShell
    {
        ShellCore ShellCore { get; set; }
    }
}
