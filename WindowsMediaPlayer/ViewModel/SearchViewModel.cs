using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace WindowsMediaPlayer
{
    public delegate void SearchMediaDoubleClick(Media media);

    public class SearchViewModel : ObservableObject
    {
        public static event SearchMediaDoubleClick OnSearchMediaDoubleClick = delegate { };

        private AutoCompleteBox _autoCompleteItem = null;
        private readonly ObservableCollection<string> _mediaSearch = new ObservableCollection<string>();

        private IEnumerable<Media> _searchContents;
        public IEnumerable<Media> SearchContents
        {
            get { return _searchContents; }

            set
            {
                _searchContents = value;
                RaisePropertyChangedEvent("SearchContents");
            }
        }

        private Media _selectedContent;
        public Media SelectedContent
        {
            get { return _selectedContent; }

            set
            {
                _selectedContent = value;
                RaisePropertyChangedEvent("SelectedContent");
            }
        }

        private List<Media> _mediaList;
        public List<Media> MediaList
        {
            get { return _mediaList; }

            set
            {
                _mediaList = value;
                RaisePropertyChangedEvent("MediaList");
            }
        }

        private int _selectedMedia;
        public int SelectedMedia
        {
            get { return _selectedMedia; }

            set
            {
                _selectedMedia = value;

                if (SelectedMedia.Equals(0))
                {
                    SearchContents = MediaList;
                }

                if (SelectedMedia.Equals(1))
                {
                    SearchContents = from s in MediaList
                                where s.Type == MediaType.Video
                                      select s;
                }

                if (SelectedMedia.Equals(2))
                {
                    SearchContents = from s in MediaList
                                where s.Type == MediaType.Music
                                      select s;
                }

                if (SelectedMedia.Equals(3))
                {
                    SearchContents = from s in MediaList
                                where s.Type == MediaType.Picture
                                      select s;
                }
                RaisePropertyChangedEvent("SelectedMedia");
            }
        }

        private string _selectedMusicsFilter;
        public string SelectedMusicsFilter
        {
            get { return _selectedMusicsFilter; }

            set
            {
                _selectedMusicsFilter = value;
                MusicsFilter = MusicsFilter;
                RaisePropertyChangedEvent("SelectedMusicsFilter");
            }
        }

        public AutoCompleteFilterPredicate<object> MusicsFilter
        {
            get
            {
                if (SelectedMusicsFilter != null)
                {
                    if (SelectedMusicsFilter.Equals("Artist"))
                    {
                        return (searchText, obj) =>
                        (obj as Media).Artists[0].Contains(searchText);
                    }
                    if (SelectedMusicsFilter.Equals("Album"))
                    {
                        return (searchText, obj) =>
                        (obj as Media).Album.Contains(searchText);
                    }
                }
                return (searchText, obj) =>
                (obj as Media).Title.Contains(searchText);
            }
            set
            {
                MusicsFilter = value;
                RaisePropertyChangedEvent("MusicsFilter");
            }
        }

        public RelayCommand<SelectionChangedEventArgs> SelectionChangedSearch
        {
            get { return new RelayCommand<SelectionChangedEventArgs>(selectionChangedSearch); }
        }
        private void selectionChangedSearch(SelectionChangedEventArgs e)
        {
            _autoCompleteItem = (AutoCompleteBox)e.OriginalSource;
        }

        public RelayCommand<MouseButtonEventArgs> DoubleClickMediaSearch
        {
            get { return new RelayCommand<MouseButtonEventArgs>(doubleClickMediaSearch); }
        }

        private void doubleClickMediaSearch(MouseButtonEventArgs e)
        {
            if (_autoCompleteItem != null)
                OnSearchMediaDoubleClick((Media)_autoCompleteItem.SelectedItem);
        }

        public RelayCommand<KeyEventArgs> KeyUpMediaSearch
        {
            get { return new RelayCommand<KeyEventArgs>(keyUpMediaSearch); }
        }

        private void keyUpMediaSearch(KeyEventArgs e)
        {
            if (_autoCompleteItem != null)
            {
                if (e.Key == System.Windows.Input.Key.Enter)
                {
                    OnSearchMediaDoubleClick((Media)_autoCompleteItem.SelectedItem);
                }
            }
        }

        private void loadLibrary(List<Media> library)
        {
            MediaList = new List<Media>(library);
            SearchContents = MediaList;
        }

        public SearchViewModel()
        {
            MediaLibrary.OnLibraryLoaded += loadLibrary;
        }
    }
}
