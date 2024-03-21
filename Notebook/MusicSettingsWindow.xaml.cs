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
using System.Media;
using Microsoft.Win32;
using System.Threading;
using System.Runtime.CompilerServices;

namespace Notebook
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class MusicSettingsWindow : Window
    {
        public MusicSettingsWindow()
        {
            closed= false;
            InitializeComponent();
            
        }
        public static MusicSettings settings=new MusicSettings();
        public static bool closed=false;
        

        private void Pause_Button_Click(object sender, RoutedEventArgs e)
        {
            settings.IsPausing=!settings.IsPausing;
            CheckPausing();
            if(settings.IsPausing)
                MainWindow.backgroundSoundPlayer.Pause();
            else
                MainWindow.backgroundSoundPlayer.Play();
        }
        private void Stop_Button_Click(object sender, RoutedEventArgs e)
        {
            this.PositionLabel.Text = MainWindow.backgroundSoundPlayer.Position.ToString().Substring(0, 8) + "/" + MainWindow.backgroundSoundPlayer.NaturalDuration.ToString().Substring(0, 8);
            MainWindow.backgroundSoundPlayer.Stop();
            this.StopButton.IsEnabled = this.PlayButton.IsEnabled= false;
            this.PathLabel.Foreground=new SolidColorBrush(Colors.LightGray);
            this.LoadButton.IsEnabled = true;
            settings.IsPausing = false;
            settings.IsPlaying = false;
            CheckPausing();
        }
        private void Load_Button_Click(object sender, RoutedEventArgs e)
        {
            do
            {
                try
                {
                    bool selectedSucces=false;
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Музыка и звуки (*.mp3,*.wav,*.wma)|*.mp3;*.wav;*.wma";
                    if (!((bool)(openFileDialog.ShowDialog())))
                    {
                        if (MainWindow.backgroundSoundPlayer == null)
                            break;
                        MainWindow.backgroundSoundPlayer.Position=settings.MusicPosition;
                        this.StopButton.IsEnabled = this.PlayButton.IsEnabled = true;
                        this.LoadButton.IsEnabled = false;
                        settings.IsPlaying=true;
                        this.PathLabel.Foreground = new SolidColorBrush(Colors.Black);
                    }

                    if (MainWindow.backgroundSoundPlayer == null)
                        MainWindow.backgroundSoundPlayer=new MediaPlayer();
                    MainWindow.backgroundSoundPlayer.Open(new Uri((openFileDialog.FileName==string.Empty)? settings.MusicPath : openFileDialog.FileName));
                    MainWindow.backgroundSoundPlayer.Position = TimeSpan.MinValue;
                    MainWindow.backgroundSoundPlayer.Play();
                    settings.IsPausing = false;
                    settings.IsPlaying = true;
                    this.StopButton.IsEnabled = this.PlayButton.IsEnabled = true;
                    this.LoadButton.IsEnabled = false;
                    this.volumeSlider.IsEnabled = true;

                    settings.MusicPath = openFileDialog.FileName;
                    CheckMusicPath();
                    //Window_Initialized(null, null);
                    break;
                }
                catch(Exception ex)
                {
                    CustomMessageBox.Show("Необработанное исключение!\n\n"+ex.Message,"Ошибка загрузки музыки!");
                }
            }
            while(true);

        }
        private void volumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MainWindow.backgroundSoundPlayer.Volume = settings.MusicVolume = volumeSlider.Value;
        }






        private void CheckPausing()
        {
            if(settings.IsPausing)
            {
                var uri = new Uri("pack://application:,,,/Resources/Resume.bmp");
                var bitmap = new BitmapImage(uri);
                PlayButton.Content = new Image { Source = bitmap };
            }
            else
            {
                var uri = new Uri("pack://application:,,,/Resources/Pause.bmp");
                var bitmap = new BitmapImage(uri);
                PlayButton.Content = new Image { Source = bitmap };
            }
        }
        private void CheckMusicPath()
        {
            if(settings.MusicPath!=string.Empty)
                this.PathLabel.Text = settings.MusicPath;
        }
        private async Task MusicPositionChecking()
        {
            while (!MusicSettingsWindow.closed)
            {
                await Task.Delay(100);
                if(settings.IsPlaying)
                this.PositionLabel.Text = MainWindow.backgroundSoundPlayer.Position.ToString().Substring(0,8)+"/"+ MainWindow.backgroundSoundPlayer.NaturalDuration.ToString().Substring(0, 8);
            }
        }

        private async void Window_Initialized(object sender, EventArgs e)
        {
            if(!settings.IsPlaying)
            {
                this.volumeSlider.IsEnabled = false;
                this.StopButton.IsEnabled = this.PlayButton.IsEnabled = false;
                this.LoadButton.IsEnabled = true;
            }
            CheckPausing();
            CheckMusicPath();
            CheckVolume();

            if (settings.IsPlaying)
            {
                var Pos = MusicPositionChecking();
                await Pos;
            }
                
        }
        private void CheckVolume()
        {
            if(settings.IsPlaying)
            volumeSlider.Value=settings.MusicVolume=MainWindow.backgroundSoundPlayer.Volume;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MusicSettingsWindow.closed = true;
        }
    }
}
