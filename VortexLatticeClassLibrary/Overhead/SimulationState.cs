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
            const double wingSpan = 7;
            const double chord = 1;
            const int numOfTilesSpanwise = 3;
            const int numOfPointsChordwise = 5;
            Vector vInfinity = new Vector(new double[] { 100, 0, 0 });
            List<List<double>> camberLine = AirfoilGeometryApproximator.GetCamberLine(args.Coordinates, numOfPointsChordwise);
            WingTile[] wingTiles = AirfoilGeometryApproximator.GetWingTiles(camberLine, chord, wingSpan, numOfTilesSpanwise);
            Matrix<double> matrix = Aerodynamics.ConstructAICCirculationEquationMatrix(wingTiles, vInfinity);
            Matrix<double>.SolveWithGaussianElimination(matrix);

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
            Console.WriteLine("-------------------------  Matrix  -----------------------------");
            foreach (double[] row in matrix.Elements)
            {
                foreach (double i in row)
                {
                    Console.Write($"{i}==");
                }
                Console.WriteLine("\n\n");
            }
        }
    }
}
