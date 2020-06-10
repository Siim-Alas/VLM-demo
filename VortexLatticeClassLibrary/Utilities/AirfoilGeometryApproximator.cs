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
        /// <param name="points">2-dimensional list of airfoil coordiantes sorted by x-value.</param>
        /// <returns>2-dimensional list of points on the camber line.</returns>
        public static List<List<double>> GetCamberLine(List<List<double>> points)
        {
            // The equation of a line defined by 2 points is
            // y = x * (y_2 - y_1)/(x_2 - x_1) + y_1 - x_1 * (y_2 - y_1)/(x_2 - x_1)

            // Expected format is sorted by x-values

            List<List<double>> camberLinePoints = new List<List<double>>();

            for (int i = 0; i < points.Count - 1; i += 2)
            {
                camberLinePoints.Add(new List<double>() { 
                    (points[i][0] + points[i+1][0]) / 2,
                    (points[i][1] + points[i+1][1]) / 2
                });
            }

            return camberLinePoints;
        }
    }
}
