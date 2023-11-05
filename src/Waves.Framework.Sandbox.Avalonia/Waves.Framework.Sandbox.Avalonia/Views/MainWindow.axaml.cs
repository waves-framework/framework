using Avalonia.Controls;
using Waves.Framework.Sandbox.Avalonia.ViewModels;
using Waves.Framework.UI.Avalonia.Controls;
using Waves.Framework.UI.Core.Attributes;

namespace Waves.Framework.Sandbox.Avalonia.Views;

[WavesView(typeof(MainWindowViewModel))]
public partial class MainWindow : WavesWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }
}