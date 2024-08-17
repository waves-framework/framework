using Waves.Framework.Attributes;
using Waves.Framework.UI.Avalonia.Controls;
using Waves.Sandbox.ViewModels;

namespace Waves.Framework.Sandbox.Avalonia.Views;

[WavesView(typeof(MainPageViewModel))]
public partial class MainPageView : WavesPage
{
    public MainPageView()
    {
        InitializeComponent();
    }
}