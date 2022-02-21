using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using NP.IoCy;
using NP.NLogAdapter;

namespace PluginsTest
{
    public class App : Application
    {
        public static IoCContainer? TheContainer { get; private set; }

        public App()
        {
            App.TheContainer = new IoCContainer();

            TheContainer.InjectType(typeof(NLogWrapper));
            TheContainer.InjectPluginsFromFolder("Plugins/Services/MockAuthentication");
            TheContainer.InjectPluginsFromFolder("Plugins/Services/TextService");
            TheContainer.InjectPluginsFromFolder("Plugins/ViewModelPlugins/AuthenticationViewModelPlugin");
            TheContainer.InjectPluginsFromFolder("Plugins/ViewPlugins/AuthenticationViewPlugin");

            TheContainer.InjectPluginsFromFolder("Plugins/ViewModelPlugins/EnterTextViewModelPlugin");
            TheContainer.InjectPluginsFromFolder("Plugins/ViewPlugins/EnterTextViewPlugin");

            //TheContainer.InjectPluginsFromFolder("Plugins/ViewModelPlugins/AuthenticationViewModelPlugin");
            //TheContainer.InjectPluginsFromFolder("Plugins/ViewPlugins/AuthenticationViewPlugin");

            TheContainer.CompleteConfiguration();
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
