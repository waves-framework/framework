using System.Windows.Input;
using ReactiveUI;
using Waves.Framework.Services.Interfaces;

namespace Waves.Sandbox.ViewModels.Base;

public abstract class PageViewModelBase : ViewModelBase
{
    protected PageViewModelBase(IWavesNavigationService navigationService)
    {
        NavigationService = navigationService;
        GoBackCommand = ReactiveCommand.CreateFromTask(OnGoBack);
    }

    protected IWavesNavigationService NavigationService { get; }

    public ICommand GoBackCommand { get; protected set; }
    
    private Task OnGoBack()
    {
        return NavigationService.GoBackAsync(this);
    }
}