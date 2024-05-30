using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tetris;

namespace Tetris
{
    public partial class RecordWindow : Window
    {
        public RecordWindow()
        {
            InitializeComponent();
            GetRecords();
        }

        void GetRecords()
        {
            try
            {
                string[] strings = File.ReadAllLines("Saves/Records.txt");

                foreach (string s in strings)
                {
                    TextBlock textBlock = new TextBlock();
                    textBlock.FontSize = 12;
                    textBlock.FontWeight = FontWeights.DemiBold;
                    textBlock.Foreground = Brushes.White;
                    textBlock.Text = s;
                    _records.Children.Add(textBlock);
                }

                ShowDialog();
            }
            catch
            {
            } 
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
