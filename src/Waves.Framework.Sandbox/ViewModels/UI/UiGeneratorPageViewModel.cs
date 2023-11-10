using Waves.Framework.UI.Attributes;
using Waves.Framework.UI.Services.Interfaces;
using Waves.Sandbox.ViewModels.Base;

namespace Waves.Sandbox.ViewModels.UI;

[WavesViewModel(typeof(UiGeneratorPageViewModel))]
public class UiGeneratorPageViewModel : PageViewModelBase
{
    public UiGeneratorPageViewModel(IWavesNavigationService navigationService) : base(navigationService)
    {
    }
}