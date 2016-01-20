using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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

    public delegate void LibraryMediaDoubleClickHandler(Media media);

    public class LibraryViewModel : ViewModelBase
    {

        public static event LibraryMediaDoubleClickHandler OnMediaDoubleClick = delegate { };

        private int _selectedIndex = -1;
        private Point _startDrag;

        private MediaLibrary _mediaManager = null;

        private ObservableCollection<Media> _libraryAllMedia = null;
        public ObservableCollection<Media> LibraryAllMedia
        {
            get { return _libraryAllMedia; }

            set
            {
                _libraryAllMedia = value;
                RaisePropertyChanged("LibraryAllMedia");
            }
        }

        public ICommand DelAllMediaLibrary
        {
            get { return new DelegateCommand(delAllMediaLibrary); }
        }
        private void delAllMediaLibrary()
        {
            if (MessageBox.Show("Do you really want to delete your library ?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                _mediaManager.DeleteLibrary();
        }

        public ICommand DelMediaLibrary
        {
            get { return new DelegateCommand(delMediaLibrary); }
        }
        private void delMediaLibrary()
        {
            if (_selectedIndex != -1)
            {
                _mediaManager.Delete(LibraryAllMedia[_selectedIndex]);
            }
        }

        public RelayCommand<SelectionChangedEventArgs> SelectionChangedLibrary
        {
            get { return new RelayCommand<SelectionChangedEventArgs>(selectionChangedLibrary); }
        }
        private void selectionChangedLibrary(SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
                _selectedIndex = ((ListBox)e.OriginalSource).SelectedIndex;
            else
                _selectedIndex = -1;
        }

        public RelayCommand<MouseButtonEventArgs> DoubleClickMediaLibrary
        {
            get { return new RelayCommand<MouseButtonEventArgs>(doubleClickMediaLibrary); }
        }
        private void doubleClickMediaLibrary(MouseButtonEventArgs e)
        {
            if (_selectedIndex != -1)
                OnMediaDoubleClick(LibraryAllMedia[_selectedIndex]);
        }

        public RelayCommand<MouseButtonEventArgs> MouseLeftButtonDownLibrary
        {
            get { return new RelayCommand<MouseButtonEventArgs>(mouseLeftButtonDownLibrary); }
        }
        private void mouseLeftButtonDownLibrary(MouseButtonEventArgs e)
        {
            _startDrag = e.GetPosition(null);
        }

        public RelayCommand<MouseEventArgs> MouseMoveLibrary
        {
            get { return new RelayCommand<MouseEventArgs>(mouseMoveLibrary); }
        }
        private void mouseMoveLibrary(MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = _startDrag - mousePos;
            if (e.OriginalSource is System.Windows.Controls.Primitives.Thumb)
                return;
            if (e.LeftButton == MouseButtonState.Pressed && (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance || Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                List<Media> mediaSelected = new List<Media>();

                foreach (Media item in ((ListBox)e.Source).SelectedItems)
                {
                    mediaSelected.Add(item);
                }

                try
                {
                    DataObject dragData = new DataObject("MediaFormat", mediaSelected);
                    DragDrop.DoDragDrop((DependencyObject)e.Source, dragData, DragDropEffects.Move);
                }
                catch (Exception ex)
                {
                    Debug.Add(ex.ToString());
                }
            }
        }

        public RelayCommand<DragEventArgs> LibraryDragEnter
        {
            get { return new RelayCommand<DragEventArgs>(libraryDragEnter); }
        }
        private void libraryDragEnter(DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.None;
            }
        }

        public RelayCommand<DragEventArgs> LibraryDrop
        {
            get { return new RelayCommand<DragEventArgs>(libraryDrop); }
        }
        private void libraryDrop(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (string file in files)
                {
                    if (_mediaManager.Library == null)
                    {
                        _mediaManager.CreateLibrary(file);
                    }
                    else
                        _mediaManager.AddToLibrary(file);
                }
            }
            if (e.Data.GetDataPresent("MediaFormat"))
            {
                List<Media> medias = (List<Media>)e.Data.GetData("MediaFormat");

                foreach (Media media in medias)
                {
                    try
                    {
                        if (_mediaManager.Library == null)
                        {
                            _mediaManager.CreateLibrary(media.Path);
                        }
                        else
                            _mediaManager.AddToLibrary(media.Path);
                    }
                    catch (InvalidMediaException ex)
                    {
                        Debug.Add(ex.ToString() + "\n");
                    }
                }
            }
        }

        public LibraryViewModel()
        {
            if (!ViewModelBase.IsInDesignModeStatic)
                _mediaManager = new MediaLibrary();
            MediaLibrary.OnLibraryLoaded += loadLibrary;
        }

        private void loadLibrary(List<Media> library)
        {
            LibraryAllMedia = new ObservableCollection<Media>(library);
        }
    }
}
