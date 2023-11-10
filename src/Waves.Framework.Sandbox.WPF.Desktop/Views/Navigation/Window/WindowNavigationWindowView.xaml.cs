using System.Windows;
using Waves.Framework.UI.Attributes;
using Waves.Framework.UI.WPF.Controls;
using Waves.Sandbox;
using Waves.Sandbox.ViewModels.Navigation;
using Waves.Sandbox.ViewModels.Navigation.Window;

namespace Waves.Framework.Sandbox.WPF.Desktop.Views.Navigation.Window;

[WavesView(typeof(WindowNavigationViewModel), region: Regions.WindowNavigation)]
public partial class WindowNavigationWindowView : WavesWindow
{
    public WindowNavigationWindowView()
    {
        InitializeComponent();
    }
}