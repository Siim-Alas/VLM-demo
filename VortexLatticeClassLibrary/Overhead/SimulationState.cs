using System;
using System.Collections.Generic;
using System.Text;
using VortexLatticeClassLibrary.IO;
using VortexLatticeClassLibrary.Utilities;

namespace VortexLatticeClassLibrary.Overhead
{
    public class SimulationState : ISimulationState
    {
        public SimulationState()
        {
            InputManager = new InputManager();
            InputManager.CoordinatesParsed += OnCoordinatesParsed;
        }

        public InputManager InputManager { get; private set; }

        private void OnCoordinatesParsed(object source, CoordinatesParsedEventArgs args)
        {
            const double wingSpan = 30;
            const double chord = 1;
            const int numOfTilesSpanwise = 10;
            const int numOfPointsChordwise = 10;
            const double rho = 1.225;
            Vector vInfinity = new Vector(new double[] { 10, 0, 0 });

            // Parse the airfoil file for the 2-dimensional coordinates of the camber line
            List<List<double>> camberLine = AirfoilGeometryApproximator.GetCamberLine(args.Coordinates, numOfPointsChordwise);
            
            // Generate an array of wing tiles
            WingTile[] wingTiles = AirfoilGeometryApproximator.GetWingTiles(camberLine, chord, wingSpan, numOfTilesSpanwise);
            
            // Generate a matrix with the aerodynamic linear equations
            Matrix<double> EquationMatrix = Aerodynamics.ConstructAICCirculationEquationMatrix(wingTiles, vInfinity);
            
            // Solve said equations for gammas (vorticities) with Gaussian elimination
            double[] gammas = Matrix<double>.SolveWithGaussianElimination(EquationMatrix);
            
            // Get the total aerodynamic reaction.
            Vector totalForce = Aerodynamics.GetTotalForce(wingTiles, vInfinity, gammas, rho);
            
            // Get the magnitude of the lift force.
            double lift = Aerodynamics.GetLift(totalForce);
            
            // Get the coefficient of lift.
            double CL = Aerodynamics.GetCL(lift, vInfinity, wingSpan * chord, rho);

            // Display
            Console.WriteLine("-------------------------  Coordinates  -----------------------------");
            foreach (List<double> pos in args.Coordinates)
            {
                Console.WriteLine($"x = {pos[0]}; y = {pos[1]}");
            }
            Console.WriteLine("-------------------------  Data  -----------------------------");
            Console.WriteLine($"CL = {CL}");
        }
    }
}
