using Avalonia;
using Avalonia.Controls;
using NP.Avalonia.Visuals.WindowsOnly;
using NP.Grpc.CommonRelayInterfaces;
using System;

namespace NP.Avalonia.Gidon
{
    public static class WinProcessAttachedProperties
    {

        #region RelayClient Attached Avalonia Property
        public static IRelayClient GetRelayClient(ImplantedWindowHostContainer obj)
        {
            return obj.GetValue(RelayClientProperty);
        }

        public static void SetRelayClient(ImplantedWindowHostContainer obj, IRelayClient value)
        {
            obj.SetValue(RelayClientProperty, value);
        }

        public static readonly AttachedProperty<IRelayClient> RelayClientProperty =
            AvaloniaProperty.RegisterAttached<ImplantedWindowHostContainer, ImplantedWindowHostContainer, IRelayClient>
            (
                "RelayClient"
            );

        #endregion RelayClient Attached Avalonia Property

        static WinProcessAttachedProperties()
        {
            RelayClientProperty.Changed.Subscribe(OnRelayClientChanged);
        }

        private static void OnRelayClientChanged(AvaloniaPropertyChangedEventArgs<IRelayClient> changedArgs)
        {
            ImplantedWindowHostContainer implantedWindowHostContainer = 
                (ImplantedWindowHostContainer) changedArgs.Sender;    

            if (implantedWindowHostContainer != null)
            {

            }    
        }


    }
}
