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
                    elements[i][j] = VHat(wingTiles[i].RC, wingTiles[j].RA, wingTiles[j].RB) * wingTiles[i].N;
                }
                // b_i = - V_infinity * n_i
                elements[i][wingTiles.Length] = -vInfinity * wingTiles[i].N;
            }
            return new Matrix<double>(elements);
        }
        /// <summary>
        /// Gets the force vectors exerted by an array of wing tiles.
        /// </summary>
        /// <param name="wingTiles">The array of wing tiles exerting the force.</param>
        /// <param name="vInfinity">The far-field velocity.</param>
        /// <param name="gammas">The strength of the horse-shoe vortices on each tile.</param>
        /// <param name="rho">The density of the air.</param>
        /// <returns>The force vectors exerted by the array of wing tiles.</returns>
        public static Vector[] GetForces(WingTile[] wingTiles, Vector vInfinity, double[] gammas, double rho)
        {
            Vector[] forces = new Vector[wingTiles.Length];
            for (int i = 0; i < wingTiles.Length; i++)
            {
                forces[i] = rho * gammas[i] * Vector.Cross(vInfinity + VHat(wingTiles[i].R, wingTiles[i].RA, wingTiles[i].RB), (wingTiles[i].RB - wingTiles[i].RA) / 2);
            }
            return forces;
        }
        /// <summary>
        /// Gets the lift(z)-component of a force.
        /// </summary>
        /// <param name="totalForce">The total force vector.</param>
        /// <returns>The lift(z)-component of a force.</returns>
        public static double GetLift(Vector totalForce)
        {
            return totalForce * new Vector(new double[] { 0, 0, 1 });
        }
        /// <summary>
        /// Gets the coefficient of lift.
        /// </summary>
        /// <param name="lift">The magnitude of the lift-force.</param>
        /// <param name="vInfinity">The far-field velocity.</param>
        /// <param name="S">The reference area of the wing.</param>
        /// <param name="rho">The density of the air.</param>
        /// <returns>The coefficient of lift.</returns>
        public static double GetCL(double lift, Vector vInfinity, double S, double rho)
        {
            return 2 * lift / (rho * Math.Pow(vInfinity.Mag, 2) * S);
        }
    }
}
