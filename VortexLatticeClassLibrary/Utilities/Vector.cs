using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Resources;
using System.Text;
using VortexLatticeClassLibrary.IO;

namespace VortexLatticeClassLibrary.Utilities
{
    public struct Vector
    {
        public Vector(double[] coordinates)
        {
            Coordinates = coordinates;
        }
        public double[] Coordinates { get; private set; }
        public int Rank { get { return Coordinates.Rank; } }

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
                throw new InvalidOperationException("Cannot add vectors of different dimesnion.");
            }

            double[] coordinates = new double[left.Coordinates.Length];
            for (int i = 0; i < coordinates.Length; i++)
            {
                coordinates[i] = left.Coordinates[i] - right.Coordinates[i];
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
        public static Vector operator *(Vector v, double c)
        {
            double[] coordinates = new double[v.Coordinates.Length];
            for (int i = 0; i < coordinates.Length; i++)
            {
                coordinates[i] = c * v.Coordinates[i];
            }
            return new Vector(coordinates);
        }


        // -------------------  Incomplete  -----------------------------


        public static double operator*(Vector left, Vector right)
        {
            return 1.1;
        }
        public static double Cross(Vector other)
        {
            return 1.1;
        }
    }
}
