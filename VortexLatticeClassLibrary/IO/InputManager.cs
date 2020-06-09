using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VortexLatticeClassLibrary.Utilities;

namespace VortexLatticeClassLibrary.IO
{
    public class InputManager
    {
        public static void ParseAirfoilDatFile(string path)
        {
            if (!File.Exists(path))
            {
                return;
            }

            int fileLength;
            char[] charArray;
            List<List<double>> coordinates = new List<List<double>>();

            using (BinaryReader br = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                fileLength = (int)br.BaseStream.Length;
                charArray = new char[fileLength];

                br.Read(charArray, 0, fileLength);
            }

            CharArrayReader charArrayReader = new CharArrayReader(charArray);

            charArrayReader.SkipToNextLine();
            charArrayReader.SkipToNextLine();
            charArrayReader.SkipToNextLine();
            charArrayReader.SkipToNextLine();

            coordinates.Add(charArrayReader.ParseAirfoilCoordinateDatLine());

            Console.WriteLine(charArray);
            Console.WriteLine($"x = {coordinates[0][0]}; y = {coordinates[0][1]}");
        }
    }
}
