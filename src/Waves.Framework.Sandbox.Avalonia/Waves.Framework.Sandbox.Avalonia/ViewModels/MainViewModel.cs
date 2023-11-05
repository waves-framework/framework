using Waves.Framework.UI.Core.Attributes;

namespace Waves.Framework.Sandbox.Avalonia.ViewModels;

[WavesViewModel(typeof(MainViewModel))]
public class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";
}