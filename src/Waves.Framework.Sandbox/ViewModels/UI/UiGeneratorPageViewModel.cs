using System.Collections.ObjectModel;
using System.Windows.Input;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Waves.Framework.UI.Attributes;
using Waves.Framework.UI.Services.Interfaces;
using Waves.Sandbox.Model.Color;
using Waves.Sandbox.Services;
using Waves.Sandbox.ViewModels.Base;

namespace Waves.Sandbox.ViewModels.UI;

[WavesViewModel(typeof(UiGeneratorPageViewModel))]
public class UiGeneratorPageViewModel : PageViewModelBase
{
    private readonly ColorService _colorService;

    public UiGeneratorPageViewModel(IWavesNavigationService navigationService, ColorService colorService) : base(navigationService)
    {
        _colorService = colorService;
        GenerateCommand = ReactiveCommand.CreateFromTask(OnGenerate);
    }
    
    [Reactive]
    public ObservableCollection<GeneratorColorTints> Colors { get; set; }

    public ICommand GenerateCommand { get; protected set; }
    
    private async Task OnGenerate()
    {
        var result = await _colorService.GenerateAsync();
        var list = new List<GeneratorColorTints>();
        foreach (var color in result.Result)
        {
            list.Add(await _colorService.GenerateTints(color));
        }

        Colors = new ObservableCollection<GeneratorColorTints>(list);
    }
}