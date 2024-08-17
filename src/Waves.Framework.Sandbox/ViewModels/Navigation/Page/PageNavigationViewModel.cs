using ReactiveUI.Fody.Helpers;
using Waves.Framework.Attributes;
using Waves.Framework.Services.Interfaces;
using Waves.Sandbox.Services;

namespace Waves.Sandbox.ViewModels.Navigation.Page;

[WavesViewModel(typeof(PageNavigationViewModel))]
public class PageNavigationViewModel : PageNavigationViewModelBase
{
    public PageNavigationViewModel(IWavesNavigationService navigationService, DataGeneratorService dataGeneratorService) : base(navigationService)
    {
        Parameter = dataGeneratorService.Generate() ?? string.Empty;
    }
    
    [Reactive]
    public string Parameter { get; set; }
}