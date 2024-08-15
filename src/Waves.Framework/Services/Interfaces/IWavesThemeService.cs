using Waves.Framework.Interfaces;

namespace Waves.Framework.Services.Interfaces;

public interface IWavesThemeService
{
    void SwitchTheme();

    void SetDictionary(IWavesResourceDictionary dictionary);
}