using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WindowsMediaPlayer
{
    public delegate void PlaylistLoadedHandler(ObservableCollection<Media> playlist);
    public delegate void PlaylistMediaDoubleClickHandler(Media media);

    public class OnePlaylist
    {
        public string Name;
        public ObservableCollection<Media> ThePlaylist;

        public OnePlaylist(string name, ObservableCollection<Media> playlist)
        {
            Name = name;
            ThePlaylist = playlist;
        }

        public override string ToString()
        {
            return System.IO.Path.GetFileNameWithoutExtension(Name);
        }
    }

    public class Playlist : ViewModelBase
    {
        public static event PlaylistLoadedHandler OnPlaylistLoaded = delegate { };
        public static event PlaylistMediaDoubleClickHandler OnMediaDoubleClick = delegate { };

        private MediaCreator _mediaCreator = new MediaCreator(); //TODO : Event

        private readonly playlistManager _manager = new playlistManager();
        private int _selectedIndex = -1;

        private string _playlistString;
        public string PlaylistNameString
        {
            get { return System.IO.Path.GetFileNameWithoutExtension(_playlistString); }

            set
            {
                _playlistString = value;
                RaisePropertyChanged("PlaylistNameString");
            }
        }

        private ObservableCollection<OnePlaylist> _observableAllPlaylists = null;
        public ObservableCollection<OnePlaylist> ObservableAllPlaylists
        {
            get { return _observableAllPlaylists; }

            set
            {
                _observableAllPlaylists = value;
                RaisePropertyChanged("ObservableAllPlaylists");
            }
        }

        private ObservableCollection<Media> _observablePlaylist = null;
        public ObservableCollection<Media> ObservablePlaylist
        {
            get { return _observablePlaylist; }

            set
            {
                _observablePlaylist = value;
                RaisePropertyChanged("ObservablePlaylist");
            }
        }

        public ICommand SavePlaylist
        {
            get { return new DelegateCommand(savePlaylist); }
        }
        private void savePlaylist()
        {
            if (_observablePlaylist == null)
            {
                MessageBox.Show("Please select a playlist !", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            SaveFileDialog fileDialog = new SaveFileDialog();

            fileDialog.FileName = "*";
            fileDialog.DefaultExt = "m3u";
            fileDialog.Filter = "M3U Files (.m3u)|*.m3u";
            if (fileDialog.ShowDialog() == true)
            {
                _manager.savePlayList(_observablePlaylist, fileDialog.FileName);
            }
        }

        public RelayCommand<MouseButtonEventArgs> DoubleClickMediaPlaylist
        {
            get { return new RelayCommand<MouseButtonEventArgs>(doubleClickMediaLibrary); }
        }
        private void doubleClickMediaLibrary(MouseButtonEventArgs e)
        {
            if (_selectedIndex != -1)
                OnMediaDoubleClick(ObservablePlaylist[_selectedIndex]);
        }

        private Visibility _createVisibility;
        public Visibility CreateVisibility
        {
            get { return _createVisibility; }

            set
            {
                _createVisibility = value;
                RaisePropertyChanged("CreateVisibility");
            }
        }

        public ICommand LoadPlaylist
        {
            get { return new DelegateCommand(loadPlaylist); }
        }
        private void loadPlaylist()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Title = "Select a playlist";
            fileDialog.DefaultExt = ".m3u";
            fileDialog.Filter = "M3U Files (*.m3u)|*.m3u";
            if (fileDialog.ShowDialog() == true)
            {
                ObservablePlaylist = _manager.loadPlayListFile(fileDialog.FileName);
                PlaylistNameString = fileDialog.FileName;
                if (_observableAllPlaylists == null)
                    ObservableAllPlaylists = new ObservableCollection<OnePlaylist>();
                ObservableAllPlaylists.Add(new OnePlaylist(fileDialog.FileName, new ObservableCollection<Media>(_observablePlaylist)));
            }
        }

        public ICommand PlayPlaylist
        {
            get { return new DelegateCommand(playPlaylist); }
        }
        private void playPlaylist()
        {
            if (_observablePlaylist != null)
                OnPlaylistLoaded(_observablePlaylist);
            else
                MessageBox.Show("Please select a playlist !", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public RelayCommand<SelectionChangedEventArgs> SelectionChangedPlaylist
        {
            get { return new RelayCommand<SelectionChangedEventArgs>(selectionChangedPlaylist); }
        }
        private void selectionChangedPlaylist(SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
                _selectedIndex = ((ListBox)e.OriginalSource).SelectedIndex;
            else
                _selectedIndex = -1;
        }

        public ICommand DelPlaylist
        {
            get { return new DelegateCommand(delPlaylist); }
        }
        private void delPlaylist()
        {
            if (_selectedIndex != -1)
            {
                ObservablePlaylist.RemoveAt(_selectedIndex);
            }
        }

        public ICommand EditPlaylist
        {
            get { return new DelegateCommand(editPlaylist); }
        }
        private void editPlaylist()
        {
            if (_playlistString != "")
            {
                _manager.savePlayList(ObservablePlaylist, _playlistString);
                MessageBoxResult mb = MessageBox.Show(System.IO.Path.GetFileNameWithoutExtension(_playlistString) + " has been saved", "Edit Playlist", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public ICommand AddPlaylist
        {
            get { return new DelegateCommand(addPlaylist); }
        }
        private void addPlaylist()
        {
            if (_observablePlaylist == null)
            {
                MessageBoxResult mb = MessageBox.Show("Please select a playlist !", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                if (mb == MessageBoxResult.OK)
                    return;
            }
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Title = "Select a media file";
            fileDialog.Filter = "Music Files|*.wav;*.flac;*.alac;*.ac3;*.mp3;*.aac;*.wma;*.m4a;*.mp2|Video Files|*.avi;*.mov;*.wmv;*.divx;*.xvid;*.mkv;*.mp4;*.flv|Image Files|*.jpg;*.gif;*.bmp;*.tif;*.pct;*.png;*.jpeg;*.raw";

            if (fileDialog.ShowDialog() == true)
            {
                if (_observablePlaylist == null)
                {
                    ObservablePlaylist = new ObservableCollection<Media>();
                }
                try
                {
                    Media media = _mediaCreator.Create(fileDialog.FileName);
                    ObservablePlaylist.Add(media);
                }
                catch (Exception e)
                {
                    Debug.Add(e.ToString());
                }
            }
        }

        public RelayCommand<KeyEventArgs> KeyUpCreatePlaylist
        {
            get { return new RelayCommand<KeyEventArgs>(keyUpCreatePlaylist); }
        }

        private void keyUpCreatePlaylist(KeyEventArgs e)
        {
            if (((TextBox)e.OriginalSource).Text != "")
            {
                if (e.Key == System.Windows.Input.Key.Enter)
                {
                    if (_observableAllPlaylists == null)
                        ObservableAllPlaylists = new ObservableCollection<OnePlaylist>();
                    ObservableAllPlaylists.Add(new OnePlaylist(((TextBox)e.OriginalSource).Text + ".m3u", new ObservableCollection<Media>()));
                    CreateVisibility = Visibility.Hidden;
                }
            }
        }

        public ICommand CreatePlaylist
        {
            get { return new DelegateCommand(createPlaylist); }
        }
        private void createPlaylist()
        {
            CreateVisibility = Visibility.Visible;
        }

        public RelayCommand<DragEventArgs> OnePlaylistDragEnter
        {
            get { return new RelayCommand<DragEventArgs>(onePlaylistDragEnter); }
        }
        private void onePlaylistDragEnter(DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.None;
            }
        }

        public RelayCommand<DragEventArgs> OnePlaylistDrop
        {
            get { return new RelayCommand<DragEventArgs>(onePlaylistDrop); }
        }
        private void onePlaylistDrop(DragEventArgs e)
        {
            //if ()
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (string file in files)
                {
                    try
                    {
                        Media tmp = _mediaCreator.Create(file);
                        if (_observablePlaylist == null)
                        {
                            ObservablePlaylist = new ObservableCollection<Media>();
                        }
                        ObservablePlaylist.Add(tmp);
                    }
                    catch (Exception ex)
                    {
                        Debug.Add(ex.ToString());
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
                        if (_observablePlaylist == null)
                        {
                            ObservablePlaylist = new ObservableCollection<Media>();
                        }
                        ObservablePlaylist.Add(tmp);
                    }
                    catch (InvalidMediaException ex)
                    {
                        Debug.Add(ex.ToString() + "\n");
                    }
                }
            }
        }

        public RelayCommand<DragEventArgs> AllPlaylistDragEnter
        {
            get { return new RelayCommand<DragEventArgs>(allPlaylistDragEnter); }
        }
        private void allPlaylistDragEnter(DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.None;
            }
        }

        public RelayCommand<DragEventArgs> AllPlaylistDrop
        {
            get { return new RelayCommand<DragEventArgs>(allPlaylistDrop); }
        }
        private void allPlaylistDrop(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (string file in files)
                {
                    if (file.Contains(".m3u"))
                    {
                        if (_observableAllPlaylists == null)
                            ObservableAllPlaylists = new ObservableCollection<OnePlaylist>();
                        ObservableAllPlaylists.Add(new OnePlaylist(file, new ObservableCollection<Media>(_manager.loadPlayListFile(file))));
                    }
                }
            }
        }

        public RelayCommand<SelectionChangedEventArgs> SelectionChangedAllPlaylist
        {
            get { return new RelayCommand<SelectionChangedEventArgs>(selectionChangedAllPlaylist); }
        }
        private void selectionChangedAllPlaylist(SelectionChangedEventArgs e)
        {
            ObservablePlaylist = ((OnePlaylist)((ListBox)e.OriginalSource).SelectedItem).ThePlaylist;
            PlaylistNameString = ((OnePlaylist)((ListBox)e.OriginalSource).SelectedItem).Name;
        }

        public Playlist()
        {
            CreateVisibility = Visibility.Hidden;
        }
    }
}
