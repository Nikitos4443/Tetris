using System.Windows;
using System.IO;


namespace Tetris
{
    public partial class Settings : Window
    {
        static public double BackgroundMusicVolume = 0.5;
        static public double EffectMusicVolume = 0.1;
        static public double Falling = 0.9;

        static public double FallingTime
        {
            get
            {
                if(Falling == 0)
                {
                    return 400;
                }

                return Falling * 1000 + 400;
            }
        }


        public Settings()
        {
            InitializeComponent();
            GetValues();
        }

        void GetValues()
        {
            try
            {
                string[] str = File.ReadAllLines("Saves/Settings.txt");

                BackVolumeSlider.Value = double.Parse(str[0].Split(' ')[0]);
                EffectsVolumeSlider.Value = double.Parse(str[0].Split(' ')[1]);
                TimeToFallSlider.Value = double.Parse(str[0].Split(' ')[2]);
            }
            catch
            {
                return;
            }
        }

        private void BackVolumeChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            BackgroundMusicVolume = BackVolumeSlider.Value;
        }
        private void EffectsVolumeChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            EffectMusicVolume = EffectsVolumeSlider.Value;
        }

        private void TimeToFallChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Falling = TimeToFallSlider.Value;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            FileSystem.WritingSettingsInFile(BackgroundMusicVolume, EffectMusicVolume, Falling);
            Close();
        }
    }
}
