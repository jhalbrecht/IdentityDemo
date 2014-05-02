using AppDevPro.Utility.Pcl;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Identity.Demo.Pcl;
using Identity.Win8.Model;

namespace Identity.Win8.ViewModel
{
    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class SecondViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private readonly IIdentityService _identityService;
        private RelayCommand _LoginCommand;
        private RelayCommand _RegisterCommand;
        private Configuration _configuration;

        /// <summary>
        ///     Initializes a new instance of the SecondViewModel class.
        /// </summary>
        public SecondViewModel(IDataService dataService, IIdentityService identityService)
        {
            UserName = "Alice@somewhere.com";
            Password = "Pw4Ta;TooUse";
            _dataService = dataService;
            _identityService = identityService;
            _configuration = new Configuration();
        }

        public string HowdySecondPage { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AccessToken { get; set; }

        public RelayCommand RegisterCommand
        {
            get
            {
                return _RegisterCommand
                       ?? (_RegisterCommand = new RelayCommand(
                           async () =>
                           {
                               Logger.Log(this, "I'm trying to RegisterUser Jim!",
                                   string.Format("name {0}, password: {1}", UserName, Password));
                               string foo = await _identityService.RegisterUser(UserName, Password);
                               Logger.Log(this, "returned from register:", foo);
                           }));
            }
        }

        public RelayCommand LoginCommand
        {
            get
            {
                return _LoginCommand
                       ?? (_LoginCommand = new RelayCommand(
                           async () =>
                           {
                               Logger.Log(this, "I'm trying to logon Jim!",
                                   string.Format("name {0}, password: {1}", UserName, Password));
                               string foo = await _identityService.LoginUser(UserName, Password);
                               // string foo = await _dataService.LoginUser(UserName, Password);
                               Logger.Log(this, "returned from login: hope it's a token", foo);
                           }));
            }
        }
    }
}