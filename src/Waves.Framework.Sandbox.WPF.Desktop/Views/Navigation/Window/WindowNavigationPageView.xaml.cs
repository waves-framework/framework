using Waves.Framework.UI.Attributes;
using Waves.Framework.UI.WPF.Controls;
using Waves.Sandbox.ViewModels.Navigation.Window;

namespace Waves.Framework.Sandbox.WPF.Desktop.Views.Navigation.Window;

[WavesView(typeof(WindowNavigationPageViewModel))]
public partial class WindowNavigationPageView : WavesPage
{
    public WindowNavigationPageView()
    {
        InitializeComponent();
    }
}