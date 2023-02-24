using NP.Avalonia.Visuals.Behaviors;
using NP.Grpc.CommonRelayInterfaces;

namespace NP.Avalonia.Gidon
{
    public class MultiPlatformProcessInitInfoWithMatcher: MultiPlatformProcessInitInfo
    {
        public WindowHandleMatcher? TheWindowHandleMatcher { get; set; }

        public string? UniqueWindowHostId { get; set; }
    }
}
