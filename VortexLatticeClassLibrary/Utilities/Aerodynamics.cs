using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;

namespace VortexLatticeClassLibrary.Utilities
{
    public static class Aerodynamics
    {
        /// <summary>
        /// Compute a vector indicating in which direction a given horse-shoe vortex is contributing velocity.
        /// </summary>
        /// <param name="r">Position vector of the point being influenced.</param>
        /// <param name="ra">Position vector of the forward-left vertex of the horse-shoe vortex.</param>
        /// <param name="rb">Position vector of the forward-right vertex of the horse-shoe vortex.</param>
        /// <returns>The vector indicating in which direction a given horse-shoe vortex is contributing velocity.</returns>
        public static Vector VHat(Vector r, Vector ra, Vector rb)
        {
            Vector xHat = new Vector(new double[] { 1, 0, 0 });
            Vector a = r - ra;
            Vector b = r - rb;
            return (Vector.Cross(a, b)*(1/a.Mag + 1/b.Mag)/(a.Mag*b.Mag + a*b) + Vector.Cross(a,xHat)/((a.Mag-a*xHat)*a.Mag) - Vector.Cross(b,xHat)/((b.Mag-b*xHat)*b.Mag)) / (4*Math.PI);
        }
        /// <summary>
        /// Constructs a set of linear equations (in matrix form) for vorticities.
        /// </summary>
        /// <param name="wingTiles">An array of wing tiles.</param>
        /// <param name="vInfinity">The far-field velocity.</param>
        /// <returns>An n*(n+1) matrix representing a set of n linear equations for n gammas.</returns>
        public static Matrix<double> ConstructAICCirculationEquationMatrix(WingTile[] wingTiles, Vector vInfinity)
        {
            double[][] elements = new double[wingTiles.Length][];
            for (int i = 0; i < wingTiles.Length; i++)
            {
                elements[i] = new double[wingTiles.Length + 1];
                for (int j = 0; j < wingTiles.Length; j++)
                {
                    // A_ij = Vhat_j(rc_i) * n_i
                    elements[i][j] = VHat(wingTiles[i].N, wingTiles[j].RA, wingTiles[j].RB) * wingTiles[i].N;
                }
                // b_i = V_infinity * n_i
                elements[i][wingTiles.Length] = vInfinity * wingTiles[i].N;
            }
            return new Matrix<double>(elements);
        }
    }
}
