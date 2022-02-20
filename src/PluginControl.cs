using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Platform;
using Avalonia.Styling;
using NP.IoCy;
using NP.Utilities.PluginUtils;
using System;

namespace NP.Avalonia.Gidon
{
    public class PluginControl : ContentControl, IStyleable
    {
        #region TheContainer Styled Avalonia Property
        public IoCContainer TheContainer
        {
            get { return GetValue(TheContainerProperty); }
            set { SetValue(TheContainerProperty, value); }
        }

        public static readonly StyledProperty<IoCContainer> TheContainerProperty =
            AvaloniaProperty.Register<PluginControl, IoCContainer>
            (
                nameof(TheContainer)
            );
        #endregion TheContainer Styled Avalonia Property

        Type IStyleable.StyleKey => typeof(ContentControl);

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

        private void OnContainerChanged(IoCContainer container)
        {
            ResetPlugin();
        }

        private void OnPluginInfoPropertyChanged(VisualPluginInfo pluginInfo)
        {
            ResetPlugin();
        }

        private void ResetPlugin()
        {
            IoCContainer container = this.TheContainer;

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
