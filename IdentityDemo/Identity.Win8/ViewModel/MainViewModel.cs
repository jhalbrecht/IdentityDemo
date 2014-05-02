using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppDevPro.Utility.Pcl;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Identity.Win8.Common;
using Identity.Win8.Model;

namespace Identity.Win8.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// The <see cref="WelcomeTitle" /> property's name.
        /// </summary>
        public const string WelcomeTitlePropertyName = "WelcomeTitle";

        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;

        private RelayCommand _navigateCommand;
        private string _originalTitle;
        private string _welcomeTitle = string.Empty;
        public ObservableCollection<string> Strings { get; set; }

        /// <summary>
        /// Gets the NavigateCommand.
        /// </summary>
        public RelayCommand NavigateCommand
        {
            get
            {
                return _navigateCommand
                       ?? (_navigateCommand = new RelayCommand(
                           () => _navigationService.Navigate(typeof(SecondPage))));
            }
        }

        private RelayCommand _refreshButtonCommand;
        /// <summary>
        /// Gets the RefreshButtonCommand.
        /// </summary>
        public RelayCommand RefreshButtonCommand
        {
            get
            {
                return _refreshButtonCommand
                    ?? (_refreshButtonCommand = new RelayCommand(
                                          async () =>
                                          {
                                              Logger.Log(this, "RelayCommand RefreshButtonCommand");
                                              await Initialize();
                                          }));
            }
        }

        private RelayCommand _postButtonCommand;
        /// <summary>
        /// Gets the PostButtonCommand.
        /// </summary>
        public RelayCommand PostButtonCommand
        {
            get
            {
                return _postButtonCommand
                    ?? (_postButtonCommand = new RelayCommand(
                                          async () =>
                                          {
                                              Logger.Log(this, "RelayCommand PostButtonCommand");
                                              // await Initialize();
                                          }));
            }
        }

        private RelayCommand _filePostCommand;

        /// <summary>
        /// Gets the FilePostCommand.
        /// </summary>
        public RelayCommand FilePostCommand
        {
            get
            {
                return _filePostCommand
                    ?? (_filePostCommand = new RelayCommand(
                                          async () =>
                                          {
                                              //var well = await _dataService.FilePostSfx();

                                          }));
            }
        }

        /// <summary>
        /// Gets the WelcomeTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string WelcomeTitle
        {
            get
            {
                return _welcomeTitle;
            }

            set
            {
                Set(ref _welcomeTitle, value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(
            IDataService dataService,
            INavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
            Initialize();
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
        public void Load(DateTime lastVisit)
        {
            if (lastVisit > DateTime.MinValue)
            {
                WelcomeTitle = string.Format(
                    "{0} (last visit on the {1})",
                    _originalTitle,
                    lastVisit);
            }
        }

        private async Task Initialize()
        {
            try
            {
                var item = await _dataService.GetData();
                _originalTitle = item.Title;
                WelcomeTitle = item.Title;
                var strings = await _dataService.GetValues(); 
                Strings = new ObservableCollection<string>(strings);
            }
            catch (Exception ex)
            {
                // Report error here
            }
        }
    }
}