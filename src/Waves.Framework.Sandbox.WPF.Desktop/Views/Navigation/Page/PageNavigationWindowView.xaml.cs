using Waves.Framework.Attributes;
using Waves.Framework.UI.WPF.Controls;
using Waves.Sandbox.ViewModels.Navigation.Page;

namespace Waves.Framework.Sandbox.WPF.Desktop.Views.Navigation.Page;

[WavesView(typeof(PageNavigationViewModel))]
public partial class PageNavigationWindowView : WavesPage
{
    public PageNavigationWindowView()
    {
        InitializeComponent();
    }
}