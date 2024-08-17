using System.Windows.Input;
using ReactiveUI;
using Waves.Framework.Attributes;
using Waves.Sandbox.Services;

namespace Waves.Sandbox.ViewModels.Navigation.Window;

[WavesViewModel(typeof(WindowNavigationWithResultWindowViewModel))]
public class WindowNavigationWithResultWindowViewModel : WindowNavigationWithResultViewModelBase
{
    public WindowNavigationWithResultWindowViewModel(DataGeneratorService dataGeneratorService)
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