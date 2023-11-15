using Waves.Sandbox.Model.Color;
using Waves.Sandbox.ViewModels.Base;

namespace Waves.Sandbox.ViewModels.UI.Color;

public class ColorSetThemeViewModel : ViewModelBase
{
    public bool Lock { get; set; }
    
    public WavesColorTintList Tints { get; set; }
}