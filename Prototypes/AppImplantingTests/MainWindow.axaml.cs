using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using NP.Avalonia.Visuals.Controls;
using NP.Gidon.Messages;
using System;
using System.IO;

namespace AppImplantingTest
{
    public partial class MainWindow : Window
    {
        IDisposable? _clientSubscription;
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            _clientSubscription =
                App.TheRelayClient
                   .ObserveTopicStream<WindowInfo>(Topic.WindowInfoTopic)
                   .Subscribe(OnWindowInfoArrived);
        }


        #region ChildWindowHandle Styled Avalonia Property
        public nint ChildWindowHandle
        {
            get { return GetValue(ChildWindowHandleProperty); }
            set { SetValue(ChildWindowHandleProperty, value); }
        }

        public static readonly StyledProperty<nint> ChildWindowHandleProperty =
            AvaloniaProperty.Register<MainWindow, nint>
            (
                nameof(ChildWindowHandle)
            );
        #endregion ChildWindowHandle Styled Avalonia Property


        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            //var handle = this.GetWinHandle();

            //string currentDir = Directory.GetCurrentDirectory();
        }

        private void OnWindowInfoArrived(WindowInfo windowInfo)
        {
            ChildWindowHandle = (nint) windowInfo.WindowHandle;
        }
    }
}
