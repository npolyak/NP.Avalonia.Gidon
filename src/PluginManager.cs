using NP.DependencyInjection.Interfaces;
using NP.IoCy;
using System;
using System.IO;

namespace NP.Avalonia.Gidon
{
    public class PluginManager<TKey>
    {
        private readonly IContainerBuilder<TKey> _containerBuilder = 
            new ContainerBuilder<TKey>();

        public IDependencyInjectionContainer<TKey> TheContainer { get; private set; }

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
            _containerBuilder.RegisterAttributedClass(typeToInject);
        }

        private void InjectPluginsFromSubFolderIntoContainer(string subFolderPath)
        {
            if (!Directory.Exists(subFolderPath))
            {
                return;
            }

            _containerBuilder.RegisterPluginsFromSubFolders(subFolderPath);
        }

        public void CompleteConfiguration()
        {
            InjectPluginsFromSubFolderIntoContainer(ServicesPath);
            InjectPluginsFromSubFolderIntoContainer(ViewModelsPath);
            InjectPluginsFromSubFolderIntoContainer(ViewsPath);

            TheContainer = _containerBuilder.Build();
        }
    }
}
