﻿using System;
using System.Collections.Generic;
using System.Text;
using VortexLatticeClassLibrary.Utilities;

namespace VortexLatticeClassLibrary.Overhead
{
    public class SimulationCompleteEventArgs : EventArgs
    {
        public SimulationCompleteEventArgs(List<List<double>> camberLine, WingTile[] wingTiles, Vector[] forces)
        {
            CamberLine = camberLine;
            WingTiles = wingTiles;
            Forces = forces;
        }
        public List<List<double>> CamberLine { get; private set; }
        public WingTile[] WingTiles { get; private set; }
        public Vector[] Forces { get; private set; }

    }
}