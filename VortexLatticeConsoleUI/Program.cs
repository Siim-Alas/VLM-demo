using System;
using VortexLatticeClassLibrary.IO;

namespace VortexLatticeConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            InputManager.ParseAirfoilDatFile("C:/Users/Siim/Desktop/goe571.dat");
            Console.ReadLine();
        }
    }
}
