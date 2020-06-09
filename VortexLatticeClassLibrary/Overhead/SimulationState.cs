using System;
using System.Collections.Generic;
using System.Text;
using VortexLatticeClassLibrary.IO;

namespace VortexLatticeClassLibrary.Overhead
{
    public class SimulationState
    {
        public SimulationState()
        {
            InputManager = new InputManager();
            InputManager.CoordinatesParsed += OnCoordinatesParsed;
        }

        public InputManager InputManager { get; private set; }

        private void OnCoordinatesParsed(object source, CoordinatesParsedEventArgs args)
        {
            foreach (List<double> pos in args.Coordinates)
            {
                Console.WriteLine($"x = {pos[0]}; y = {pos[1]}");
            }
        }
    }
}
