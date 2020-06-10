using System;
using System.Collections.Generic;
using System.Text;
using VortexLatticeClassLibrary.IO;
using VortexLatticeClassLibrary.Utilities;

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
            Console.WriteLine("-------------------------  Coordinates  -----------------------------");
            foreach (List<double> pos in args.Coordinates)
            {
                Console.WriteLine($"x = {pos[0]}; y = {pos[1]}");
            }
            Console.WriteLine("-------------------------  Camber line  -----------------------------");
            foreach (List<double> pos in AirfoilGeometryApproximator.GetCamberLine(args.Coordinates))
            {
                Console.WriteLine($"x = {pos[0]}; y = {pos[1]}");
            }
        }
    }
}
