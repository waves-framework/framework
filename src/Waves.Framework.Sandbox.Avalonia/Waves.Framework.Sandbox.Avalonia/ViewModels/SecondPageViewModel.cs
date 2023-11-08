using Waves.Framework.UI.Attributes;

namespace Waves.Framework.Sandbox.Avalonia.ViewModels;

[WavesViewModel(typeof(SecondPageViewModel))]
public class SecondPageViewModel : PageViewModelBase
{
    public string Greeting => "Another text";

    public override string PageName => "Second page (#2)";
}