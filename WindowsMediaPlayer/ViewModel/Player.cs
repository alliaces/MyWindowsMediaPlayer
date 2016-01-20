using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace WindowsMediaPlayer
{
    public class Player : ViewModelBase
    {
        public enum RepeatType { NONE, LIST, ONE };

        private MediaCreator _mediaCreator = new MediaCreator(); //TODO : Event

        private DispatcherTimer _timerVolume = new DispatcherTimer();
        private DispatcherTimer _timerMedia = new DispatcherTimer();

        private Random _randomGenerator = new Random(DateTime.Now.Millisecond);

        private ObservableCollection<Media> _currentPlaylist;
        public ObservableCollection<Media> CurrentPlaylist
        {
            get { return _currentPlaylist; }

            set
            {
                _currentPlaylist = value;
                RaisePropertyChanged("CurrentPlaylist");
            }
        }

        private int _tempoCurrentImage = 0;
        public int TempoCurrentImage
        {
            get { return _tempoCurrentImage; }

            set
            {
                _tempoCurrentImage = value;
                RaisePropertyChanged("TempoCurrentImage");
            }
        }

        private int _tempoImage = 10;
        public int TempoImage
        {
            get { return _tempoImage; }

            set
            {
                _tempoImage = value;
                RaisePropertyChanged("TempoImage");
            }
        }

        private int _currentMediaInt = 0;
        public int CurrentMediaInt
        {
            get { return _currentMediaInt; }

            set
            {
                if (_currentPlaylist == null)
                    return;
                if (value >= _currentPlaylist.Count())
                    _currentMediaInt = 0;
                else if (value < 0)
                    _currentMediaInt = _currentPlaylist.Count() - 1;
                else
                    _currentMediaInt = value;
                RaisePropertyChanged("CurrentMediaInt");
            }
        }

        private string _mediaTitle;
        public string MediaTitle
        {
            get { return _mediaTitle; }

            set
            {
                _mediaTitle = value;
                RaisePropertyChanged("MediaTitle");
            }
        }


        private string _mediaPicture;
        public string MediaPicture
        {
            get { return _mediaPicture; }

            set
            {
                _mediaPicture = value;
                RaisePropertyChanged("MediaPicture");
            }
        }

        private Visibility _volumeVisibility;
        public Visibility VolumeVisibility
        {
            get { return _volumeVisibility; }

            set
            {
                _volumeVisibility = value;
                RaisePropertyChanged("VolumeVisibility");
            }
        }

        private Visibility _imageVisibility;
        public Visibility ImageVisibility
        {
            get { return _imageVisibility; }

            set
            {
                _imageVisibility = value;
                RaisePropertyChanged("ImageVisibility");
            }
        }

        private string _buttonPlayString;
        public string ButtonPlayString
        {
            get { return _buttonPlayString; }

            set
            {
                _buttonPlayString = value;
                RaisePropertyChanged("ButtonPlayString");
            }
        }

        private string _volumeString;
        public string VolumeString
        {
            get { return _volumeString; }

            set
            {
                _volumeString = value;
                RaisePropertyChanged("VolumeString");
            }
        }

        private string _timeMediaString;
        public string TimeMediaString
        {
            get { return _timeMediaString; }

            set
            {
                _timeMediaString = value;
                RaisePropertyChanged("TimeMediaString");
            }
        }

        private double _timeMedia;
        public double TimeMedia
        {
            get { return _timeMedia; }

            set
            {
                _timeMedia = value;
                RaisePropertyChanged("TimeMedia");
            }
        }

        private double _timeMediaTotal;
        public double TimeMediaTotal
        {
            get { return _timeMediaTotal; }

            set
            {
                _timeMediaTotal = value;
                RaisePropertyChanged("TimeMediaTotal");
            }
        }

        private Visibility _mediaVisible;
        public Visibility MediaVisible
        {
            get { return _mediaVisible; }

            set
            {
                _mediaVisible = value;
                RaisePropertyChanged("MediaVisible");
            }
        }

        private Visibility _imageVisible;
        public Visibility ImageVisible
        {
            get { return _imageVisible; }

            set
            {
                _imageVisible = value;
                RaisePropertyChanged("ImageVisible");
            }
        }

        private Visibility _isVisible;
        public Visibility IsVisible
        {
            get { return _isVisible; }

            set
            {
                _isVisible = value;
                RaisePropertyChanged("IsVisible");
            }
        }

        private MediaElement _media = new MediaElement();
        public MediaElement Media
        {
            get { return _media; }

            set
            {
                _media = value;
                RaisePropertyChanged("Media");
            }
        }

        private bool _shuffleMedia;
        public bool ShuffleMedia
        {
            get { return _shuffleMedia; }

            set
            {
                _shuffleMedia = value;
                RaisePropertyChanged("ShuffleMedia");
            }
        }

        private RepeatType _repeatMedia;
        public RepeatType RepeatMedia
        {
            get { return _repeatMedia; }

            set
            {
                _repeatMedia = value;
                RaisePropertyChanged("RepeatMedia");
            }
        }

        private bool _isMediaPlaying;
        public bool IsMediaPlaying
        {
            get { return _isMediaPlaying; }

            set
            {
                _isMediaPlaying = value;
                RaisePropertyChanged("IsMediaPlaying");
            }
        }

        public ICommand PlayMedia
        {
            get { return new DelegateCommand(playMedia); }
        }
        private void playMedia()
        {
            if (_currentPlaylist == null)
                return;
            if (IsMediaPlaying == false)
            {
                if (_currentPlaylist[_currentMediaInt].Type != MediaType.Picture)
                    Media.Play();
                IsMediaPlaying = true;
                
            }
            else
            {
                if (_currentPlaylist[_currentMediaInt].Type != MediaType.Picture)
                    Media.Pause();
                IsMediaPlaying = false;
            }
        }

        public ICommand StopMedia
        {
            get { return new DelegateCommand(stopMedia); }
        }
        private void stopMedia()
        {
            if (_currentPlaylist == null)
                return;
            MediaVisible = Visibility.Hidden;
            IsMediaPlaying = false;
            if (_currentPlaylist[_currentMediaInt].Type != MediaType.Picture)
                Media.Stop();
        }

        public ICommand ForwardMedia
        {
            get { return new DelegateCommand(forwardMedia); }
        }
        private void forwardMedia()
        {
            if (_currentPlaylist == null)
                return;
            if (_currentPlaylist[_currentMediaInt].Type != MediaType.Picture)
                Media.Position = new TimeSpan(0, 0, (int)Media.Position.TotalSeconds + 1);
        }

        public ICommand RewindMedia
        {
            get { return new DelegateCommand(rewindMedia); }
        }
        private void rewindMedia()
        {
            if (_currentPlaylist == null)
                return;
            if (_currentPlaylist[_currentMediaInt].Type != MediaType.Picture)
                Media.Position = new TimeSpan(0, 0, (int)Media.Position.TotalSeconds - 1);
        }

        public ICommand NextMedia
        {
            get { return new DelegateCommand(nextMedia); }
        }
        private void nextMedia()
        {
            if (_currentPlaylist == null)
                return;
            if (_repeatMedia == RepeatType.ONE)
            {
                if (_currentPlaylist[_currentMediaInt].Type != MediaType.Picture)
                    Media.Position = new TimeSpan(0, 0, 0);
                return;
            }
            switch (_shuffleMedia)
            {
                case true:
                    int r = _randomGenerator.Next(0, _currentPlaylist.Count() - 1);
                    if (_currentPlaylist.Count() != 1)
                        while ((r = _randomGenerator.Next(0, _currentPlaylist.Count() - 1)) == _currentMediaInt) ;
                    CurrentMediaInt = r;
                    break;
                case false:
                    if (_repeatMedia == RepeatType.NONE && CurrentMediaInt == CurrentPlaylist.Count - 1)
                    {
                        stopMedia();
                    }
                    else
                        CurrentMediaInt += 1;
                    break;
            }
            Media.Source = new Uri(_currentPlaylist[_currentMediaInt].Path);
            TempoCurrentImage = 0;
            if (_currentPlaylist[_currentMediaInt].Type == MediaType.Picture)
                TimeMediaTotal = _tempoImage;
            MediaTitle = System.IO.Path.GetFileNameWithoutExtension(_currentPlaylist[_currentMediaInt].Path);
            MediaPicture = _currentPlaylist[_currentMediaInt].Picture;
        }

        public ICommand PrevMedia
        {
            get { return new DelegateCommand(prevMedia); }
        }
        private void prevMedia()
        {
            if (_currentPlaylist == null)
                return;
            if (_repeatMedia == RepeatType.ONE)
            {
                if (_currentPlaylist[_currentMediaInt].Type != MediaType.Picture)
                    Media.Position = new TimeSpan(0, 0, 0);
                return;
            }
            switch (_shuffleMedia)
            {
                case true:
                    int r = _randomGenerator.Next(0, _currentPlaylist.Count() - 1);
                    if (_currentPlaylist.Count() != 1)
                        while ((r = _randomGenerator.Next(0, _currentPlaylist.Count() - 1)) == _currentMediaInt) ;
                    CurrentMediaInt = r;
                    break;
                case false:
                    CurrentMediaInt -= 1;
                    break;
            }
            Media.Source = new Uri(_currentPlaylist[_currentMediaInt].Path);
            TempoCurrentImage = 0;
            if (_currentPlaylist[_currentMediaInt].Type == MediaType.Picture)
                TimeMediaTotal = _tempoImage;
            MediaTitle = System.IO.Path.GetFileNameWithoutExtension(_currentPlaylist[_currentMediaInt].Path);
            MediaPicture = _currentPlaylist[_currentMediaInt].Picture;
        }

        public ICommand ShuffleTheMedia
        {
            get { return new DelegateCommand(shuffleTheMedia); }
        }
        private void shuffleTheMedia()
        {
            ShuffleMedia = !_shuffleMedia;
        }

        public ICommand IncreaseTempo
        {
            get { return new DelegateCommand(increaseTempo); }
        }
        private void increaseTempo()
        {
            TempoImage = TempoImage + 1;
            if (_currentPlaylist[_currentMediaInt].Type == MediaType.Picture)
                TimeMediaTotal = _tempoImage;
        }

        public ICommand DecreaseTempo
        {
            get { return new DelegateCommand(decreaseTempo); }
        }
        private void decreaseTempo()
        {
            if (TempoImage > 3)
                TempoImage = TempoImage - 1;
            if (_currentPlaylist[_currentMediaInt].Type == MediaType.Picture)
                TimeMediaTotal = _tempoImage;
        }

        public ICommand RepeatTheMedia
        {
            get { return new DelegateCommand(repeatTheMedia); }
        }
        private void repeatTheMedia()
        {
            switch (_repeatMedia)
            {
                case RepeatType.NONE:
                    RepeatMedia = RepeatType.LIST;
                    break;
                case RepeatType.LIST:
                    RepeatMedia = RepeatType.ONE;
                    break;
                case RepeatType.ONE:
                    RepeatMedia = RepeatType.NONE;
                    break;
            }
        }

        public RelayCommand<MouseEventArgs> MediaPositionChanged
        {
            get { return new RelayCommand<MouseEventArgs>(mediaPositionChanged); }
        }
        private void mediaPositionChanged(MouseEventArgs e)
        {
            if (_currentPlaylist == null)
                return;
            if (_currentPlaylist[_currentMediaInt].Type != MediaType.Picture)
                Media.Position = new TimeSpan(0, 0, (int)((Slider)e.Source).Value);
        }

        public RelayCommand<DragEventArgs> MediaElementDragEnter
        {
            get { return new RelayCommand<DragEventArgs>(mediaElementDragEnter); }
        }
        private void mediaElementDragEnter(DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop) || !e.Data.GetDataPresent("MediaFormat"))
            {
                e.Effects = DragDropEffects.None;
            }
        }

        public RelayCommand<SelectionChangedEventArgs> TabChanged
        {
            get { return new RelayCommand<SelectionChangedEventArgs>(tabChanged); }
        }
        private void tabChanged(SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                int tabItem = ((TabControl)e.Source).SelectedIndex;

                switch (tabItem)
                {
                    case 0:
                        if (_currentPlaylist == null)
                            break;
                        if (_currentPlaylist[_currentMediaInt].Type != MediaType.Video)
                        {
                            MediaVisible = Visibility.Hidden;
                            ImageVisible = Visibility.Visible;
                        }
                        else
                        {
                            MediaVisible = Visibility.Visible;
                            ImageVisible = Visibility.Hidden;
                        }
                        break;

                    case 1:
                        MediaVisible = Visibility.Hidden;
                        ImageVisible = Visibility.Hidden;
                        break;

                    default:
                        return;
                }
            }
        }

        public RelayCommand<MouseWheelEventArgs> ImageEventMouseWheel
        {
            get { return new RelayCommand<MouseWheelEventArgs>(imageEventMouseWheel); }
        }
        private void imageEventMouseWheel(MouseWheelEventArgs e)
        {
            if (_currentPlaylist == null)
                return;
            if (_currentPlaylist[_currentMediaInt].Type == MediaType.Picture)
                return;
            if (e.Delta > 0)
            {
                if (Media.Volume < 1)
                    Media.Volume += 0.05;
            }
            else
            {
                if (Media.Volume > 0)
                    Media.Volume -= 0.05;
            }
            VolumeString = (Media.Volume * 100).ToString() + " %";
            VolumeVisibility = Visibility.Visible;
            _timerVolume.Stop();
            _timerVolume.Start();
        }

        public RelayCommand<DragEventArgs> MediaElementDrop
        {
            get { return new RelayCommand<DragEventArgs>(mediaElementDrop); }
        }
        private void mediaElementDrop(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (string file in files)
                {
                    try
                    {
                        Media tmp = _mediaCreator.Create(file);
                        addOneMedia(tmp);
                        if (tmp.Type != MediaType.Video)
                        {
                            MediaVisible = Visibility.Hidden;
                            ImageVisible = Visibility.Visible;
                        }
                        else
                        {
                            MediaVisible = Visibility.Visible;
                            ImageVisible = Visibility.Hidden;
                        }
                    }
                    catch (InvalidMediaException ex)
                    {
                        Debug.Add(ex.ToString() + "\n");
                    }
                }
            }
            if (e.Data.GetDataPresent("MediaFormat"))
            {
                List<Media> medias = (List<Media>)e.Data.GetData("MediaFormat");

                foreach (Media media in medias)
                {
                    try
                    {
                        Media tmp = _mediaCreator.Create(media.Path);
                        addOneMedia(tmp);
                        if (tmp.Type != MediaType.Video)
                        {
                            MediaVisible = Visibility.Hidden;
                            ImageVisible = Visibility.Visible;
                        }
                        else
                        {
                            MediaVisible = Visibility.Visible;
                            ImageVisible = Visibility.Hidden;
                        }
                    }
                    catch (InvalidMediaException ex)
                    {
                        Debug.Add(ex.ToString() + "\n");
                    }
                }
            }
        }

        public Player()
        {
            IsVisible = Visibility.Visible;
            MediaVisible = Visibility.Hidden;
            ImageVisible = Visibility.Hidden;
            VolumeVisibility = Visibility.Hidden;
            ImageVisibility = Visibility.Visible;
            MediaTitle = "Pas Persona Media Player";
            ButtonPlayString = "Play";
            VolumeString = "";
            ShuffleMedia = false;
            RepeatMedia = RepeatType.NONE;
            IsMediaPlaying = false;
            Media.LoadedBehavior = MediaState.Manual;
            Playlist.OnPlaylistLoaded += loadPlaylist;
            SearchViewModel.OnSearchMediaDoubleClick += addOneMedia;
            LibraryViewModel.OnMediaDoubleClick += addOneMedia;
            Playlist.OnMediaDoubleClick += addOneMedia;
            Media.MediaOpened += MediaOpened;
            Media.MediaEnded += MediaEnded;
            Media.MouseWheel += mediaEventMouseWheel;
            _timerMedia.Interval = TimeSpan.FromSeconds(1);
            _timerMedia.Tick += TimerMedia;
            _timerMedia.Start();
            _timerVolume.Interval = TimeSpan.FromSeconds(1);
            _timerVolume.Tick += TimerVolume;
        }

        private void addOneMedia(Media media)
        {
            if (media == null)
                return;
            if (_currentPlaylist == null)
                CurrentPlaylist = new ObservableCollection<Media>();
            CurrentPlaylist.Add(media);
            CurrentMediaInt = CurrentPlaylist.Count - 1;
            Media.Source = new Uri(media.Path);
            TempoCurrentImage = 0;
            if (_currentPlaylist[_currentMediaInt].Type == MediaType.Picture)
                TimeMediaTotal = _tempoImage;
            MediaPicture = media.Picture;
            MediaTitle = System.IO.Path.GetFileNameWithoutExtension(_currentPlaylist[_currentMediaInt].Path);
            IsMediaPlaying = true;
            if (_currentPlaylist[_currentMediaInt].Type != MediaType.Picture)
                Media.Play();
        }

        private void loadPlaylist(ObservableCollection<Media> playlist)
        {
            CurrentPlaylist = new ObservableCollection<Media>(playlist);
            CurrentMediaInt = 0;
            IsVisible = Visibility.Visible;
            Media.Source = new Uri(_currentPlaylist[_currentMediaInt].Path);
            TempoCurrentImage = 0;
            if (_currentPlaylist[_currentMediaInt].Type == MediaType.Picture)
                TimeMediaTotal = _tempoImage;
            MediaPicture = _currentPlaylist[_currentMediaInt].Picture;
            MediaTitle = System.IO.Path.GetFileNameWithoutExtension(_currentPlaylist[_currentMediaInt].Path);
            IsMediaPlaying = true;
            if (_currentPlaylist[_currentMediaInt].Type != MediaType.Picture)
                Media.Play();
        }

        private void mediaEventMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (_currentPlaylist[_currentMediaInt].Type != MediaType.Picture)
                return;
            if (e.Delta > 0)
            {
                if (Media.Volume < 1)
                    Media.Volume += 0.05;
            }
            else
            {
                if (Media.Volume > 0)
                    Media.Volume -= 0.05;
            }
            VolumeString = (Media.Volume * 100).ToString() + " %";
            VolumeVisibility = Visibility.Visible;
            _timerVolume.Stop();
            _timerVolume.Start();
        }

        private void MediaOpened(object sender, RoutedEventArgs e)
        {
            if (_currentPlaylist[_currentMediaInt].Type != MediaType.Picture)
                TimeMediaTotal = Media.NaturalDuration.TimeSpan.TotalSeconds;
            else
                TimeMediaTotal = _tempoImage;
        }

        private void MediaEnded(object sender, RoutedEventArgs e)
        {
            if (_repeatMedia == RepeatType.ONE)
            {
                if (_currentPlaylist[_currentMediaInt].Type != MediaType.Picture)
                    Media.Position = new TimeSpan(0, 0, 0);
                return;
            }
            switch (_shuffleMedia)
            {
                case true:
                    int r = _randomGenerator.Next(0, _currentPlaylist.Count() - 1);
                    if (_currentPlaylist.Count() != 1)
                        while ((r = _randomGenerator.Next(0, _currentPlaylist.Count() - 1)) == _currentMediaInt) ;
                    CurrentMediaInt = r;
                    break;
                case false:
                    if (_repeatMedia == RepeatType.NONE && CurrentMediaInt == CurrentPlaylist.Count - 1)
                    {
                        stopMedia();
                    }
                    else
                        CurrentMediaInt += 1;
                    break;
            }
            Media.Source = new Uri(_currentPlaylist[_currentMediaInt].Path);
            TempoCurrentImage = 0;
            if (_currentPlaylist[_currentMediaInt].Type == MediaType.Picture)
                TimeMediaTotal = _tempoImage;
            MediaPicture = _currentPlaylist[_currentMediaInt].Picture;
            MediaTitle = System.IO.Path.GetFileNameWithoutExtension(_currentPlaylist[_currentMediaInt].Path);
        }

        private void TimerVolume(object sender, EventArgs e)
        {
            VolumeVisibility = Visibility.Hidden;
            _timerVolume.Stop();
        }

        private void TimerMedia(object sender, EventArgs e)
        {
            if (_currentPlaylist == null || _isMediaPlaying == false)
                return;
            if (Media.Source != null && _currentPlaylist[_currentMediaInt].Type != MediaType.Picture)
            {
                if (Media.NaturalDuration.HasTimeSpan)
                {
                    TimeMedia = Media.Position.TotalSeconds;
                    TimeMediaString = String.Format("{0} / {1}", Media.Position.ToString(@"hh\:mm\:ss"), Media.NaturalDuration.TimeSpan.ToString(@"hh\:mm\:ss"));
                }
            }
            else if (_currentPlaylist[_currentMediaInt].Type == MediaType.Picture)
            {
                TempoCurrentImage = _tempoCurrentImage + 1;
                TimeMedia = _tempoCurrentImage;
                TimeMediaString = String.Format("{0} / {1}", new TimeSpan(0, 0, _tempoCurrentImage).ToString(@"hh\:mm\:ss"), new TimeSpan(0, 0, _tempoImage).ToString(@"hh\:mm\:ss")); ;
                if (_tempoCurrentImage >= _tempoImage)
                {
                    TempoCurrentImage = 0;
                    MediaEnded(null, null);
                }
            }
        }
    }
}
