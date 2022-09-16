using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using NP.Avalonia.UniDock;
using NP.Avalonia.UniDockService;
using SecurityTestViewModelPlugin;
using System.Collections.ObjectModel;

namespace MultiTabsWithPluginTest
{
    public partial class MainWindow : Window
    {
        private int _numberStocks = 0;

        private static SecurityTestViewModel IBM =
            new SecurityTestViewModel
            {
                Symbol = "IBM",
                Description = "Internation Business Machines",
                Ask = 51,
                Bid = 49
            };

        private static SecurityTestViewModel MSFT =
            new SecurityTestViewModel
            {
                Symbol = "MSFT",
                Description = "Microsoft",
                Ask = 101,
                Bid = 99
            };

        private static SecurityTestViewModel[] Stocks =
        {
            IBM,
            MSFT
        };

        private DockManager _dockManager;

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            _dockManager = (DockManager)this.Resources["TheDockManager"]!;

            _dockManager.DockItemsViewModels = new ObservableCollection<DockItemViewModelBase>();

            Button addStockButton = this.FindControl<Button>("AddStockButton");

            addStockButton.Click += AddStockButton_Click;
        }
        private void AddStockButton_Click(object? sender, RoutedEventArgs e)
        {
            var stock = Stocks[_numberStocks%2];
            string? stockName = stock.Symbol;

            var newTabVm = new DockItemViewModelWithStrKey
            {
                DockId = $"{stockName}_{_numberStocks + 1}",
                DefaultDockGroupId = "Securities",
                DefaultDockOrderInGroup = _numberStocks,
                HeaderContentTemplateResourceKey = "SecurityHeaderDataTemplate",
                ContentTemplateResourceKey = "SecurityDataTemplate",
                TheVM = stock,
                IsPredefined = false
            };

            _dockManager.DockItemsViewModels!.Add(newTabVm);

            newTabVm.IsSelected = true;

            _numberStocks++;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }

    public class DockItemViewModelWithStrKey : DockItemViewModel<SecurityTestViewModel>
    {

    }
}
