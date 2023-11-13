using System.Collections.ObjectModel;
using System.Windows.Input;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Waves.Sandbox.ViewModels.Base;

namespace Waves.Sandbox.ViewModels.UI.Color;

public class GeneratorColorTintsList : ViewModelBase
{
    private string _name;
    private string _swapName;

    public GeneratorColorTintsList()
    {
        NameSelectionChangedCommand = ReactiveCommand.CreateFromTask(OnNameSelectionChanged);
        ReverseCommand = ReactiveCommand.CreateFromTask(OnReverse);
    }

    public event EventHandler<string> OrderChanged;

    [Reactive]
    public string Name { get; set; }

    public List<string> AvailableNames => UiGeneratorPageViewModel.AvailableColorNames;
    
    public ICommand NameSelectionChangedCommand { get; private set; }
    public ICommand ReverseCommand { get; private set; }
    
    public bool Lock { get; set; }
    
    [Reactive]
    public ObservableCollection<GeneratorColor> Tints { get; set; } = new();

    protected virtual void OnOrderChanged(string e)
    {
        OrderChanged?.Invoke(this, e);
    }
    
    private Task OnNameSelectionChanged()
    {
        OnOrderChanged(Name);
        return Task.CompletedTask;
    }
    
    private Task OnReverse()
    {
        Tints = new ObservableCollection<GeneratorColor>(Tints.Reverse());
        return Task.CompletedTask;
    }
}