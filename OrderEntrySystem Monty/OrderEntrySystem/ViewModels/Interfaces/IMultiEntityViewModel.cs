﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntrySystem
{
    public interface IMultiEntityViewModel
    {
        event EventHandler RequestClose;

        string DisplayName { get; }

        Type Type { get; }
    }
}
