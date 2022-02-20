using NP.Utilities;
using NP.Utilities.Attributes;
using NP.Utilities.BasicServices;
using NP.Utilities.PluginUtils;
using System.ComponentModel;

namespace AuthenticationViewModelPlugin
{
    [Implements(typeof(IPlugin), partKey:"AuthenticationVM", isSingleton:true)]
    public class AuthenticationViewModel : VMBase, IPlugin
    {
        IAuthenticationService? _authenticationService;
        [Part(typeof(IAuthenticationService))]
        public IAuthenticationService? TheAuthenticationService
        {
            get => _authenticationService;
            internal set
            {
                if (_authenticationService == value)
                    return;

                if (_authenticationService != null)
                {
                    _authenticationService.PropertyChanged -= _authenticationService_PropertyChanged;
                }

                _authenticationService = value;

                if (_authenticationService != null)
                {
                    _authenticationService.PropertyChanged += _authenticationService_PropertyChanged;
                }
            }
        }

        private void _authenticationService_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is (nameof(UserName) or nameof(Password)))
            {
                OnPropertyChanged(nameof(CanAuthenticate));
            }
        }

        #region UserName Property
        private string? _userName;
        public string? UserName
        {
            get
            {
                return this._userName;
            }
            set
            {
                if (this._userName == value)
                {
                    return;
                }

                this._userName = value;
                this.OnPropertyChanged(nameof(UserName));
                this.OnPropertyChanged(nameof(CanAuthenticate));
            }
        }
        #endregion UserName Property


        #region Password Property
        private string? _password;
        public string? Password
        {
            get
            {
                return this._password;
            }
            set
            {
                if (this._password == value)
                {
                    return;
                }

                this._password = value;
                this.OnPropertyChanged(nameof(Password));
                this.OnPropertyChanged(nameof(CanAuthenticate));
            }
        }
        #endregion Password Property

        public bool CanAuthenticate =>
            (!string.IsNullOrEmpty(UserName)) && (!string.IsNullOrEmpty(Password));

        public void Authenticate()
        {
            TheAuthenticationService?.Authenticate(UserName, Password);

            OnPropertyChanged(nameof(IsAuthenticated)); 
        }

        public void ExitApplication()
        {
            Environment.Exit(0);
        }

        public bool IsAuthenticated => TheAuthenticationService?.IsAuthenticated ?? false;
    }
}