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
        [Part]
        public IAuthenticationService? TheAuthenticationService
        {
            get;
            private set;
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
            UserName != null && Password != null;

        public void Authenticate()
        {
            TheAuthenticationService?.Authenticate(UserName, Password);
        }
    }
}