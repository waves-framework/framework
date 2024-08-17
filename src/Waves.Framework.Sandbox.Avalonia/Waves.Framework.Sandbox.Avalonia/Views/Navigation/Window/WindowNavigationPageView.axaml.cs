using Waves.Framework.Attributes;
using Waves.Framework.UI.Avalonia.Controls;
using Waves.Sandbox.ViewModels.Navigation.Window;

namespace Waves.Framework.Sandbox.Avalonia.Views.Navigation.Window;

[WavesView(typeof(WindowNavigationPageViewModel))]
public partial class WindowNavigationPageView : WavesPage
{
    public WindowNavigationPageView()
    {
        InitializeComponent();
    }
}