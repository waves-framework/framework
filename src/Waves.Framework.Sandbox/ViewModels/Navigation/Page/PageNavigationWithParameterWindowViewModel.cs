using Waves.Framework.Attributes;
using Waves.Framework.Services.Interfaces;

namespace Waves.Sandbox.ViewModels.Navigation.Page;

[WavesViewModel(typeof(PageNavigationWithParameterWindowViewModel))]
public class PageNavigationWithParameterWindowViewModel : PageNavigationWithParameterViewModelBase
{
    public PageNavigationWithParameterWindowViewModel(IWavesNavigationService navigationService) : base(navigationService)
    {
    }
}