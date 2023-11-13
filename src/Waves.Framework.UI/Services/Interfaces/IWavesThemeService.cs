using Waves.Framework.UI.Interfaces;

namespace Waves.Framework.UI.Services.Interfaces;

public interface IWavesThemeService
{
    void SwitchTheme();

    void SetDictionary(IWavesResourceDictionary dictionary);
}