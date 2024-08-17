using ReactiveUI.Fody.Helpers;
using Waves.Framework.Attributes;
using Waves.Sandbox.Services;

namespace Waves.Sandbox.ViewModels.Navigation.Window;

[WavesViewModel(typeof(WindowNavigationViewModel))]
public class WindowNavigationViewModel : WindowNavigationViewModelBase
{
    public WindowNavigationViewModel(DataGeneratorService dataGeneratorService)
    {
        Parameter = dataGeneratorService.Generate() ?? string.Empty;
    }
    
    [Reactive]
    public string Parameter { get; set; }
}