using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Tetris
{
    public static class Audio
    {
        public static MediaPlayer backGroundMediaPlayer;
        public static MediaPlayer stepMediaPlayer;

        public static void PlayMusic()
        {
            string URI = "Audio/Tetris.wav";
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), URI);
            backGroundMediaPlayer = new MediaPlayer();
            backGroundMediaPlayer.Open(new Uri(filePath));
            backGroundMediaPlayer.MediaEnded += new EventHandler(MediaEnded);
            backGroundMediaPlayer.Volume = Settings.BackgroundMusicVolume;
            backGroundMediaPlayer.Play();
        }

        static void MediaEnded(object sender, EventArgs e)
        {
            backGroundMediaPlayer.Position = TimeSpan.Zero;
            backGroundMediaPlayer.Play();
        }

        public static void PlaySoundStep(char check)
        {
            string relPath = check == 's' ? "Audio/Step.wav" : "Audio/Fall.wav";
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), relPath);
            stepMediaPlayer = new MediaPlayer();
            stepMediaPlayer.Open(new Uri(filePath));
            stepMediaPlayer.Volume = Settings.EffectMusicVolume;
            stepMediaPlayer.Play();
        }
    }
}
