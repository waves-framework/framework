using Avalonia.Controls;
using Waves.Framework.Sandbox.Avalonia.ViewModels;
using Waves.Framework.UI.Attributes;
using Waves.Framework.UI.Avalonia.Controls;
using Waves.Framework.UI.Presentation.Interfaces;

namespace Waves.Framework.Sandbox.Avalonia.Views;

[WavesView(typeof(MainPageViewModel))]
public partial class MainPageView : WavesPage
{
    public MainPageView()
    {
        InitializeComponent();
    }
}