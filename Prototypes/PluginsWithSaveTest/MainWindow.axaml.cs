using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using NP.Avalonia.UniDock;

namespace PluginsWithSaveTest
{
    public partial class MainWindow : Window
    {
        private DockManager _dockManager;

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            _dockManager = (DockManager) this.Resources["TheDockManager"]!;

            Button saveButton = this.FindControl<Button>("SaveButton");
            saveButton.Click += SaveButton_Click;

            Button restoreButton = this.FindControl<Button>("RestoreButton");
            restoreButton.Click += RestoreButton_Click;
        }

        private void RestoreButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            _dockManager.RestoreFromFile("SerializationFile.xml");
        }

        private void SaveButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            _dockManager.SaveToFile("SerializationFile.xml");
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
