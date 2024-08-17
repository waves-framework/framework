using Waves.Framework.Attributes;
using Waves.Framework.UI.WPF.Controls;
using Waves.Sandbox.ViewModels.Navigation.Page;

namespace Waves.Framework.Sandbox.WPF.Desktop.Views.Navigation.Page;

[WavesView(typeof(PageNavigationPageViewModel))]
public partial class PageNavigationPageView : WavesPage
{
    public PageNavigationPageView()
    {
        InitializeComponent();
    }
}