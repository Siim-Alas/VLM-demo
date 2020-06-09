using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VortexLatticeClassLibrary.Utilities
{
    public static class AirfoilGeometryApproximator
    {
        public static void GetCamberLine(List<List<double>> points)
        {
            // The equation of a line defined by 2 points is

            // y = x * (y_2 - y_1)/(x_2 - x_1) + y_1 - x_1 * (y_2 - y_1)/(x_2 - x_1)

            if (points.Where(p => p[0] == 0).Count() == 2)
            {
                // Format

                //  0.0000000 0.0000000
                //  0.0010700 -.0175200
                //
                //  0.0000000 0.0000000
                //  0.0010700 -.0175200

                for (int i = 0; i < points.Count / 2; i++)
                {

                }
            }
            else
            {
                // Continuous loop around airfoil

                for (int i = 0; i < points.Count / 2; i++)
                {

                }
            }
        }
    }
}
