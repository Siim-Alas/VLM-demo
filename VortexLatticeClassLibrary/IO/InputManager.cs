using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VortexLatticeClassLibrary.Overhead;

namespace VortexLatticeClassLibrary.IO
{
    public class InputManager
    {
        public delegate void CoordinatesParsedEventHandler(object source, CoordinatesParsedEventArgs args);
        public event CoordinatesParsedEventHandler CoordinatesParsed;
        
        /// <summary>
        /// Parses airfoil datapoints from file and invokes an event with the datapoints (sorted by x) as args.
        /// </summary>
        /// <param name="file">The string representing the airfoil .dat file.</param>
        public async Task ParseAirfoilDatFile(string file)
        {
            // Airfoil .dat files are in the following formats
            // Newlines are \r\n
            // Header is optional
            // Last line always has data

            // Example of format 1

            //  0.0000000 0.0000000
            //  0.0010700 -.0175200
            //  1.0000000  0.0000000
            //
            //  0.0000000 0.0000000
            //  0.0010700 -.0175200
            //  1.0000000  0.0000000

            // Example of format 2

            //  1.00000  0.00000
            //  0.51893  0.07317
            //  0.10256  0.05432
            //  0.94977 - 0.00691
            //  1.00000  0.00000

            List<List<double>> coordinates = new List<List<double>>();

            Regex rx = new Regex(@"\b *([ 01]*\.[0-9]+)[ ]+([ 0-]*\.[0-9]+) *.*$");
            Match match;

            using (var reader = new StringReader(file))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
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
            }

            // Coordinates are ordered by x-values
            CoordinatesParsed?.Invoke(this, new CoordinatesParsedEventArgs(coordinates.OrderBy(c => c[0]).ToList()));
        }
    }
}
