using Shell.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell.Core
{
    public class ShellExtension : IShellModule
    {
        public void InitModule()
        {
            return;
        }

        public List<int> GetVersion()
        {
            return new List<int> { 1, 0, 0, 0 };
        }
    }
}
