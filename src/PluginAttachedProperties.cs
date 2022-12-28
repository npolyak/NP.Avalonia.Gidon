using Avalonia;
using Avalonia.Controls;
using NP.Utilities.PluginUtils;
using NP.DependencyInjection.Interfaces;
using System;

namespace NP.Avalonia.Gidon
{
    public class PluginAttachedProperties<TKey>
    {
        #region PluginDataContext Attached Avalonia Property
        public static object? GetPluginDataContext(IControl obj)
        {
            return obj.GetValue(PluginDataContextProperty);
        }

        private static void SetPluginDataContext(IControl obj, object? value)
        {
            obj.SetValue(PluginDataContextProperty, value);
        }

        public static readonly AttachedProperty<object?> PluginDataContextProperty =
            AvaloniaProperty.RegisterAttached<PluginAttachedProperties<TKey>, IControl, object?>
            (
                "PluginDataContext"
            );
        #endregion PluginDataContext Attached Avalonia Property


        #region PluginVmInfo Attached Avalonia Property
        public static ViewModelPluginInfo<TKey> GetPluginVmInfo(IControl obj)
        {
            return obj.GetValue(PluginVmInfoProperty);
        }

        public static void SetPluginVmInfo(IControl obj, ViewModelPluginInfo<TKey> value)
        {
            obj.SetValue(PluginVmInfoProperty, value);
        }

        public static readonly AttachedProperty<ViewModelPluginInfo<TKey>> PluginVmInfoProperty =
            AvaloniaProperty.RegisterAttached<PluginAttachedProperties<TKey>, IControl, ViewModelPluginInfo<TKey>>
            (
                "PluginVmInfo"
            );
        #endregion PluginVmInfo Attached Avalonia Property


        #region TheContainer Attached Avalonia Property
        public static IDependencyInjectionContainer<TKey> GetTheContainer(IControl obj)
        {
            return obj.GetValue(TheContainerProperty);
        }

        public static void SetTheContainer(IControl obj, IDependencyInjectionContainer<TKey> value)
        {
            obj.SetValue(TheContainerProperty, value);
        }

        public static readonly AttachedProperty<IDependencyInjectionContainer<TKey>> TheContainerProperty =
            AvaloniaProperty.RegisterAttached
            <
                PluginAttachedProperties<TKey>, 
                IControl, 
                IDependencyInjectionContainer<TKey>>
            (
                "TheContainer"
            );
        #endregion TheContainer Attached Avalonia Property


        static PluginAttachedProperties()
        {
            PluginVmInfoProperty.Changed.Subscribe(OnPluginVmInfoChanged);

            TheContainerProperty.Changed.Subscribe(OnContainerChanged);
        }

        private static void OnPluginVmInfoChanged
        (
            AvaloniaPropertyChangedEventArgs<ViewModelPluginInfo<TKey>> args)
        {
            IControl sender = (IControl)args.Sender;
            ResetPluginDataContext(sender);
        }


        private static void OnContainerChanged
        (
            AvaloniaPropertyChangedEventArgs<IDependencyInjectionContainer<TKey>> args)
        {
            IControl sender = (IControl) args.Sender;
            ResetPluginDataContext(sender);
        }

        private static void ResetPluginDataContext(IControl control)
        {
            IDependencyInjectionContainer<TKey> container = GetTheContainer(control);
            ViewModelPluginInfo<TKey> pluginInfo = GetPluginVmInfo(control); 

            if (container == null || pluginInfo == null)
            {
                return;
            }

            object? viewModel = pluginInfo.ViewModelType == null ?
                null
                :
                container.Resolve(pluginInfo.ViewModelType, pluginInfo.ViewModelKey);
            SetPluginDataContext(control, viewModel);
        }
    }
}
