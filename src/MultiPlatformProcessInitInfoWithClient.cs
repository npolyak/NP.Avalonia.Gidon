using NP.Avalonia.Visuals.Behaviors;
using NP.Grpc.CommonRelayInterfaces;

namespace NP.Avalonia.Gidon
{
    public class MultiPlatformProcessInitInfoWithClient : MultiPlatformProcessInitInfo
    {
        public IRelayClient? TheRelayClient { get; set; }

        public string? UniqueWindowHostId { get; set; }
    }
}
