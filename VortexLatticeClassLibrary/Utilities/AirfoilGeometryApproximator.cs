using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VortexLatticeClassLibrary.Utilities
{
    public static class AirfoilGeometryApproximator
    {
        /// <summary>
        /// Approximates the camber line of an airfoil given a list of datapoints.
        /// </summary>
        /// <param name="points">2-dimensional list of airfoil coordiantes, sorted by x-value.</param>
        /// <param name="n">Number of points to be returned.</param>
        /// <returns>2-dimensional list of points on the camber line, sorted by x-value.</returns>
        public static List<List<double>> GetCamberLine(List<List<double>> points, int n = 100)
        {
            // dp is the set of points defining the camber line, shortened for brevity in expressions
            List<List<double>> dp = new List<List<double>>();
            List<List<double>> outputPoints = new List<List<double>>();
            int j = 0;
            double deltaPhi = Math.PI / (2 * n - 2);
            double xi;
            // Expected format is sorted by x-values, so camberLinePoints will also be sorted by x-value
            for (int i = 0; i < points.Count - 1; i += 2)
            {
                dp.Add(new List<double>() { 
                    (points[i][0] + points[i+1][0]) / 2,
                    (points[i][1] + points[i+1][1]) / 2
                });
            }
            // Values tabulated using the cosine scheme
            // delta_phi = pi / 2(n - 1)
            // x_i = 1 - cos(i * delta_phi) ; i = 0, 1, ..., n - 1
            for (int i = 0; i < n; i++)
            {
                xi = 1 - Math.Cos(i * deltaPhi);
                while (dp[j][0] < xi)
                {
                    j++;
                }
                j = (j < 1) ? 1 : j;
                // xi is in (camberLinePoints[j-1][0]; camberLinePoints[j][0]]
                // The equation of a line defined by 2 points is
                // y = x * (y_2 - y_1)/(x_2 - x_1) + y_1 - x_1 * (y_2 - y_1)/(x_2 - x_1)
                outputPoints.Add(new List<double>() { 
                    xi,
                    xi * (dp[j][1] - dp[j-1][1]) / (dp[j][0] - dp[j-1][0]) + dp[j-1][1] - dp[j-1][0] * (dp[j][1] - dp[j-1][1]) / (dp[j][0] - dp[j-1][0])
                });
            }
            return outputPoints;
        }

        public static WingTile[] GetWingTiles(List<List<double>> camberLine, double chord, double wingSpan, int numOfTilesSpanwise)
        {
            double tileWidth = wingSpan / numOfTilesSpanwise;
            WingTile[] wingTiles = new WingTile[numOfTilesSpanwise * (camberLine.Count - 1)];
            int index = 0;

            for (int i = 0; i < numOfTilesSpanwise; i++)
            {
                // camberLine already has the desired amount of points
                for (int j = 0; j < camberLine.Count - 1; j++)
                {
                    // x - chordwise (backwards)
                    // y - spanwise (to the right)
                    // z - normal (upwards)

                    wingTiles[index++] = new WingTile(new Vector(new double[] { 
                                                   chord * camberLine[j][0],
                                                   i * tileWidth,
                                                   chord * camberLine[j][1]
                                                   }), 
                                                   new Vector(new double[] {
                                                   chord * camberLine[j][0],
                                                   (i + 1) * tileWidth,
                                                   chord * camberLine[j][1]
                                                   }),
                                                   new Vector(new double[] {
                                                   chord * camberLine[j + 1][0],
                                                   i * tileWidth,
                                                   chord * camberLine[j + 1][1]
                                                   })
                                                   );
                }
            }
            return wingTiles;
        }
    }
}
