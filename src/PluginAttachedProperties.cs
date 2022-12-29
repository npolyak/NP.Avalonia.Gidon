using Avalonia;
using NP.Utilities.PluginUtils;
using NP.DependencyInjection.Interfaces;
using System;

namespace NP.Avalonia.Gidon
{
    public class PluginAttachedProperties<TKey>
    {
        #region PluginDataContext Attached Avalonia Property
        public static object? GetPluginDataContext(AvaloniaObject obj)
        {
            return obj.GetValue(PluginDataContextProperty);
        }

        private static void SetPluginDataContext(AvaloniaObject obj, object? value)
        {
            obj.SetValue(PluginDataContextProperty, value);
        }

        public static readonly AttachedProperty<object?> PluginDataContextProperty =
            AvaloniaProperty.RegisterAttached<PluginAttachedProperties<TKey>, AvaloniaObject, object?>
            (
                "PluginDataContext"
            );
        #endregion PluginDataContext Attached Avalonia Property


        #region PluginVmInfo Attached Avalonia Property
        public static ViewModelPluginInfo<TKey>? GetPluginVmInfo(AvaloniaObject obj)
        {
            return obj.GetValue(PluginVmInfoProperty);
        }

        public static void SetPluginVmInfo(AvaloniaObject obj, ViewModelPluginInfo<TKey>? value)
        {
            obj.SetValue(PluginVmInfoProperty, value);
        }

        public static readonly AttachedProperty<ViewModelPluginInfo<TKey>?> PluginVmInfoProperty =
            AvaloniaProperty.RegisterAttached<PluginAttachedProperties<TKey>, AvaloniaObject, ViewModelPluginInfo<TKey>?>
            (
                "PluginVmInfo"
            );
        #endregion PluginVmInfo Attached Avalonia Property


        #region TheContainer Attached Avalonia Property
        public static IDependencyInjectionContainer<TKey> GetTheContainer(AvaloniaObject obj)
        {
            return obj.GetValue(TheContainerProperty);
        }

        public static void SetTheContainer(AvaloniaObject obj, IDependencyInjectionContainer<TKey> value)
        {
            obj.SetValue(TheContainerProperty, value);
        }

        public static readonly AttachedProperty<IDependencyInjectionContainer<TKey>> TheContainerProperty =
            AvaloniaProperty.RegisterAttached
            <
                PluginAttachedProperties<TKey>,
                AvaloniaObject,
                IDependencyInjectionContainer<TKey>>
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
            AvaloniaPropertyChangedEventArgs<ViewModelPluginInfo<TKey>?> args)
        {
            AvaloniaObject sender = (AvaloniaObject)args.Sender;
            ResetPluginDataContext(sender);
        }


        private static void OnContainerChanged
        (
            AvaloniaPropertyChangedEventArgs<IDependencyInjectionContainer<TKey>> args)
        {
            AvaloniaObject sender = (AvaloniaObject)args.Sender;
            ResetPluginDataContext(sender);
        }

        private static void ResetPluginDataContext(AvaloniaObject control)
        {
            IDependencyInjectionContainer<TKey> container = GetTheContainer(control);
            ViewModelPluginInfo<TKey>? pluginInfo = GetPluginVmInfo(control);

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

    public class PluginAttachedProperties : PluginAttachedProperties<object?>
    {
        public static new object? GetPluginDataContext(AvaloniaObject obj)
        {
            return obj.GetValue(PluginDataContextProperty);
        }

        public static new ViewModelPluginInfo<object?>? GetPluginVmInfo(AvaloniaObject obj)
        {
            return obj.GetValue(PluginVmInfoProperty);
        }

        public static new void SetPluginVmInfo(AvaloniaObject obj, ViewModelPluginInfo<object?>? value)
        {
            obj.SetValue(PluginVmInfoProperty, value);
        }

        public static new IDependencyInjectionContainer<object?> GetTheContainer(AvaloniaObject obj)
        {
            return obj.GetValue(TheContainerProperty);
        }

        public static new void SetTheContainer(AvaloniaObject obj, IDependencyInjectionContainer<object?> value)
        {
            obj.SetValue(TheContainerProperty, value);
        }
    }

    public class PluginAttachedPropertiesStr : PluginAttachedProperties<string?>
    {
        public static new object? GetPluginDataContext(AvaloniaObject obj)
        {
            return obj.GetValue(PluginDataContextProperty);
        }

        public static new ViewModelPluginInfo<string?>? GetPluginVmInfo(AvaloniaObject obj)
        {
            return obj.GetValue(PluginVmInfoProperty);
        }

        public static new void SetPluginVmInfo(AvaloniaObject obj, ViewModelPluginInfo<string?>? value)
        {
            obj.SetValue(PluginVmInfoProperty, value);
        }

        public static new IDependencyInjectionContainer<string?> GetTheContainer(AvaloniaObject obj)
        {
            return obj.GetValue(TheContainerProperty);
        }

        public static new void SetTheContainer(AvaloniaObject obj, IDependencyInjectionContainer<string?> value)
        {
            obj.SetValue(TheContainerProperty, value);
        }
    }
}
