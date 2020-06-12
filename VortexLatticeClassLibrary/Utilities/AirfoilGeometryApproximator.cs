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
        /// <param name="points">2-dimensional list of airfoil coordiantes, in Selig format.</param>
        /// <param name="n">Number of points to be returned.</param>
        /// <returns>2-dimensional list of points on the camber line, front-to-back.</returns>
        public static double[,] GetCamberLine(double[,] points, int n = 100)
        {
            // dp is the set of points defining the camber line, shortened for brevity in expressions
            double[,] dp = new double[points.GetLength(0), 2];
            double[,] outputPoints = new double[n, 2];
            int j = 1;
            double deltaPhi = Math.PI / (2 * n - 2);
            double xi;
            // Expected format is in Selig format, meaning a continuous loop around the airfoil
            for (int i = 0; i < points.GetLength(0) / 2; i++)
            {
                dp[i, 0] = (points[i, 0] + points[points.GetLength(0) - i - 1, 0]) / 2;
                dp[i, 1] = (points[i, 1] + points[points.GetLength(0) - i - 1, 1]) / 2;
            }
            // Values tabulated using the cosine scheme
            // delta_phi = pi / 2(n - 1)
            // x_i = 1 - cos(i * delta_phi) ; i = 0, 1, ..., n - 1
            for (int i = n - 1; i > -1; i--)
            {
                xi = 1 - Math.Cos(i * deltaPhi);
                while (dp[j, 0] > xi)
                {
                    j++;
                }
                // xi is in (camberLinePoints[j-1][0]; camberLinePoints[j][0]]
                // The equation of a line defined by 2 points is
                // y = x * (y_2 - y_1)/(x_2 - x_1) + y_1 - x_1 * (y_2 - y_1)/(x_2 - x_1)
                outputPoints[i, 0] = xi;
                outputPoints[i, 1] = xi * (dp[j, 1] - dp[j - 1, 1]) / (dp[j, 0] - dp[j - 1, 0]) + dp[j - 1, 1] - dp[j - 1, 0] * (dp[j, 1] - dp[j - 1, 1]) / (dp[j, 0] - dp[j - 1, 0]);
            }
            return outputPoints;
        }
        /// <summary>
        /// Generates an array of wingtiles.
        /// </summary>
        /// <param name="camberLine">The camber line represented by a 2-dimensional array of points.</param>
        /// <param name="chord">The wing chord length.</param>
        /// <param name="wingSpan">The wingspan.</param>
        /// <param name="numOfTilesSpanwise">Number of tiles spanwise.</param>
        /// <returns>The generated array of wingtiles.</returns>
        public static WingTile[] GetWingTiles(double[,] camberLine, double chord, double wingSpan, int numOfTilesSpanwise)
        {
            double tileWidth = wingSpan / numOfTilesSpanwise;
            WingTile[] wingTiles = new WingTile[numOfTilesSpanwise * (camberLine.GetLength(0) - 1)];
            int index = 0;

            for (int i = 0; i < numOfTilesSpanwise; i++)
            {
                // camberLine already has the desired amount of points
                for (int j = 0; j < camberLine.GetLength(0) - 1; j++)
                {
                    // x - chordwise (backwards)
                    // y - spanwise (to the right)
                    // z - normal (upwards)

                    wingTiles[index++] = new WingTile(new Vector(new double[] { 
                                                   chord * camberLine[j, 0],
                                                   i * tileWidth,
                                                   chord * camberLine[j, 1]
                                                   }), 
                                                   new Vector(new double[] {
                                                   chord * camberLine[j, 0],
                                                   (i + 1) * tileWidth,
                                                   chord * camberLine[j, 1]
                                                   }),
                                                   new Vector(new double[] {
                                                   chord * camberLine[j + 1, 0],
                                                   i * tileWidth,
                                                   chord * camberLine[j + 1, 1]
                                                   })
                                                   );
                }
            }
            return wingTiles;
        }
    }
}
