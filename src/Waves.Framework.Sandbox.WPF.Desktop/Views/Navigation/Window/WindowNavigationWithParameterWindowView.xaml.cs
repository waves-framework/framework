using Waves.Framework.Attributes;
using Waves.Framework.UI.WPF.Controls;
using Waves.Sandbox;
using Waves.Sandbox.ViewModels.Navigation.Window;

namespace Waves.Framework.Sandbox.WPF.Desktop.Views.Navigation.Window;

[WavesView(typeof(WindowNavigationWithParameterWindowViewModel), region: Regions.WindowNavigationWithParameter)]
public partial class PageNavigationWithParameterWindowView : WavesWindow
{
    public PageNavigationWithParameterWindowView()
    {
        InitializeComponent();
    }
}