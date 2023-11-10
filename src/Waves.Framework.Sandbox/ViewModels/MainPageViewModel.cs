using System.Windows.Input;
using ReactiveUI;
using Waves.Framework.UI.Attributes;
using Waves.Framework.UI.Services.Interfaces;
using Waves.Sandbox.ViewModels.Base;
using Waves.Sandbox.ViewModels.Navigation;
using Waves.Sandbox.ViewModels.Navigation.Page;
using Waves.Sandbox.ViewModels.Navigation.Window;

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
    }
    
    public ICommand GoNextCommand { get; private set; }
    
    public ICommand GoToWindowNavigationCommand { get; private set; }
    
    public ICommand GoToPageNavigationCommand { get; private set; }
    
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
}