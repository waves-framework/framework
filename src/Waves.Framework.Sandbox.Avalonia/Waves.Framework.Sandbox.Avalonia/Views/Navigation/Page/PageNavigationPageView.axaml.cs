using Waves.Framework.Attributes;
using Waves.Framework.UI.Avalonia.Controls;
using Waves.Sandbox.ViewModels.Navigation.Page;

namespace Waves.Framework.Sandbox.Avalonia.Views.Navigation.Page;

[WavesView(typeof(PageNavigationPageViewModel))]
public partial class PageNavigationPageView : WavesPage
{
    public PageNavigationPageView()
    {
        InitializeComponent();
    }
}