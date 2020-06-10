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
            const double wingSpan = 10;
            const double chord = 1;
            const int numOfTilesSpanwise = 10;
            const int numOfTilesChordwise = 50;
            List<List<double>> camberLine = AirfoilGeometryApproximator.GetCamberLine(args.Coordinates, numOfTilesChordwise);

            // Display
            Console.WriteLine("-------------------------  Coordinates  -----------------------------");
            foreach (List<double> pos in args.Coordinates)
            {
                Console.WriteLine($"x = {pos[0]}; y = {pos[1]}");
            }
            Console.WriteLine("-------------------------  Camber line  -----------------------------");
            foreach (List<double> pos in camberLine)
            {
                Console.WriteLine($"x = {pos[0]}; y = {pos[1]}");
            }
        }
    }
}
