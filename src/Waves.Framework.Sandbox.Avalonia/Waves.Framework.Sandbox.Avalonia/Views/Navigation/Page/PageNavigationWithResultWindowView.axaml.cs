using Waves.Framework.UI.Attributes;
using Waves.Framework.UI.Avalonia.Controls;
using Waves.Sandbox.ViewModels.Navigation.Page;

namespace Waves.Framework.Sandbox.Avalonia.Views.Navigation.Page;

[WavesView(typeof(PageNavigationWithResultWindowViewModel))]
public partial class PageNavigationWithResultWindowView : WavesPage
{
    public PageNavigationWithResultWindowView()
    {
        InitializeComponent();
    }
}