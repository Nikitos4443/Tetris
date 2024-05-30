using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Shapes;

namespace Tetris
{
    public class FileSystem
    {
        static public void WritingSaveInFile(List<Rectangle> rectangles, int score, double falling, string filePath)
        {
            StreamWriter writer = new StreamWriter(filePath);

            writer.Write($"{MainWindow.checkMode}\n");

            for(int i = 0; i < rectangles.Count; i++)
            {
                writer.Write($"{rectangles[i].Margin} ");
            }

            writer.Write($"{score} ");
            writer.Write($"{falling}");

            writer.Close();
        }

        static public void WritingRecordInFile(int score, string filePath)
        {
            if(score == 0)
            {
                return;
            }

            StreamWriter file = File.AppendText(filePath);

            file.Write($"{DateTime.Now} -- {score}\n");
            file.Close();
        }

        static public void WritingSettingsInFile(double backMusic, double effectMusic, double falling)
        {
            StreamWriter writer = new StreamWriter("Saves/Settings.txt");

            writer.Write($"{backMusic} {effectMusic} {falling}");
            writer.Close();
        }
    }
}
