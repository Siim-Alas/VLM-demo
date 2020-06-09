using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using VortexLatticeClassLibrary.Overhead;

namespace VortexLatticeClassLibrary.IO
{
    public class InputManager
    {
        public delegate void CoordinatesParsedEventHandler(object source, CoordinatesParsedEventArgs args);
        public event CoordinatesParsedEventHandler CoordinatesParsed;
        
        public void ParseAirfoilDatFile(string path)
        {
            if (!File.Exists(path))
            {
                return;
            }

            // Airfoil .dat files are in the following format
            // Newlines are \r\n

            // Airfoil name
            //     2.    2.
            //
            //  0.0000000 0.0000000
            //  0.0010700 -.0175200
            //
            //  0.0000000 0.0000000
            //  0.0010700 -.0175200

            List<List<double>> coordinates = new List<List<double>>();

            Regex rx = new Regex(@"\b *([ 01]*\.[0-9]+)[ ]+([ 0-]*\.[0-9]+) *.*$");
            Match match;

            foreach (string line in File.ReadLines(path))
            {
                match = rx.Match(line);
                
                if (match.Success)
                {
                    coordinates.Add(new List<double>() { 
                        Convert.ToDouble(match.Groups[1].Value, CultureInfo.InvariantCulture),
                        Convert.ToDouble(match.Groups[2].Value, CultureInfo.InvariantCulture)
                    });
                }
            }

            CoordinatesParsed?.Invoke(this, new CoordinatesParsedEventArgs(coordinates));
        }
    }
}
