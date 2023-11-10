using Waves.Framework.UI.Attributes;
using Waves.Framework.UI.WPF.Controls;
using Waves.Sandbox.ViewModels.Navigation.Page;

namespace Waves.Framework.Sandbox.WPF.Desktop.Views.Navigation.Page;

[WavesView(typeof(PageNavigationWithParameterWithResultWindowViewModel))]
public partial class PageNavigationWithParameterWithResultWindowView : WavesPage
{
    public PageNavigationWithParameterWithResultWindowView()
    {
        InitializeComponent();
    }
}