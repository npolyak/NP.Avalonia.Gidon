using Avalonia;
using NP.Avalonia.Visuals.Behaviors;
using NP.Avalonia.Visuals.WindowsOnly;
using NP.Gidon.Messages;
using NP.Grpc.CommonRelayInterfaces;
using NP.IoC.CommonImplementations;
using System;
using System.Reactive.Linq;

namespace NP.Avalonia.Gidon
{
    public static class WinProcessAttachedProperties
    {
        #region TheProcessInitInfoWithClient Attached Avalonia Property
        public static MultiPlatformProcessInitInfoWithClient GetTheProcessInitInfoWithClient(ImplantedWindowHostContainer obj)
        {
            return obj.GetValue(TheProcessInitInfoWithClientProperty);
        }

        public static void SetTheProcessInitInfoWithClient(ImplantedWindowHostContainer obj, MultiPlatformProcessInitInfoWithClient value)
        {
            obj.SetValue(TheProcessInitInfoWithClientProperty, value);
        }

        public static readonly AttachedProperty<MultiPlatformProcessInitInfoWithClient> TheProcessInitInfoWithClientProperty =
            AvaloniaProperty.RegisterAttached<ImplantedWindowHostContainer, ImplantedWindowHostContainer, MultiPlatformProcessInitInfoWithClient>
            (
                "TheProcessInitInfoWithClient"
            );
        #endregion TheProcessInitInfoWithClient Attached Avalonia Property

        static WinProcessAttachedProperties()
        {
            TheProcessInitInfoWithClientProperty.Changed.Subscribe(OnProcessInitInfoWithClientChanged);
        }

        private static void OnProcessInitInfoWithClientChanged(AvaloniaPropertyChangedEventArgs<MultiPlatformProcessInitInfoWithClient> changeArgs)
        {
            var sender = (ImplantedWindowHostContainer)changeArgs.Sender;

            var mpProcInitInfo = changeArgs.NewValue.Value;

            void OnWindowInfoArrived(WindowInfo windowInfo)
            {
                if (windowInfo.UniqueWindowHostId == mpProcInitInfo.UniqueWindowHostId)
                {
                    sender.ImplantedWindowHandle = (nint)windowInfo.WindowHandle;
                }
            }

            var uniqueWindowHostId = mpProcInitInfo.UniqueWindowHostId;

            if (uniqueWindowHostId == null)
            {
                "uniqueWindowHostId should not be null".ThrowProgError();
            }

            sender.OnMultiPlatformProcInitInfoChangedWithExtraParams(mpProcInitInfo, new string[] { uniqueWindowHostId! });

            mpProcInitInfo
                .TheRelayClient
                .ObserveTopicStream<WindowInfo>(Topic.WindowInfoTopic)
                .Subscribe(OnWindowInfoArrived);
        }
    }

    public class MultiPlatformProcessInitInfoWithClient : MultiPlatformProcessInitInfo
    {
        public IRelayClient? TheRelayClient { get; set; }

        public string? UniqueWindowHostId { get; set; }
    }
}
