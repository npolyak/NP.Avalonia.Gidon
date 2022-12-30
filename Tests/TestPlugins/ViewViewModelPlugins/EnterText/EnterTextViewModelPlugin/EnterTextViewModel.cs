using Common;
using CommonNonVisualLib;
using NP.DependencyInjection.Attributes;
using NP.Utilities;
using NP.Utilities.PluginUtils;
using TestServiceInterfaces;

namespace EnterTextViewModelPlugin;

[RegisterType(typeof(IPlugin), resolutionKey: PluginKeys.EnterTextViewModel, isSingleton: true)]
public class EnterTextViewModel : VMBase, IPlugin
{
    public MyTestViewModel TheVM { get; } = new MyTestViewModel();

    // ITextService implementation
    [Inject(typeof(ITextService), resolutionKey:PluginKeys.TheTextService)]
    public ITextService? TheTextService { get; private set; }

    #region Text Property
    private string? _text;

    // notifiable property
    public string? Text
    {
        get
        {
            return this._text;
        }
        set
        {
            if (this._text == value)
            {
                return;
            }

            this._text = value;
            this.OnPropertyChanged(nameof(Text));
            this.OnPropertyChanged(nameof(CanSendText));
        }
    }
    #endregion Text Property

    // change notified the Text changes
    public bool CanSendText => !string.IsNullOrWhiteSpace(this._text);

    // method to send the text via TextService
    public void SendText()
    {
        if (!CanSendText)
        {
            throw new Exception("Cannost send text, this method should not have been called.");
        }

        TheTextService!.Send(Text!);
    }
}
