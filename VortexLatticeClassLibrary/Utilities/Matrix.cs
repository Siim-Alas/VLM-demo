using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace VortexLatticeClassLibrary.Utilities
{
    public struct Matrix<T>
    {
        public Matrix(T[][] elements)
        {
            Elements = elements;
        }
        public T[][] Elements { get; private set; }
        public static double[] SolveWithGaussianElimination(Matrix<double> M)
        {
            double[][] A = M.Elements;
            int rows = A.Length;
            int columns = A[0].Length;
            int k = 0;
            double[] row;
            double factor;
            
            // There are k variables, so the procedure has to be repeated k times

            // For every column (k variables, hence k columns have already been looked through)
            for (int j = k; j < columns; j++)
            {
                // Look through the rows (k variables, hence k rows have already been looked through)
                for (int i = k; i < rows; i++)
                {
                    // Find the first row with a non-zero leading figure
                    if (A[i][j] != 0)
                    {
                        // Move it to the top
                        row = A[k];
                        A[k] = A[i];
                        A[i] = row;

                        // Divide the moved row by its leading figure (we know it isn't 0)
                        factor = 1 / A[k][j];
                        for (int c = j; c < columns; c++)
                        {
                            A[k][c] *= factor;
                        }
                        // Avoid rounding errors
                        A[k][j] = 1;

                        // For every row under our normalized one
                        for (int r = k + 1; r < rows; r++)
                        {
                            // Subtract the leading figure times our normalized row from it
                            factor = A[r][j];
                            for (int c = j; c < columns; c++)
                            {
                                A[r][c] -= factor * A[k][c];
                            }
                            // Avoid rounding errors
                            A[r][j] = 0;
                        }
                        // A variable has been dealt with, move onto the next one
                        k++;
                        break;
                    }
                }
            }

            // The matrix has been brought to row-echelon form


            return new double[1];
        }
    }
}
