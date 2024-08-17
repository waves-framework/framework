using System.Windows.Input;
using ReactiveUI;
using Waves.Framework.Attributes;
using Waves.Framework.Services.Interfaces;
using Waves.Sandbox.ViewModels.Base;
using Waves.Sandbox.ViewModels.Navigation.Page;
using Waves.Sandbox.ViewModels.Navigation.Window;
using Waves.Sandbox.ViewModels.UI;

namespace Waves.Sandbox.ViewModels;

[WavesViewModel(typeof(MainPageViewModel))]
public class MainPageViewModel : PageViewModelBase
{
    private readonly IWavesNavigationService _navigationService;

    public MainPageViewModel(IWavesNavigationService navigationService) : base(navigationService)
    {
        _navigationService = navigationService;
        
        GoToWindowNavigationCommand = ReactiveCommand.CreateFromTask(OnGoToWindowNavigation);
        GoToPageNavigationCommand = ReactiveCommand.CreateFromTask(OnGoToPageNavigation);
        GoToUiGeneratorCommand = ReactiveCommand.CreateFromTask(OnGoToUiGenerator);
    }
    
    public ICommand GoNextCommand { get; private set; }
    
    public ICommand GoToWindowNavigationCommand { get; private set; }
    
    public ICommand GoToPageNavigationCommand { get; private set; }
    
    public ICommand GoToUiGeneratorCommand { get; private set; }
    
    private Task OnGoToWindowNavigation()
    {
        _navigationService.NavigateAsync<WindowNavigationPageViewModel>();
        return Task.CompletedTask;
    }
    
    private Task OnGoToPageNavigation()
    {
        _navigationService.NavigateAsync<PageNavigationPageViewModel>();
        return Task.CompletedTask;
    }
    
    private Task OnGoToUiGenerator()
    {
        _navigationService.NavigateAsync<UiGeneratorPageViewModel>();
        return Task.CompletedTask;
    }
}