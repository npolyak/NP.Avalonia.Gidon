using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using NP.IoCy;

namespace PluginsTest
{
    public class App : Application
    {
        public static IoCContainer TheContainer { get; }

        static App()
        {
            TheContainer = new IoCContainer();

            TheContainer.InjectPluginsFromFolder("Plugins/ViewPlugins/AuthenticationView");
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
