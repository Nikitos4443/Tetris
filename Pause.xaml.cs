using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Learning
{
    public partial class Pause : Window
    {
        MainWindow window;
        public Pause(MainWindow window)
        {
            InitializeComponent();
            this.window = window;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            window.isContinue = true;
            Close();
        }

        private void GoMenu(object sender, RoutedEventArgs e)
        {
            window.Close();
            MainMenu menu = new MainMenu();
            menu.Show();
            Close();
        }
    }
}
