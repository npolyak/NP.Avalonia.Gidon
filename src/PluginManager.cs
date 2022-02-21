using NP.IoCy;
using System;

namespace NP.Avalonia.Gidon
{
    public class PluginManager
    {
        public IoCContainer TheContainer { get; } = new IoCContainer();

        public string ServicesPath { get; }
        public string ViewModelsPath { get; }
        public string ViewsPath { get; }

        public PluginManager
        (
            string servicesPath = "Plugins/Services", 
            string viewModelsPath = "Plugins/ViewModelPlugins", 
            string viewsPath = "Plugins/ViewPlugins")
        {
            ServicesPath = servicesPath;
            ViewModelsPath = viewModelsPath;
            ViewsPath = viewsPath;
        }

        public void InjectType(Type typeToInject)
        {
            TheContainer.InjectType(typeToInject);
        }

        public void CompleteConfiguration()
        {
            TheContainer.InjectPluginsFromSubFolders(ServicesPath);
            TheContainer.InjectPluginsFromSubFolders(ViewModelsPath);
            TheContainer.InjectPluginsFromSubFolders(ViewsPath);

            TheContainer.CompleteConfiguration();
        }
    }
}
