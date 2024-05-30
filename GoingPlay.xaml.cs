using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Tetris
{
    public partial class GoingPlay : Window
    {
        public GoingPlay()
        {
            InitializeComponent();
            if (!isGameForContinue())
            {
                cont.IsEnabled = false;
                cont.Opacity = 0.4;
            }
        }

        private bool isGameForContinue()
        {
            string filePath = "Saves/Savings.txt";
            try
            {
                string[] lines = File.ReadAllLines(filePath);

                if (lines.Length < 2)
                {
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void Start_Tetramino(object sender, RoutedEventArgs e)
        {
            string filePath = "Saves/Savings.txt";
            StreamWriter writer = new StreamWriter(filePath);
            writer.Close();

            MainWindow wnd = new MainWindow('t');
            wnd.Show();
            DialogResult = true;
            Close();
        }

        private void Start_Pentamino(object sender, RoutedEventArgs e)
        {
            string filePath = "Saves/Savings.txt";
            StreamWriter writer = new StreamWriter(filePath);
            writer.Close();

            MainWindow wnd = new MainWindow('p');
            wnd.Show();
            DialogResult = true;
            Close();
        }

        private List<Rectangle> GetPlacedRects()
        {
            List<Thickness> list = Saving.GetPlaced();
            List<Rectangle> rects = new List<Rectangle>();

            foreach (Thickness margin in list)
            {
                Rectangle rectangle = new Rectangle();
                Color aqua = Color.FromRgb(0, 200, 200);
                SolidColorBrush color = new SolidColorBrush(aqua);

                ProgenitorRectangle.CopyRectangleProperties(ref rectangle, color);

                rectangle.Effect = null;
                rectangle.StrokeThickness = 0.1;
                rectangle.Stroke = Brushes.Black;

                rectangle.Margin = margin;

                rects.Add(rectangle);
            }

            return rects;
        }

        private void ContinueGame(object sender, RoutedEventArgs e)
        {
            List<Rectangle> rects = GetPlacedRects();
            int score = Saving.GetScore();
            char mode = Saving.CheckMode();
            double timeToFall = Saving.checkTimeToFall();

            MainWindow wnd = new MainWindow(mode, rects, score, timeToFall);
            wnd.Show();
            DialogResult = true;
            Close();
        }

        private void Back_to_menu(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
