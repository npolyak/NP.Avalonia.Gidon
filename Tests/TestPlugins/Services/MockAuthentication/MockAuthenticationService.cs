using NP.Utilities;
using NP.Utilities.Attributes;
using NP.Utilities.BasicServices;
using System.Security;

namespace MockAuthentication
{
    [Implements(typeof(IAuthenticationService), IsSingleton = true)]
    public class MockAuthenticationService : VMBase, IAuthenticationService
    {
        [Part]
        private ILog? Log { get; set; }

        private IAuthenticationService Intrfc => this;

        private string? _currentUserName;
        public string? CurrentUserName 
        { 
            get => _currentUserName;
            private set
            {
                if (_currentUserName == value)
                    return;

                _currentUserName = value;

                OnPropertyChanged(nameof(CurrentUserName));
                OnPropertyChanged(nameof(IAuthenticationService.IsAuthenticated));
            }
        }

        public bool Authenticate(string userName, string password)
        {
            if (Intrfc.IsAuthenticated)
            {
                throw new Exception("Already Authenticated");
            }

            CurrentUserName =
                 (userName == "nick" && password == "1234") ? userName : null;

            if (Intrfc.IsAuthenticated)
            {
                Log?.Log
                (
                    LogKind.Info,
                    nameof(IAuthenticationService),
                    $"Authenticated user '{userName}'");
            }

            return Intrfc.IsAuthenticated;
        }

        public void Logout()
        {
            if (!Intrfc.IsAuthenticated)
            {
                throw new Exception("Already logged out");
            }

            CurrentUserName = null;
        }
    }
}