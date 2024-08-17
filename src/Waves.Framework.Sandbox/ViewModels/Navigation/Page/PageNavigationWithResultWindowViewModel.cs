using System.Windows.Input;
using ReactiveUI;
using Waves.Framework.Attributes;
using Waves.Framework.Services.Interfaces;
using Waves.Sandbox.Services;

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