using NP.IoCy;
using System;
using System.IO;

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

        private void InjectPluginsFromSubFolderIntoContainer(string subFolderPath)
        {
            if (!Directory.Exists(subFolderPath))
            {
                return;
            }

            TheContainer.InjectPluginsFromSubFolders(subFolderPath);
        }

        public void CompleteConfiguration()
        {
            InjectPluginsFromSubFolderIntoContainer(ServicesPath);
            InjectPluginsFromSubFolderIntoContainer(ViewModelsPath);
            InjectPluginsFromSubFolderIntoContainer(ViewsPath);

            TheContainer.CompleteConfiguration();
        }
    }
}
