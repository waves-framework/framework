using System.Windows;
using Waves.Framework.UI.Attributes;
using Waves.Framework.UI.WPF.Controls;
using Waves.Sandbox;
using Waves.Sandbox.ViewModels.Navigation;
using Waves.Sandbox.ViewModels.Navigation.Window;

namespace Waves.Framework.Sandbox.WPF.Desktop.Views.Navigation.Window;

[WavesView(typeof(WindowNavigationWithResultWindowViewModel), region: Regions.WindowNavigationWithParameterWithResult)]
public partial class WindowNavigationWithResultWindowView : WavesWindow
{
    public WindowNavigationWithResultWindowView()
    {
        InitializeComponent();
    }
}