using AuthenticationViewModelPlugin;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using MockAuthentication;
using NP.DependencyInjection.Interfaces;
using NP.IoCy;
using NP.Utilities.PluginUtils;

namespace AuthenticationPluginTest
{
    public class App : Application
    {
        public static IDependencyInjectionContainer TheContainer { get; }

        public static AuthenticationViewModel TheViewModel { get; }

        static App()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterAssembly(typeof(MockAuthenticationService).Assembly);

            containerBuilder.RegisterAssembly(typeof(AuthenticationViewModel).Assembly);

            TheContainer = containerBuilder.Build();

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
