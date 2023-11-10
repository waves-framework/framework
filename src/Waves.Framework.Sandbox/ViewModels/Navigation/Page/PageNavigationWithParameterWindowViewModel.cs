using Waves.Framework.UI.Attributes;
using Waves.Framework.UI.Services.Interfaces;

namespace Waves.Sandbox.ViewModels.Navigation.Page;

[WavesViewModel(typeof(PageNavigationWithParameterWindowViewModel))]
public class PageNavigationWithParameterWindowViewModel : PageNavigationWithParameterViewModelBase
{
    public PageNavigationWithParameterWindowViewModel(IWavesNavigationService navigationService) : base(navigationService)
    {
    }
}