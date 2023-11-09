using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using Waves.Framework.UI.Attributes;
using Waves.Framework.UI.Services.Interfaces;

namespace Waves.Framework.Sandbox.Avalonia.ViewModels;

[WavesViewModel(typeof(MainPageViewModel))]
public class MainPageViewModel : PageViewModelBase
{
    private readonly IWavesNavigationService _navigationService;

    public MainPageViewModel(IWavesNavigationService navigationService)
    {
        _navigationService = navigationService;
        
        GoNextCommand = ReactiveCommand.CreateFromTask(OnGoNext);
    }

    public string Greeting => "Welcome to Avalonia!";

    public override string PageName => "Main page (#1)";
    
    public ICommand GoNextCommand { get; private set; }
    
    private Task OnGoNext()
    {
        _navigationService.NavigateAsync<SecondPageViewModel>();
        return Task.CompletedTask;
    }
}