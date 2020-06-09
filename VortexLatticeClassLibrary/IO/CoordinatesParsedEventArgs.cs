using System;
using System.Collections.Generic;
using System.Text;

namespace VortexLatticeClassLibrary.IO
{
    public class CoordinatesParsedEventArgs : EventArgs
    {
        public CoordinatesParsedEventArgs(List<List<double>> coordinates)
        {
            Coordinates = coordinates;
        }
        public List<List<double>> Coordinates { get; private set; }
    }
}
