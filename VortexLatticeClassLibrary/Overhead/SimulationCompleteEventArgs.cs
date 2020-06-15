using System;
using System.Collections.Generic;
using System.Text;
using VortexLatticeClassLibrary.Utilities;

namespace VortexLatticeClassLibrary.Overhead
{
    public class SimulationCompleteEventArgs : EventArgs
    {
        public SimulationCompleteEventArgs(double[,] camberLine, 
                                           WingTile[] wingTiles, 
                                           Vector[] forces,
                                           double cl,
                                           double cdi,
                                           double cm)
        {
            CamberLine = camberLine;
            WingTiles = wingTiles;
            Forces = forces;
            CL = cl;
            CDI = cdi;
            CM = cm;
        }
        public double[,] CamberLine { get; private set; }
        public WingTile[] WingTiles { get; private set; }
        public Vector[] Forces { get; private set; }
        public double CL { get; private set; }
        public double CDI { get; private set; }
        public double CM { get; private set; }
    }
}
