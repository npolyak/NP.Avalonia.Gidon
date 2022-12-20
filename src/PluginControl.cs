using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Styling;
using NP.DependencyInjection.Interfaces;
using NP.IoCy;
using NP.Utilities.PluginUtils;
using System;

namespace NP.Avalonia.Gidon
{
    public class PluginControl : ContentPresenter
    {
        #region TheContainer Styled Avalonia Property
        public IDependencyInjectionContainer TheContainer
        {
            get { return GetValue(TheContainerProperty); }
            set { SetValue(TheContainerProperty, value); }
        }

        public static readonly StyledProperty<IDependencyInjectionContainer> TheContainerProperty =
            AvaloniaProperty.Register<PluginControl, IDependencyInjectionContainer>
            (
                nameof(TheContainer)
            );
        #endregion TheContainer Styled Avalonia Property

        #region PluginInfo Styled Avalonia Property
        public VisualPluginInfo PluginInfo
        {
            get { return GetValue(PluginInfoProperty); }
            set { SetValue(PluginInfoProperty, value); }
        }

        public static readonly StyledProperty<VisualPluginInfo> PluginInfoProperty =
            AvaloniaProperty.Register<PluginControl, VisualPluginInfo>
            (
                nameof(PluginInfo)
            );
        #endregion PluginInfo Styled Avalonia Property

        public PluginControl()
        {
            this.GetObservable(TheContainerProperty).Subscribe(OnContainerChanged);
            this.GetObservable(PluginInfoProperty).Subscribe(OnPluginInfoPropertyChanged);
        }

        private void OnContainerChanged(IDependencyInjectionContainer container)
        {
            ResetPlugin();
        }

        private void OnPluginInfoPropertyChanged(VisualPluginInfo pluginInfo)
        {
            ResetPlugin();
        }

        private void ResetPlugin()
        {
            IDependencyInjectionContainer container = this.TheContainer;

            VisualPluginInfo pluginInfo = this.PluginInfo;

            if (container == null || pluginInfo == null)
                return;

            if (pluginInfo.ViewModelType != null)
            {
                object viewModel =
                    container.Resolve(pluginInfo.ViewModelType, pluginInfo.ViewModelKey);

                this.Content = viewModel;
            }

            IResourceDictionary resourceDictionary =
                (IResourceDictionary)AvaloniaXamlLoader.Load(new Uri(pluginInfo.ViewDataTemplateResourcePath));

            this.ContentTemplate = (DataTemplate)resourceDictionary[pluginInfo.ViewDataTemplateResourceKey]!;
        }
    }
}
