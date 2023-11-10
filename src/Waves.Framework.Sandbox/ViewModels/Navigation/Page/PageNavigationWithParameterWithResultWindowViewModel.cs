using System.Windows.Input;
using ReactiveUI;
using Waves.Framework.UI.Attributes;
using Waves.Framework.UI.Services.Interfaces;
using Waves.Sandbox.Services;

namespace Waves.Sandbox.ViewModels.Navigation.Page;

[WavesViewModel(typeof(PageNavigationWithParameterWithResultWindowViewModel))]
public class PageNavigationWithParameterWithResultWindowViewModel : PageNavigationWithParameterWithResultViewModelBase
{
    public PageNavigationWithParameterWithResultWindowViewModel(DataGeneratorService dataGeneratorService, IWavesNavigationService navigationService) : base(navigationService)
    {
        OkCommand = ReactiveCommand.CreateFromTask(OnOk);
        Result = dataGeneratorService.Generate() ?? string.Empty;
    }

    public ICommand OkCommand { get; protected set; }
    
    public override Task Prepare(string t)
    {
        Result = t;
        return Task.CompletedTask;
    }
    
    private Task OnOk()
    {
        OnResultApproved();
        return Task.CompletedTask;
    }
}