using Avalonia.Controls;
using Waves.Framework.Sandbox.Avalonia.ViewModels;
using Waves.Framework.UI.Core.Attributes;
using Waves.Framework.UI.Core.Presentation.Interfaces;

namespace Waves.Framework.Sandbox.Avalonia.Views;

[WavesView(typeof(MainViewModel))]
public partial class MainView : UserControl, IWavesView
{
    public MainView()
    {
        InitializeComponent();
    }

    public void Dispose()
    {
        // TODO release managed resources here
    }
}