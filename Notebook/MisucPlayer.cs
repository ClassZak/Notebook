using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Notebook
{
    class MisucPlayer
    {
        public MusicSettings MusicSettings { get; set; }
        public MediaPlayer MediaPlayer;
        public void UpdateMediaPlayer()
        {
            try
            {
                if (!MusicSettings.IsPlaying)
                    return;

                MediaPlayer.Open(new Uri(MusicSettings.MusicPath));
                MediaPlayer.Volume=MusicSettings.MusicVolume;
                MediaPlayer.Position=TimeSpan.FromMilliseconds(MusicSettings.MusicPosition);
                if(!MusicSettings.IsPausing)
                    MediaPlayer.Play();
                MediaPlayer.MediaEnded += new EventHandler(MusicLoop);
            }
            catch(IOException ex)
            {
                CustomMessageBox.Show(ex.Message,"Ошибка настроек музыки!",MessageBoxButton.OK);
                throw;
            }
        }



        public void MusicLoop(object sender, EventArgs eventArgs)
        {
            if (MusicSettings.IsPlaying)
                MediaPlayer.Play();
        }
        public void MusicPause()
        {
            if(MusicSettings.IsPlaying)
                MediaPlayer.Pause();
        }
        public void MusicResume()
        {
            if (MusicSettings.IsPlaying)
                MediaPlayer.Play();
        }
        public void SetVolume(float volume)
        {
            if (MusicSettings.IsPlaying)
                MediaPlayer.Volume = volume;
        }
        public void SetPosition(double pos)
        {
            if (MusicSettings.IsPlaying)
                MediaPlayer.Position = TimeSpan.FromMilliseconds(pos);
        }
    }
}
