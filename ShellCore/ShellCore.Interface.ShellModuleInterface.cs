﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell.Core
{
    public interface IShellModule
    {
        void InitModule(); // initmodule
        List<int> GetVersion();

    }
}
