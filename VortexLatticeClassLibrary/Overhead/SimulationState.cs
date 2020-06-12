using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VortexLatticeClassLibrary.IO;
using VortexLatticeClassLibrary.Utilities;

namespace VortexLatticeClassLibrary.Overhead
{
    public class SimulationState : ISimulationState
    {
        public delegate void SimulationCompleteEventHandler(object source, SimulationCompleteEventArgs args);
        public event SimulationCompleteEventHandler SimulationComplete;
        public SimulationState()
        {
            IOManager = new IOManager();
            IOManager.CoordinatesParsed += OnCoordinatesParsed;
            SimulationComplete += IOManager.OnSimulationComplete;
        }

        public IOManager IOManager { get; private set; }

        private void OnCoordinatesParsed(object source, CoordinatesParsedEventArgs args)
        {
            Vector vInfinity = new Vector(new double[] { 
                Math.Cos(args.AOA) * Math.Cos(args.AOY) * args.MagnitudeOfVInfinity,
                -Math.Sin(args.AOY) * args.MagnitudeOfVInfinity, 
                Math.Sin(args.AOA) * Math.Cos(args.AOY) * args.MagnitudeOfVInfinity 
            });

            // Parse the airfoil file for the 2-dimensional coordinates of the camber line
            double[,] camberLine = AirfoilGeometryApproximator.GetCamberLine(args.Coordinates, args.NumberOfTilesChordwise + 1);
            
            // Generate an array of wing tiles
            WingTile[] wingTiles = AirfoilGeometryApproximator.GetWingTiles(camberLine, args.Chord, args.WingSpan, args.NumberOfTilesSpanwise);
            
            // Generate a matrix with the aerodynamic linear equations
            Matrix<double> EquationMatrix = Aerodynamics.ConstructAICCirculationEquationMatrix(wingTiles, vInfinity);
            
            // Solve said equations for gammas (vorticities) with Gaussian elimination
            double[] gammas = Matrix<double>.SolveWithGaussianElimination(EquationMatrix);

            // Get the total aerodynamic reaction.
            Vector[] forces = Aerodynamics.GetForces(wingTiles, vInfinity, gammas, args.Rho);
            Vector totalForce = new Vector(new double[] { 0, 0, 0 });
            foreach (Vector f in forces)
            {
                totalForce += f;
            }
            
            // Get the magnitude of the lift force.
            double lift = Aerodynamics.GetLift(totalForce);
            
            // Get the coefficient of lift.
            double CL = Aerodynamics.GetCL(lift, vInfinity, args.WingSpan * args.Chord, args.Rho);

            // Display
            Console.WriteLine("-------------------------  Data  -----------------------------");
            Console.WriteLine($"CL = {CL}");

            SimulationComplete?.Invoke(this, new SimulationCompleteEventArgs(camberLine, wingTiles, forces));
        }
    }
}
