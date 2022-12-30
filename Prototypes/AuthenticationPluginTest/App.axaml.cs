using AuthenticationViewModelPlugin;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Common;
using MockAuthentication;
using NP.DependencyInjection.Interfaces;
using NP.IoCy;
using NP.Utilities.PluginUtils;
using System;

namespace AuthenticationPluginTest
{
    public class App : Application
    {
        public static IDependencyInjectionContainer<Enum> TheContainer { get; }

        public static AuthenticationViewModel TheViewModel { get; }

        static App()
        {
            var containerBuilder = new ContainerBuilder<Enum>();

            containerBuilder.RegisterAssembly(typeof(MockAuthenticationService).Assembly);

            containerBuilder.RegisterAssembly(typeof(AuthenticationViewModel).Assembly);

            TheContainer = containerBuilder.Build();

            TheViewModel = (AuthenticationViewModel) TheContainer.Resolve(typeof(IPlugin), PluginKeys.AuthenticationVM);
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
