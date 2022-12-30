using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Templates;
using NP.Avalonia.Gidon;
using NP.DependencyInjection.Interfaces;
using NP.Utilities.PluginUtils;
using System;

namespace NP.Avalonia.Gidon
{
    public class PluginControl<TKey> : ContentPresenter
    {
        #region TheContainer Styled Avalonia Property
        public IDependencyInjectionContainer<TKey> TheContainer
        {
            get { return GetValue(TheContainerProperty); }
            set { SetValue(TheContainerProperty, value); }
        }

        public static readonly StyledProperty<IDependencyInjectionContainer<TKey>> TheContainerProperty =
            AvaloniaProperty.Register<PluginControl<TKey>, IDependencyInjectionContainer<TKey>>
            (
                nameof(TheContainer)
            );
        #endregion TheContainer Styled Avalonia Property

        #region PluginInfo Styled Avalonia Property
        public VisualPluginInfo<TKey> PluginInfo
        {
            get { return GetValue(PluginInfoProperty); }
            set { SetValue(PluginInfoProperty, value); }
        }

        public static readonly StyledProperty<VisualPluginInfo<TKey>> PluginInfoProperty =
            AvaloniaProperty.Register<PluginControl<TKey>, VisualPluginInfo<TKey>>
            (
                nameof(PluginInfo)
            );
        #endregion PluginInfo Styled Avalonia Property

        public PluginControl()
        {
            this.GetObservable(TheContainerProperty).Subscribe(OnContainerChanged);
            this.GetObservable(PluginInfoProperty).Subscribe(OnPluginInfoPropertyChanged);
        }

        private void OnContainerChanged(IDependencyInjectionContainer<TKey> container)
        {
            ResetPlugin();
        }

        private void OnPluginInfoPropertyChanged(VisualPluginInfo<TKey> pluginInfo)
        {
            ResetPlugin();
        }

        private void ResetPlugin()
        {
            IDependencyInjectionContainer<TKey> container = this.TheContainer;

            VisualPluginInfo<TKey> pluginInfo = this.PluginInfo;

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

    public class PluginControl : PluginControl<object?>
    {

    }

    public class PluginControlStr : PluginControl<string?>
    {

    }

    public class PluginControlEnum : PluginControl<Enum>
    {

    }
}