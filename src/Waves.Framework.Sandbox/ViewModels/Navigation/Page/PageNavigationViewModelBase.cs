using System.Windows.Input;
using ReactiveUI;
using Waves.Framework.Presentation;
using Waves.Framework.Services.Interfaces;

namespace Waves.Sandbox.ViewModels.Navigation.Page;

public class PageNavigationViewModelBase : WavesViewModel
{
    protected PageNavigationViewModelBase(IWavesNavigationService navigationService)
    {
        NavigationService = navigationService;
        GoBackCommand = ReactiveCommand.CreateFromTask(OnGoBack);
    }
    
    public ICommand GoBackCommand { get; protected set; }

    protected IWavesNavigationService NavigationService { get; }
    
    private Task OnGoBack()
    {
        return NavigationService.GoBackAsync(this);
    }
}

public class PageNavigationWithResultViewModelBase : WavesViewModel<string>
{
    protected PageNavigationWithResultViewModelBase(IWavesNavigationService navigationService)
    {
        NavigationService = navigationService;
        GoBackCommand = ReactiveCommand.CreateFromTask(OnGoBack);
    }
    
    public ICommand GoBackCommand { get; protected set; }

    protected IWavesNavigationService NavigationService { get; }
    
    private Task OnGoBack()
    {
        return NavigationService.GoBackAsync(this);
    }
}

public class PageNavigationWithParameterViewModelBase : WavesParameterizedViewModel<string>
{
    protected PageNavigationWithParameterViewModelBase(IWavesNavigationService navigationService)
    {
        NavigationService = navigationService;
        GoBackCommand = ReactiveCommand.CreateFromTask(OnGoBack);
    }
    
    public string Parameter { get; private set; }
    
    public ICommand GoBackCommand { get; protected set; }

    protected IWavesNavigationService NavigationService { get; }
    
    public override Task Prepare(string t)
    {
        Parameter = t;
        return Task.CompletedTask;
    }
    
    private Task OnGoBack()
    {
        return NavigationService.GoBackAsync(this);
    }
}

public class PageNavigationWithParameterWithResultViewModelBase : WavesParameterizedViewModel<string, string>
{
    protected PageNavigationWithParameterWithResultViewModelBase(IWavesNavigationService navigationService)
    {
        NavigationService = navigationService;
        GoBackCommand = ReactiveCommand.CreateFromTask(OnGoBack);
    }
    
    public string Parameter { get; private set; }
    
    public ICommand GoBackCommand { get; protected set; }

    protected IWavesNavigationService NavigationService { get; }
    
    public override Task Prepare(string t)
    {
        Parameter = t;
        return Task.CompletedTask;
    }
    
    private Task OnGoBack()
    {
        return NavigationService.GoBackAsync(this);
    }
}