using CommonNonVisualLib;
using NP.Utilities;
using NP.Utilities.PluginUtils;
using TestServiceInterfaces;
using NP.DependencyInjection.Attributes;
using Common;

namespace ReceiveTextViewModelPlugin;

//[RegisterType(typeof(IPlugin), resolutionKey: PluginKeys.ReceiveTextViewModel, isSingleton: true)]
public class ReceiveTextViewModel : VMBase, IPlugin
{
    public MyTestViewModel TheVM { get; } = new MyTestViewModel();

    ITextService? _textService;

    // ITextService implementation
    //[Part(typeof(ITextService), "TheTextService")]
    public ITextService? TheTextService
    {
        get => _textService;
        private set
        {
            if (_textService == value)
                return;

            if (_textService != null)
            {
                // disconnect old service's SentTextEvent
                _textService.SentTextEvent -= _textService_SentTextEvent;
            }

            _textService = value;

            if (_textService != null)
            {   // connect the handler to the service's
                // SentTextEvent
                _textService.SentTextEvent += _textService_SentTextEvent;
            }
        }
    }

    // set Text property when receives it from TheTextService
    // via SentTextEvent
    private void _textService_SentTextEvent(string text)
    {
        Text = text;
    }

    #region Text Property
    private string? _text;
    // notifiable property
    public string? Text
    {
        get
        {
            return this._text;
        }
        private set
        {
            if (this._text == value)
            {
                return;
            }

            this._text = value;
            this.OnPropertyChanged(nameof(Text));
        }
    }
    #endregion Text Property

    [CompositeConstructor]
    public ReceiveTextViewModel([Inject(resolutionKey:PluginKeys.TheTextService)] ITextService textService)
    {
        TheTextService = textService;
    }

    [HasRegisterMethods]
    public static class ReceiveTestViewFactory
    {
        [RegisterMethod(resolutionKey:PluginKeys.ReceiveTextViewModel)]
        public static IPlugin CreateFactory([Inject(resolutionKey:PluginKeys.TheTextService)] ITextService textService)
        {
            return new ReceiveTextViewModel(textService);
        }
    }
}