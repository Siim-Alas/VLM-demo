using System;
using System.Collections.Generic;
using System.Text;

namespace VortexLatticeClassLibrary.IO
{
    public class CoordinatesParsedEventArgs : EventArgs
    {
        public CoordinatesParsedEventArgs(double[,] coordinates)
        {
            Coordinates = coordinates;
        }
        public double[,] Coordinates { get; private set; }
    }
}
