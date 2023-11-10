using Waves.Framework.UI.Attributes;
using Waves.Framework.UI.Avalonia.Controls;
using Waves.Sandbox;
using Waves.Sandbox.ViewModels.Navigation.Window;

namespace Waves.Framework.Sandbox.Avalonia.Views.Navigation.Window;

[WavesView(typeof(WindowNavigationWithParameterWithResultWindowViewModel), region: Regions.WindowNavigationWithResult)]
public partial class WindowNavigationWithParameterWithResultWindowView : WavesWindow
{
    public WindowNavigationWithParameterWithResultWindowView()
    {
        InitializeComponent();
    }
}