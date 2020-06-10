using System;
using System.Collections.Generic;
using System.Text;

namespace VortexLatticeClassLibrary.Utilities
{
    /// <summary>
    /// A rectangular tile on a wing, defined by 3 position vectors.
    /// </summary>
    public struct WingTile
    {
        /// <summary>
        /// Creates a new rectangular wing tile, defined by 3 position vectors.
        /// </summary>
        /// <param name="forwardLeft">The forward-left vertex position vector.</param>
        /// <param name="forwardRight">The forward-right vertex position vector.</param>
        /// <param name="backRight">The back-left vertex position vector.</param>
        public WingTile(Vector forwardLeft, Vector forwardRight, Vector backRight)
        {
            FL = forwardLeft;
            FR = forwardRight;
            BL = backRight;
        }
        /// <summary>
        /// The forward-left vertex position vector.
        /// </summary>
        public Vector FL { get; private set; }
        /// <summary>
        /// The forward-right vertex position vector.
        /// </summary>
        public Vector FR { get; private set; }
        /// <summary>
        /// The back-left vertex position vector.
        /// </summary>
        public Vector BL { get; private set; }
        /// <summary>
        /// The normal vector to the tile.
        /// </summary>
        public Vector N { get { return Vector.Cross(BL - FL, FR - FL); } }
        /// <summary>
        /// The position-vector of the forward-left vertex of the horse-shoe vortex (1/4 from the front).
        /// </summary>
        public Vector RA { get { return FL + 0.25 * (BL - FL); } }
        /// <summary>
        /// The position-vector of the forward-left vertex of the horse-shoe vortex (1/4 from the front).
        /// </summary>
        public Vector RB { get { return FR + 0.25 * (BL - FL); } }
        /// <summary>
        /// The position-vector of the control point (3/4 from the front).
        /// </summary>
        public Vector RC { get { return 0.25 * FR + 0.5 * FL + 0.75 * BL; } }
    }
}
