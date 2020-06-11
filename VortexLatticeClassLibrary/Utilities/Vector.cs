using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using VortexLatticeClassLibrary.IO;

namespace VortexLatticeClassLibrary.Utilities
{
    public readonly struct Vector
    {
        public Vector(double[] coordinates)
        {
            Coordinates = coordinates;
        }
        public double[] Coordinates { get; }
        /// <summary>
        /// Returns the dimension of the vector.
        /// </summary>
        public int Rank { get { return Coordinates.Length; } }
        /// <summary>
        /// Returns the squared magnitude of teh vecotr.
        /// </summary>
        public double MagS { get {
                double magnitudeSquared = 0;
                foreach (double c in Coordinates)
                {
                    magnitudeSquared += Math.Pow(c, 2);
                }
                return magnitudeSquared;
            } }
        /// <summary>
        /// Returns the magnitude of the vector.
        /// </summary>
        public double Mag { get { return Math.Sqrt(MagS); } }

        public static Vector operator+(Vector left, Vector right)
        {
            if (left.Rank != right.Rank)
            {
                throw new InvalidOperationException("Cannot add vectors of different dimesnion.");
            }

            double[] coordinates = new double[left.Coordinates.Length];
            for (int i = 0; i < coordinates.Length; i++) {
                coordinates[i] = left.Coordinates[i] + right.Coordinates[i];
            }
            return new Vector(coordinates);
        }
        public static Vector operator-(Vector left, Vector right)
        {
            if (left.Rank != right.Rank)
            {
                throw new InvalidOperationException("Cannot subtract vectors of different dimesnion.");
            }

            double[] coordinates = new double[left.Coordinates.Length];
            for (int i = 0; i < coordinates.Length; i++)
            {
                coordinates[i] = left.Coordinates[i] - right.Coordinates[i];
            }
            return new Vector(coordinates);
        }
        public static Vector operator-(Vector v)
        {
            double[] coordinates = new double[v.Coordinates.Length];
            for (int i = 0; i < coordinates.Length; i++)
            {
                coordinates[i] = -v.Coordinates[i];
            }
            return new Vector(coordinates);
        }
        public static Vector operator*(double c, Vector v)
        {
            double[] coordinates = new double[v.Coordinates.Length];
            for (int i = 0; i < coordinates.Length; i++)
            {
                coordinates[i] = c * v.Coordinates[i];
            }
            return new Vector(coordinates);
        }
        public static Vector operator*(Vector v, double c)
        {
            double[] coordinates = new double[v.Coordinates.Length];
            for (int i = 0; i < coordinates.Length; i++)
            {
                coordinates[i] = c * v.Coordinates[i];
            }
            return new Vector(coordinates);
        }
        public static Vector operator/(Vector v, double c)
        {
            double[] coordinates = new double[v.Coordinates.Length];
            for (int i = 0; i < coordinates.Length; i++)
            {
                coordinates[i] = v.Coordinates[i] / c;
            }
            return new Vector(coordinates);
        }
        public static double operator*(Vector left, Vector right)
        {
            if (left.Rank != right.Rank)
            {
                throw new InvalidOperationException("Cannot add vectors of different dimesnion.");
            }

            double dotProduct = 0;
            for (int i = 0; i < left.Coordinates.Length; i++)
            {
                dotProduct += left.Coordinates[i] * right.Coordinates[i];
            }
            return dotProduct;
        }
        public static Vector Cross(Vector left, Vector right)
        {
            if ((left.Rank != 3) || (right.Rank != 3))
            {
                throw new NotImplementedException("Cross product only implemented for 3-dimensional vectors");
            }
            return new Vector(new double[] { 
                (left.Coordinates[1] * right.Coordinates[2]) - (left.Coordinates[2] * right.Coordinates[1]),
                (left.Coordinates[2] * right.Coordinates[0]) - (left.Coordinates[0] * right.Coordinates[2]),
                (left.Coordinates[0] * right.Coordinates[1]) - (left.Coordinates[1] * right.Coordinates[0])
            });
        }
    }
}
