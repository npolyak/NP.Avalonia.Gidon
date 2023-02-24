using Avalonia.Threading;
using NP.Avalonia.Visuals.Behaviors;
using NP.Concepts.Behaviors;
using NP.Gidon.Messages;
using NP.Grpc.CommonRelayInterfaces;
using NP.IoC.CommonImplementations;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime;

namespace NP.Avalonia.Gidon
{
    public class WindowHandleMatcher : ObservableCollection<WindowInfo> 
    {
        IRelayClient _relayClient;

        IDisposable _subscription;

        public event Action<WindowInfo> WinInfoArrivedChangedEvent;

        public WindowHandleMatcher(IRelayClient relayClient)
        {
            _relayClient = relayClient;

            _subscription = 
                _relayClient.ObserveTopicStream<WindowInfo>(Topic.WindowInfoTopic)
                            .ObserveOn(AvaloniaScheduler.Instance)
                            .Subscribe(OnWindowInfoArrived);
        }

        private void OnWindowInfoArrived(WindowInfo windowInfo)
        {
            if (this.Count(cell => cell.UniqueWindowHostId == windowInfo.UniqueWindowHostId) > 0)
            {
                $"Programming Error: There are more than 1 cells with UniqueWindowHostId '{windowInfo.UniqueWindowHostId}'"
                    .ThrowProgError();
            }

            WinInfoArrivedChangedEvent.Invoke(windowInfo);
        }

        public WindowInfo? GetCellForUniqueWindowHostId(string uniqueWindowHostId)
        {
            return this.SingleOrDefault(cell => cell.UniqueWindowHostId == uniqueWindowHostId);
        }
    }
}
