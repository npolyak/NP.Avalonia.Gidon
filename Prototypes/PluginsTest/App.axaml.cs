using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Common;
using NP.Avalonia.Gidon;
using NP.DependencyInjection.Interfaces;
using NP.NLogAdapter;
using NP.Utilities.PluginUtils;
using System;

namespace PluginsTest
{
    public class App : Application
    {
        /// defined the Gidon plugin manager
        /// use the following paths (relative to the PluginsTest.exe executable)
        /// to dynamically load the plugins and services:
        /// "Plugins/Services" - to load the services (non-visual singletons)
        /// "Plugins/ViewModelPlugins" - to load view model plugins
        /// "Plugins/ViewPlugins" - to load view plugins
        public static PluginManager<Enum> ThePluginManager { get; } = 
            new PluginManager<Enum>
            (
                "Plugins/Services", 
                "Plugins/ViewModelPlugins", 
                "Plugins/ViewPlugins");

        // the IoC container
        public static IDependencyInjectionContainer<Enum> TheContainer => ThePluginManager.TheContainer;

        public App()
        {
            // inject a type from a statically loaded project NLogAdapter
            ThePluginManager.InjectType(typeof(NLogWrapper));

            // inject all dynamically loaded assemblies
            ThePluginManager.CompleteConfiguration();
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
