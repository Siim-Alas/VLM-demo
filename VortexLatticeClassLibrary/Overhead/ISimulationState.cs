﻿using System;
using System.Collections.Generic;
using System.Text;
using VortexLatticeClassLibrary.IO;

namespace VortexLatticeClassLibrary.Overhead
{
    public interface ISimulationState
    {
        InputManager InputManager { get; }
    }
}
