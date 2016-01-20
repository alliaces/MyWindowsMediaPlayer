/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:WindowsMediaPlayer"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace WindowsMediaPlayer
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        /// 
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            CreatePlayer();
            CreatePlaylist();
            CreateLibrary();
        }

        /* START */
        private static Player _playerViewModel;
        public static Player PlayerStatic
        {
            get
            {
                if (_playerViewModel == null)
                {
                    CreatePlayer();
                }

                return _playerViewModel;
            }
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public Player PlayerViewModel
        {
            get
            {
                return PlayerStatic;
            }
        }
        public static void ClearPlayer()
        {
            _playerViewModel.Cleanup();
            _playerViewModel = null;
        }
        public static void CreatePlayer()
        {
            if (_playerViewModel == null)
            {
                _playerViewModel = new Player();
            }
        }
        /* END */

        private static Playlist _playlistViewModel;
        public static Playlist PlaylistStatic
        {
            get
            {
                if (_playlistViewModel == null)
                {
                    CreatePlaylist();
                }

                return _playlistViewModel;
            }
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public Playlist PlaylistViewModel
        {
            get
            {
                return PlaylistStatic;
            }
        }
        public static void ClearPlaylist()
        {
            _playlistViewModel.Cleanup();
            _playlistViewModel = null;
        }
        public static void CreatePlaylist()
        {
            if (_playlistViewModel == null)
            {
                _playlistViewModel = new Playlist();
            }
        }


        private static LibraryViewModel _libraryViewModel;
        public static LibraryViewModel LibraryStatic
        {
            get
            {
                if (_libraryViewModel == null)
                {
                    CreateLibrary();
                }

                return _libraryViewModel;
            }
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public LibraryViewModel LibraryVM
        {
            get
            {
                return LibraryStatic;
            }
        }
        public static void ClearLibrary()
        {
            _libraryViewModel.Cleanup();
            _libraryViewModel = null;
        }
        public static void CreateLibrary()
        {
            if (_libraryViewModel == null)
            {
                _libraryViewModel = new LibraryViewModel();
            }
        }

        public static void Cleanup()
        {
            ClearPlayer();
            ClearPlaylist();
            ClearLibrary();
        }
    }
}