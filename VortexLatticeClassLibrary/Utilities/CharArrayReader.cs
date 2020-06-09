using System;
using System.Collections.Generic;
using System.Text;

namespace VortexLatticeClassLibrary.Utilities
{
    public class CharArrayReader
    {
        // Airfoil .dat files are in the following format
        // Newlines are \r\n

        // Airfoil name
        //     2.    2.
        //
        //  0.0000000 0.0000000
        //  0.0010700 -.0175200
        //
        //  0.0000000 0.0000000
        //  0.0010700 -.0175200

        public CharArrayReader(char[] charArray, int index = 0)
        {
            CharArray = charArray;
            Index = index;
        }
        public char[] CharArray { get; private set; }
        public int Index { get; set; }

        public void SkipToNextLine()
        {
            for (int i = Index; i < CharArray.Length; i++)
            {
                if (CharArray[i] == '\n')
                {
                    Index = i + 1;
                    break;
                }
            }
        }

        public List<double> ParseAirfoilCoordinateDatLine()
        {
            // This method assumes x is in [0;1] and y is in (-1;1]

            List<double> coordinates = new List<double>() { 0, 0 };
            double factor = 0.1;
            int i = Index;

            // Get to the start of x
            while (CharArray[i + 1] != '.')
            {
                i++;
            }

            // Parse x (starts with ' ', '0', or '1')
            coordinates[0] += OwnConvertToDouble(CharArray[i]);
            // Skip the dot
            i += 2;
            while (true)
            {
                try
                {
                    coordinates[0] += factor * OwnConvertToDouble(CharArray[i]);
                    factor /= 10;
                    i++;
                }
                catch (ArgumentException)
                {
                    break;
                }
            }
            factor = 0.1;

            // Get to the start of y
            while (CharArray[i + 1] != '.')
            {
                i++;
            }

            // Parse y (starts with ' ', '0' or '-')
            factor *= (CharArray[i] == '-') ? -1 : 1;
            // Skip the dot
            i += 2;
            while (true)
            {
                try
                {
                    coordinates[1] += factor * OwnConvertToDouble(CharArray[i]);
                    factor /= 10;
                    i++;
                }
                catch (ArgumentException)
                {
                    break;
                }
            }

            // Jump to the new line
            while (CharArray[i] != '\n')
            {
                i++;
            }
            Index = i + 1;

            return coordinates;
        }

        private double OwnConvertToDouble(char c)
        {
            switch (c)
            {
                case '0':
                    return 0.0;
                case '1':
                    return 1.0;
                case '2':
                    return 2.0;
                case '3':
                    return 3.0;
                case '4':
                    return 4.0;
                case '5':
                    return 5.0;
                case '6':
                    return 6.0;
                case '7':
                    return 7.0;
                case '8':
                    return 8.0;
                case '9':
                    return 9.0;
                default:
                    throw new ArgumentException();
            }
        }
    }
}
