using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WindowsMediaPlayer
{
    /// <summary>
    /// Logique d'interaction pour Player.xaml
    /// </summary>
    public partial class Player2 : UserControl
    {
        bool play = false;
        DispatcherTimer timerVolume = new DispatcherTimer();
        playlistManager l = new playlistManager();

        public Player2()
        {
            InitializeComponent();
            mediaElement.Visibility = Visibility.Hidden;
            buttonPlay.Visibility = Visibility.Hidden;
            sliderTime.Visibility = Visibility.Hidden;
            labelTime.Visibility = Visibility.Hidden;
            buttonStop.Visibility = Visibility.Hidden;
            buttonForward.Visibility = Visibility.Hidden;
            buttonRewind.Visibility = Visibility.Hidden;
            buttonNext.Visibility = Visibility.Hidden;
            buttonPrev.Visibility = Visibility.Hidden;
            buttonShuffle.Visibility = Visibility.Hidden;
            buttonRepeat.Visibility = Visibility.Hidden;
            labelVolume.Visibility = Visibility.Hidden;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            timerVolume.Interval = TimeSpan.FromSeconds(1);
            timerVolume.Tick += Timer_Volume;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (mediaElement.Source != null)
            {
                if (mediaElement.NaturalDuration.HasTimeSpan)
                {
                    sliderTime.Value = mediaElement.Position.TotalSeconds;

                    labelTime.Content = String.Format("{0} / {1}", mediaElement.Position.ToString(@"mm\:ss"), mediaElement.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
                }
            }
        }

        private void Timer_Volume(object sender, EventArgs e)
        {
            labelVolume.Visibility = Visibility.Hidden;
            timerVolume.Stop();
        }

        /*
        ** To change to something that interact with others user controls
        */
        private void buttonOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Title = "Select a media file";
            fileDialog.DefaultExt = ".mp4";
            fileDialog.Filter = "MP4 Files (*.mp4)|*.mp4|MP3 Files (*.mp3)|*.mp3|AVI Files (*.avi)|*.avi|M4A Files (*.m4a)|*.m4a";

            if (fileDialog.ShowDialog() == true)
            {
                mediaElement.Source = new Uri(fileDialog.FileName);
                playMedia(fileDialog.FileName);
            }
        }

        public void playMedia(string path)
        {
            mediaElement.Source = new Uri(path);
            mediaElement.Visibility = Visibility.Visible;
            buttonPlay.Visibility = Visibility.Visible;
            sliderTime.Visibility = Visibility.Visible;
            labelTime.Visibility = Visibility.Visible;
            buttonStop.Visibility = Visibility.Visible;
            buttonForward.Visibility = Visibility.Visible;
            buttonRewind.Visibility = Visibility.Visible;
            buttonNext.Visibility = Visibility.Visible;
            buttonPrev.Visibility = Visibility.Visible;
            buttonShuffle.Visibility = Visibility.Visible;
            buttonRepeat.Visibility = Visibility.Visible;
            buttonOpen.Visibility = Visibility.Hidden;
            mediaElement.Volume = 0.5;
        }

        private void buttonPlay_Click(object sender, RoutedEventArgs e)
        {
            if (play == false)
            {
                mediaElement.Visibility = Visibility.Visible;
                mediaElement.Play();
                buttonPlay.Content = "Pause";
                play = true;
            }
            else
            {
                buttonPlay.Content = "Play";
                mediaElement.Pause();
                play = false;
            }
        }

        private void mediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            sliderTime.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
        }

        private void buttonStop_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Visibility = Visibility.Hidden;
            mediaElement.Stop();
        }



        private void sliderTime_LostMouseCapture(object sender, MouseEventArgs e)
        {
            mediaElement.Position = new TimeSpan(0, 0, (int)sliderTime.Value);
        }

        private void mediaElement_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (mediaElement.Volume < 1)
                    mediaElement.Volume += 0.05;
            }
            else
            {
                if (mediaElement.Volume > 0)
                    mediaElement.Volume -= 0.05;
            }
            labelVolume.Content = (mediaElement.Volume * 100).ToString() + " %";
            labelVolume.Visibility = Visibility.Visible;
            timerVolume.Start();
        }

        private void buttonForward_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Position = new TimeSpan(0, 0, (int)mediaElement.Position.TotalSeconds + 1);
        }

        private void buttonRewind_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Position = new TimeSpan(0, 0, (int)mediaElement.Position.TotalSeconds - 1);
        }
    }
}
