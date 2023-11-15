using Waves.Sandbox.Model.Color;
using Waves.Sandbox.ViewModels.UI.Color;

namespace Waves.Sandbox.Services.Interfaces;

public interface IThemePrepareService
{
    Task RandomTheme();

    ColorSetViewModel GetColors();
    
    Task SetColors(ColorSetViewModel tintLists);

    void ApplyTheme();
}