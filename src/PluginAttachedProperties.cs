using Avalonia;
using Avalonia.Controls;
using NP.IoCy;
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
        public static ViewModelPluginInfo GetPluginVmInfo(IControl obj)
        {
            return obj.GetValue(PluginVmInfoProperty);
        }

        public static void SetPluginVmInfo(IControl obj, ViewModelPluginInfo value)
        {
            obj.SetValue(PluginVmInfoProperty, value);
        }

        public static readonly AttachedProperty<ViewModelPluginInfo> PluginVmInfoProperty =
            AvaloniaProperty.RegisterAttached<PluginAttachedProperties, IControl, ViewModelPluginInfo>
            (
                "PluginVmInfo"
            );
        #endregion PluginVmInfo Attached Avalonia Property


        #region TheContainer Attached Avalonia Property
        public static IDependencyInjectionContainer GetTheContainer(IControl obj)
        {
            return obj.GetValue(TheContainerProperty);
        }

        public static void SetTheContainer(IControl obj, IDependencyInjectionContainer value)
        {
            obj.SetValue(TheContainerProperty, value);
        }

        public static readonly AttachedProperty<IDependencyInjectionContainer> TheContainerProperty =
            AvaloniaProperty.RegisterAttached<PluginAttachedProperties, IControl, IDependencyInjectionContainer>
            (
                "TheContainer"
            );
        #endregion TheContainer Attached Avalonia Property


        static PluginAttachedProperties()
        {
            PluginVmInfoProperty.Changed.Subscribe(OnPluginVmInfoChanged);

            TheContainerProperty.Changed.Subscribe(OnContainerChanged);
        }

        private static void OnPluginVmInfoChanged(AvaloniaPropertyChangedEventArgs<ViewModelPluginInfo> args)
        {
            IControl sender = (IControl)args.Sender;
            ResetPluginDataContext(sender);
        }


        private static void OnContainerChanged(AvaloniaPropertyChangedEventArgs<IDependencyInjectionContainer> args)
        {
            IControl sender = (IControl) args.Sender;
            ResetPluginDataContext(sender);
        }

        private static void ResetPluginDataContext(IControl control)
        {
            IDependencyInjectionContainer container = GetTheContainer(control);
            ViewModelPluginInfo pluginInfo = GetPluginVmInfo(control); 

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
