using AuthenticationViewModelPlugin;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using MockAuthentication;
using NP.Avalonia.Gidon;
using NP.IoCy;
using NP.NLogAdapter;
using NP.Utilities.PluginUtils;

namespace AuthenticationPluginTest
{
    public class App : Application
    {
        public static IoCContainer TheContainer { get; } =
            new IoCContainer();

        public static AuthenticationViewModel TheViewModel { get; }

        static App()
        {
            TheContainer.InjectAssembly(typeof(MockAuthenticationService).Assembly);

            TheContainer.InjectAssembly(typeof(AuthenticationViewModel).Assembly);

            TheContainer.CompleteConfiguration();


            TheViewModel = (AuthenticationViewModel) TheContainer.Resolve(typeof(IPlugin), "AuthenticationVM");
        }

        public override void Initialize()
        {                
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {  
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
