using Waves.Framework.UI.Attributes;
using Waves.Framework.UI.Avalonia.Controls;
using Waves.Sandbox;
using Waves.Sandbox.ViewModels.Navigation.Window;

namespace Waves.Framework.Sandbox.Avalonia.Views.Navigation.Window;

[WavesView(typeof(WindowNavigationViewModel), region: Regions.WindowNavigation)]
public partial class WindowNavigationWindowView : WavesWindow
{
    public WindowNavigationWindowView()
    {
        InitializeComponent();
    }
}