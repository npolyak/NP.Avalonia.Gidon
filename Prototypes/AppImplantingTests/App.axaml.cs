using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using NP.DependencyInjection.Interfaces;
using NP.Gidon.Messages;
using NP.Grpc.CommonRelayInterfaces;
using NP.Grpc.RelayClient;
using NP.Grpc.RelayServer;
using NP.GrpcConfig;
using NP.IoCy;
using NP.Protobuf;
using System;

namespace AppImplantingTest
{
    public class App : Application
    {
        internal static IDependencyInjectionContainer<Enum> IoCContainer { get; }

        private static IRelayServer TheRelayServer { get; }

        public static IRelayClient TheRelayClient { get; }

        static App()
        {
            IContainerBuilderWithMultiCells<Enum> containerBuilder = 
                new ContainerBuilder<Enum>();
#if DEBUG
            containerBuilder.RegisterMultiCell(typeof(Enum), IoCKeys.Topics);
            containerBuilder.RegisterSingletonType<IGrpcConfig, GrpcConfig>();
            containerBuilder.RegisterSingletonType<IRelayServer, RelayServer>();
            containerBuilder.RegisterSingletonType<IRelayClient, RelayClient>();
            containerBuilder.RegisterAttributedStaticFactoryMethodsFromClass(typeof(MessagesTopicsGetter));
#else

#endif
            IoCContainer = containerBuilder.Build();

            TheRelayServer = IoCContainer.Resolve<IRelayServer>();

            TheRelayClient = IoCContainer.Resolve<IRelayClient>();
        }

        public override void Initialize()
        {                
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {  
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();

                desktop.ShutdownRequested += Desktop_ShutdownRequested;
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void Desktop_ShutdownRequested(object? sender, ShutdownRequestedEventArgs e)
        {
            TheRelayServer.Shutdown();
        }
    }
}
