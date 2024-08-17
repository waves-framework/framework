using System.Windows.Input;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Waves.Framework.Attributes;
using Waves.Framework.Services.Interfaces;
using Waves.Sandbox.Services;
using Waves.Sandbox.ViewModels.Base;

namespace Waves.Sandbox.ViewModels.Navigation.Window;

[WavesViewModel(typeof(WindowNavigationPageViewModel))]
public class WindowNavigationPageViewModel : PageViewModelBase
{
    public WindowNavigationPageViewModel(IWavesNavigationService navigationService, DataGeneratorService dataGeneratorService) : base(navigationService)
    {
        NavigateToWindowCommand = ReactiveCommand.CreateFromTask(OnNavigateToWindow);
        NavigateToWindowWithParameterCommand = ReactiveCommand.CreateFromTask(OnNavigateToWindowWithParameter);
        NavigateToWindowWithResultCommand = ReactiveCommand.CreateFromTask(OnNavigateToWindowWithResult);
        NavigateToWindowWithParameterAndResultCommand = ReactiveCommand.CreateFromTask(OnNavigateToWindowWithParameterAndResult);

        Parameter = dataGeneratorService.Generate() ?? string.Empty;
    }

    [Reactive]
    public string Parameter { get; set; }
    
    [Reactive]
    public string Result { get; set; }
    
    public ICommand NavigateToWindowCommand { get; protected set; }
    
    public ICommand NavigateToWindowWithParameterCommand { get; protected set; }
    
    public ICommand NavigateToWindowWithResultCommand { get; protected set; }
    
    public ICommand NavigateToWindowWithParameterAndResultCommand { get; protected set; }
    
    private Task OnNavigateToWindow()
    {
        return NavigationService.NavigateAsync<WindowNavigationViewModel>();
    }
    
    private Task OnNavigateToWindowWithParameter()
    {
        return NavigationService.NavigateAsync<WindowNavigationWithParameterWindowViewModel, string>(Parameter);
    }
    
    private async Task OnNavigateToWindowWithResult()
    {
        Result = await NavigationService.NavigateAsync<WindowNavigationWithResultWindowViewModel, string>() ?? string.Empty;
    }
    
    private async Task OnNavigateToWindowWithParameterAndResult()
    {
        Result = await NavigationService.NavigateAsync<WindowNavigationWithParameterWithResultWindowViewModel, string, string>(Parameter) ?? string.Empty;
    }
}