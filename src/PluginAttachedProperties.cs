using Avalonia;
using Avalonia.Controls;
using NP.Utilities.PluginUtils;
using NP.DependencyInjection.Interfaces;
using System;

namespace NP.Avalonia.Gidon
{
    public class PluginAttachedProperties
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
            AvaloniaProperty.RegisterAttached<PluginAttachedProperties, IControl, object?>
            (
                "PluginDataContext"
            );
        #endregion PluginDataContext Attached Avalonia Property


        #region PluginVmInfo Attached Avalonia Property
        public static ViewModelPluginInfo<object?> GetPluginVmInfo(IControl obj)
        {
            return obj.GetValue(PluginVmInfoProperty);
        }

        public static void SetPluginVmInfo(IControl obj, ViewModelPluginInfo<object?> value)
        {
            obj.SetValue(PluginVmInfoProperty, value);
        }

        public static readonly AttachedProperty<ViewModelPluginInfo<object?>> PluginVmInfoProperty =
            AvaloniaProperty.RegisterAttached<PluginAttachedProperties, IControl, ViewModelPluginInfo<object?>>
            (
                "PluginVmInfo"
            );
        #endregion PluginVmInfo Attached Avalonia Property


        #region TheContainer Attached Avalonia Property
        public static IDependencyInjectionContainer<object?> GetTheContainer(IControl obj)
        {
            return obj.GetValue(TheContainerProperty);
        }

        public static void SetTheContainer(IControl obj, IDependencyInjectionContainer<object?> value)
        {
            obj.SetValue(TheContainerProperty, value);
        }

        public static readonly AttachedProperty<IDependencyInjectionContainer<object?>> TheContainerProperty =
            AvaloniaProperty.RegisterAttached
            <
                PluginAttachedProperties,
                IControl,
                IDependencyInjectionContainer<object?>>
            (
                "TheContainer");
        #endregion TheContainer Attached Avalonia Property


        static PluginAttachedProperties()
        {
            PluginVmInfoProperty.Changed.Subscribe(OnPluginVmInfoChanged);

            TheContainerProperty.Changed.Subscribe(OnContainerChanged);
        }

        private static void OnPluginVmInfoChanged
        (
            AvaloniaPropertyChangedEventArgs<ViewModelPluginInfo<object?>> args)
        {
            IControl sender = (IControl)args.Sender;
            ResetPluginDataContext(sender);
        }


        private static void OnContainerChanged
        (
            AvaloniaPropertyChangedEventArgs<IDependencyInjectionContainer<object?>> args)
        {
            IControl sender = (IControl)args.Sender;
            ResetPluginDataContext(sender);
        }

        private static void ResetPluginDataContext(IControl control)
        {
            IDependencyInjectionContainer<object?> container = GetTheContainer(control);
            ViewModelPluginInfo<object?> pluginInfo = GetPluginVmInfo(control);

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
