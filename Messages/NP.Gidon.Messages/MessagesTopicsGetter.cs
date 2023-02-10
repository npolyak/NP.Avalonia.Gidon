using NP.DependencyInjection.Attributes;
using NP.Protobuf;

namespace NP.Gidon.Messages
{
    [HasRegisterMethods]
    public static class MessagesTopicsGetter
    {
        [RegisterMultiCellMethod(cellType: typeof(Enum), resolutionKey: IoCKeys.Topics)]
        public static Enum GetTopics()
        {
            return Topic.WindowInfoTopic;
        }
    }
}
