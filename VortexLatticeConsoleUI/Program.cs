using System;
using System.Globalization;
using System.IO;
using System.Linq;
using VortexLatticeClassLibrary.IO;
using VortexLatticeClassLibrary.Overhead;

namespace VortexLatticeConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-------------------------  Airfoil file  -----------------------------");

            SimulationState simState = new SimulationState();
            simState.InputManager.ParseAirfoilDatFile("C:/Users/Siim/Desktop/goe571.dat");

            Console.ReadLine();
        }
    }
}
