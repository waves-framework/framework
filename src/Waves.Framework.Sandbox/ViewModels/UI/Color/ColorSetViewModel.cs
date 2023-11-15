using Waves.Sandbox.ViewModels.Base;

namespace Waves.Sandbox.ViewModels.UI.Color;

public class ColorSetViewModel : ViewModelBase
{
    public ColorSetThemeViewModel Light { get; set; }
    
    public ColorSetThemeViewModel Dark { get; set; }
}