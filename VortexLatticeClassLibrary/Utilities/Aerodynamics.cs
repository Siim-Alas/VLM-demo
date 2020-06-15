using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;

namespace VortexLatticeClassLibrary.Utilities
{
    public static class Aerodynamics
    {
        /// <summary>
        /// Compute the Aerodynamic Influence Coefficienf matrix element w_ij, representing the influence a wing tile j is exerting on the location i.
        /// </summary>
        /// <param name="ri">The position vector of the point being influenced by tile j.</param>
        /// <param name="j">The tile whose horse-shoe vortex is influencing point i.</param>
        /// <returns>The vector indicating in which direction a given horse-shoe vortex is contributing velocity.</returns>
        public static Vector Wij(Vector ri, WingTile j)
        {
            Vector xHat = new Vector(new double[] { 1, 0, 0 });
            Vector a = ri - j.RA;
            Vector b = ri - j.RB;
            return (Vector.Cross(a, b)*(1/a.Mag + 1/b.Mag)/(a.Mag*b.Mag + a*b) + Vector.Cross(a,xHat)/((a.Mag-a*xHat)*a.Mag) - Vector.Cross(b,xHat)/((b.Mag-b*xHat)*b.Mag)) / (4*Math.PI);
        }
        /// <summary>
        /// Gets the perturbation velocity at a given point r.
        /// </summary>
        /// <param name="r">The position vector of the pint being influenced.</param>
        /// <param name="wingTiles">The array of wingtiles whose horse-shoe vortices are influencing point r.</param>
        /// <param name="gammas">An array representing the vorticities of the horse-shoe vortices influencing point r.</param>
        /// <returns>The perturbation velocity vector at the point r.</returns>
        public static Vector Vi(Vector r, WingTile[] wingTiles, double[] gammas)
        {
            Vector vi = new Vector(new double[] { 0, 0, 0 });
            for (int i = 0; i < wingTiles.Length; i++)
            {
                vi += Wij(r, wingTiles[i]) * gammas[i];
            }
            return vi;
        }
        /// <summary>
        /// Constructs a set of linear equations (in matrix form) for vorticities, using the normalwash matrix tangential flow condition.
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
                    // A_ij = W_i(rc_j) * n_i
                    elements[i][j] = Wij(wingTiles[i].RC, wingTiles[j]) * wingTiles[i].N;
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
                // v_i = perturbation velocity (at the tile's center) = Sum_1_to_N(w_ij * gamma_j)
                // l_i = vortex's transverse segment vector
                // F_i = rho * Gamma_i * Vector.Cross((V_infinity + v_i), l_i) 
                forces[i] = rho * gammas[i] * Vector.Cross(vInfinity + Vi(wingTiles[i].R, wingTiles, gammas), wingTiles[i].RB - wingTiles[i].RA);
            }
            return forces;
        }
        /// <summary>
        /// Gets the total force by adding individual forces.
        /// </summary>
        /// <param name="forces">The array of force-vectors o be summed.</param>
        /// <returns>The sum of the forces.</returns>
        public static Vector GetTotalForce(Vector[] forces)
        {
            Vector totalForce = new Vector(new double[] { 0, 0, 0 });
            foreach (Vector f in forces)
            {
                totalForce += f;
            }
            return totalForce;
        }
        /// <summary>
        /// Gets the total moment about a point r.
        /// </summary>
        /// <param name="r">The position-vector of the point about which the moment is being calculated.</param>
        /// <param name="forces">An array of forces, corresponding to the array of wing tiles.</param>
        /// <param name="wingTiles">An array of wing tiles, correcponding to the array of forces.</param>
        /// <returns>The total moment about point r.</returns>
        public static Vector GetTotalMoment(Vector r, Vector[] forces, WingTile[] wingTiles)
        {
            Vector totalMoment = new Vector(new double[] { 0, 0, 0 });
            for (int i = 0; i < forces.Length; i++)
            {
                // M_i = Vector.Cross(r_i, F_i)
                totalMoment += Vector.Cross(wingTiles[i].R - r, forces[i]);
            }
            return totalMoment;
        }
    }
}
