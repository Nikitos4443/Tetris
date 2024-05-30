using System.Windows;
using System.IO;


namespace Tetris
{
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
            if (!IsRecord())
            {
                recordButton.IsEnabled = false;
                recordButton.Opacity = 0.4;
            }
        }

        private bool IsRecord()
        {
            string filePath = "Saves/Records.txt";
            try
            {
                string[] lines = File.ReadAllLines(filePath);

                if (lines.Length == 0)
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


        private void OpenTetris(object sender, RoutedEventArgs e)
        {           
            GoingPlay goingPlay = new GoingPlay();
            var f = goingPlay.ShowDialog();
            if (f.Value)
            {
                Close();
            }
        }

        private void OpenSettings(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }

        private void OpenRecords(object sender, RoutedEventArgs e)
        {
            RecordWindow rec = new RecordWindow();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
