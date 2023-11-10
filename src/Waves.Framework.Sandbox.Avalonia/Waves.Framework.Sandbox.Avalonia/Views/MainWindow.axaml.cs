using Avalonia.Controls;
using Waves.Framework.UI.Attributes;
using Waves.Framework.UI.Avalonia.Controls;
using Waves.Sandbox.ViewModels;

namespace Waves.Framework.Sandbox.Avalonia.Views;

[WavesView(typeof(MainWindowViewModel))]
public partial class MainWindow : WavesWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }
}