using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using NP.Avalonia.Gidon;
using NP.IoCy;
using NP.NLogAdapter;

namespace PluginsTest
{
    public class App : Application
    {
        public static PluginManager ThePluginManager { get; } = new PluginManager();

        public App()
        {

            ThePluginManager.InjectType(typeof(NLogWrapper));
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
