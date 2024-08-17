using System.Windows.Input;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Waves.Framework.Attributes;
using Waves.Framework.Services.Interfaces;
using Waves.Sandbox.Services;
using Waves.Sandbox.ViewModels.Base;

namespace Waves.Sandbox.ViewModels.Navigation.Page;

[WavesViewModel(typeof(PageNavigationPageViewModel))]
public class PageNavigationPageViewModel : PageViewModelBase
{
    public PageNavigationPageViewModel(IWavesNavigationService navigationService, DataGeneratorService dataGeneratorService) : base(navigationService)
    {
        NavigateToPageCommand = ReactiveCommand.CreateFromTask(OnNavigateToPage);
        NavigateToPageWithParameterCommand = ReactiveCommand.CreateFromTask(OnNavigateToPageWithParameter);
        NavigateToPageWithResultCommand = ReactiveCommand.CreateFromTask(OnNavigateToPageWithResult);
        NavigateToPageWithParameterAndResultCommand = ReactiveCommand.CreateFromTask(OnNavigateToPageWithParameterAndResult);

        Parameter = dataGeneratorService.Generate() ?? string.Empty;
    }

    [Reactive]
    public string Parameter { get; set; }
    
    [Reactive]
    public string Result { get; set; }
    
    public ICommand NavigateToPageCommand { get; protected set; }
    
    public ICommand NavigateToPageWithParameterCommand { get; protected set; }
    
    public ICommand NavigateToPageWithResultCommand { get; protected set; }
    
    public ICommand NavigateToPageWithParameterAndResultCommand { get; protected set; }
    
    private Task OnNavigateToPage()
    {
        return NavigationService.NavigateAsync<PageNavigationViewModel>();
    }
    
    private Task OnNavigateToPageWithParameter()
    {
        return NavigationService.NavigateAsync<PageNavigationWithParameterWindowViewModel, string>(Parameter);
    }
    
    private async Task OnNavigateToPageWithResult()
    {
        Result = await NavigationService.NavigateAsync<PageNavigationWithResultWindowViewModel, string>() ?? string.Empty;
    }
    
    private async Task OnNavigateToPageWithParameterAndResult()
    {
        Result = await NavigationService.NavigateAsync<PageNavigationWithParameterWithResultWindowViewModel, string, string>(Parameter) ?? string.Empty;
    }
}