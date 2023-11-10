using Waves.Framework.UI.Attributes;
using Waves.Framework.UI.Avalonia.Controls;
using Waves.Sandbox.ViewModels.Navigation.Page;

namespace Waves.Framework.Sandbox.Avalonia.Views.Navigation.Page;

[WavesView(typeof(PageNavigationWithParameterWindowViewModel))]
public partial class PageNavigationWithParameterWindowView : WavesPage
{
    public PageNavigationWithParameterWindowView()
    {
        InitializeComponent();
    }
}