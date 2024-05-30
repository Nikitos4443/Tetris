using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Tetris
{
    public class Saving
    {

        static public List<Thickness> GetPlaced()
        {
            List<Thickness> thicknesses = new List<Thickness>();

            string[] strings = File.ReadAllLines("Saves/Savings.txt");

            string[] array = strings[1].Split(' ');

            for (int i = 0; i < array.Length - 2; i++)
            {
                Thickness thickness = new Thickness();

                thickness.Left = int.Parse(array[i].Split(',')[0]);
                thickness.Top = int.Parse(array[i].Split(',')[1]);

                thicknesses.Add(thickness);
            }

            return thicknesses;
        }

        static public int GetScore()
        {
            string[] strings = File.ReadAllLines("Saves/Savings.txt");

            string[] array = strings[1].Split(' ');

            return int.Parse(array[array.Length-2]);
        }

        static public void ClearSavings()
        {
            StreamWriter writer = new StreamWriter("Saves/Savings.txt");
            writer.Close();
        }

        static public char CheckMode()
        {
            string[] lines = File.ReadAllLines("Saves/Savings.txt");
            return lines[0][0];
        }

        static public double checkTimeToFall()
        {
            string[] strings = File.ReadAllLines("Saves/Savings.txt");

            string[] array = strings[1].Split(' ');

            return double.Parse(array[array.Length - 1]);
        }
    }
}
