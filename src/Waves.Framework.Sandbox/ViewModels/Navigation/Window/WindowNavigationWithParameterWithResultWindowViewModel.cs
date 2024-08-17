using System.Windows.Input;
using ReactiveUI;
using Waves.Framework.Attributes;
using Waves.Sandbox.Services;

namespace Waves.Sandbox.ViewModels.Navigation.Window;

[WavesViewModel(typeof(WindowNavigationWithParameterWithResultWindowViewModel))]
public class WindowNavigationWithParameterWithResultWindowViewModel : WindowNavigationWithParameterWithResultViewModelBase
{
    public WindowNavigationWithParameterWithResultWindowViewModel(DataGeneratorService dataGeneratorService)
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