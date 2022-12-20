using NP.DependencyInjection.Attributes;
using TestServiceInterfaces;

namespace TextService
{
    [RegisterType(typeof(ITextService), resolutionKey:"TheTextService", isSingleton:true)]
    public class TextService : ITextService
    {
        public event Action<string>? SentTextEvent;

        public void Send(string text)
        {
            SentTextEvent?.Invoke(text);
        }
    }
}