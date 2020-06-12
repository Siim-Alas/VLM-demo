using System;
using System.Collections.Generic;
using System.Text;

namespace VortexLatticeClassLibrary.Overhead
{
    public class SimulationCompleteEventArgs : EventArgs
    {
        public SimulationCompleteEventArgs(List<List<double>> camberLine)
        {
            CamberLine = camberLine;
        }
        public List<List<double>> CamberLine { get; private set; }
    }
}
