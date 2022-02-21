using NP.Utilities;
using NP.Utilities.Attributes;
using NP.Utilities.PluginUtils;
using System.ComponentModel;
using TestServiceInterfaces;

namespace EnterTextViewModelPlugin
{
    [Implements(typeof(IPlugin), partKey:nameof(EnterTextViewModel), isSingleton:true)]
    public class EnterTextViewModel : VMBase, IPlugin
    {
        [Part(typeof(ITextService))]
        public ITextService? TestService { get; private set; }

        #region Text Property
        private string? _text;
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

        public bool CanSendText => !string.IsNullOrWhiteSpace(this._text);

        public void SendText()
        {
            if (!CanSendText)
            {
                throw new Exception("Cannost send text, this method should not have been called.");
            }

            TestService!.Send(Text!);
        }
    }
}
