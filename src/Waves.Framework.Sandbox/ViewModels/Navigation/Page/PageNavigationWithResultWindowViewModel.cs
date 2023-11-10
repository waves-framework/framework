using System.Windows.Input;
using ReactiveUI;
using Waves.Framework.UI.Attributes;
using Waves.Framework.UI.Services.Interfaces;
using Waves.Sandbox.Services;
using Waves.Sandbox.ViewModels.Navigation.Window;

namespace Waves.Sandbox.ViewModels.Navigation.Page;

[WavesViewModel(typeof(PageNavigationWithResultWindowViewModel))]
public class PageNavigationWithResultWindowViewModel : PageNavigationWithResultViewModelBase
{
    public PageNavigationWithResultWindowViewModel(DataGeneratorService dataGeneratorService, IWavesNavigationService navigationService) : base(navigationService)
    {
        OkCommand = ReactiveCommand.CreateFromTask(OnOk);
        Result = dataGeneratorService.Generate() ?? string.Empty;
    }

    public ICommand OkCommand { get; protected set; }
    
    private Task OnOk()
    {
        OnResultApproved();
        return Task.CompletedTask;
    }
}