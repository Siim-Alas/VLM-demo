using System;
using System.Collections.Generic;
using System.Text;

namespace VortexLatticeClassLibrary.IO
{
    public class CoordinatesParsedEventArgs : EventArgs
    {
        public CoordinatesParsedEventArgs(double[,] coordinates, 
                                          double? wingSpan = 7, 
                                          double? chord = 1,
                                          int? numberOfTilesSpanwise = 10,
                                          int? numberOfTilesChordwise = 20,
                                          double? rho = 1.225,
                                          double? magnitudeOfVInfinity = 30,
                                          double? aoa = 0,
                                          double? aoy = 0)
        {
            Coordinates = coordinates;
            WingSpan = wingSpan ?? 7;
            Chord = chord ?? 1;
            NumberOfTilesSpanwise = numberOfTilesSpanwise ?? 10;
            NumberOfTilesChordwise = numberOfTilesChordwise ?? 20;
            Rho = rho ?? 1.225;
            MagnitudeOfVInfinity = magnitudeOfVInfinity ?? 30;
            AOA = aoa ?? 0;
            AOY = aoy ?? 0;
        }
        public double[,] Coordinates { get; private set; }
        public double WingSpan { get; private set; }
        public double Chord { get; private set; }
        public int NumberOfTilesSpanwise { get; private set; }
        public int NumberOfTilesChordwise { get; private set; }
        public double Rho { get; private set; }
        public double MagnitudeOfVInfinity { get; private set; }
        public double AOA { get; private set; }
        public double AOY { get; private set; }
    }
}
