using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VortexLatticeClassLibrary.Overhead;
using VortexLatticeClassLibrary.Utilities;

namespace VortexLatticeClassLibrary.IO
{
    public class IOManager
    {
        public delegate void CoordinatesParsedEventHandler(object source, CoordinatesParsedEventArgs args);
        public event CoordinatesParsedEventHandler CoordinatesParsed;

        public IOManager()
        {
            Coordinates = new double[0, 0];
            CamberLine = new double[0, 0];
            WingTiles = new WingTile[] { };
            Forces = new Vector[] { };
        }

        public double[,] Coordinates { get; private set; }
        public double[,] CamberLine { get; private set; }
        public WingTile[] WingTiles { get; private set; }
        public Vector[] Forces { get; private set; }

        /// <summary>
        /// Parses airfoil datapoints from file and invokes an event with the datapoints (sorted by x) as args.
        /// </summary>
        /// <param name="file">The string representing the airfoil .dat file.</param>
        public void ParseAirfoilDatFile(string file,
                                        double? wingSpan,
                                        double? chord,
                                        int? numberOfTilesSpanwise,
                                        int? numberOfTilesChordwise,
                                        double? rho,
                                        double? magnitudeOfVInfinity,
                                        double? aoa,
                                        double? aoy
                                        )
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

            // Example of format 2 (Selig, the one this uses)

            //  1.00000  0.00000
            //  0.51893  0.07317
            //  0.10256  0.05432
            //  0.94977 - 0.00691
            //  1.00000  0.00000


            //MatchCollection matches;

            List<List<double>> coordinates = new List<List<double>>();

            using (var reader = new StringReader(file))
            {
                Regex rx = new Regex(@"\b *([ 01]*\.[0-9]+)[ ]+([ 0-]*\.[0-9]+) *.*$");
                Match match;
                string line;
                while ((line = reader.ReadLine()) != null)
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

                //matches = rx.Matches(reader.ReadToEnd());
                //Coordinates = new double[matches.Count, 2];
                //for (int i = 0; i < matches.Count; i++)
                //{
                //    Coordinates[i, 0] = Convert.ToDouble(matches[i].Groups[1].Value, CultureInfo.InvariantCulture);
                //    Coordinates[i, 1] = Convert.ToDouble(matches[i].Groups[2].Value, CultureInfo.InvariantCulture);
                //}
            }
            Coordinates = new double[coordinates.Count, 2];
            for (int i = 0; i < coordinates.Count; i++)
            {
                Coordinates[i, 0] = coordinates[i][0];
                Coordinates[i, 1] = coordinates[i][1];
            }

            // Coordinates are ordered by x-values
            CoordinatesParsed?.Invoke(this, new CoordinatesParsedEventArgs(Coordinates, 
                                                                           wingSpan, 
                                                                           chord, 
                                                                           numberOfTilesSpanwise, 
                                                                           numberOfTilesChordwise, 
                                                                           rho, 
                                                                           magnitudeOfVInfinity, 
                                                                           aoa, 
                                                                           aoy
                                                                           ));
        }
        public void OnSimulationComplete(object source, SimulationCompleteEventArgs args)
        {
            CamberLine = args.CamberLine;
            WingTiles = args.WingTiles;
            Forces = args.Forces;
        }
    }
}
