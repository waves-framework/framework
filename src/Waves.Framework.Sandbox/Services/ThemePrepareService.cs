using Waves.Framework.Attributes;
using Waves.Framework.Enums;
using Waves.Framework.Services.Interfaces;
using Waves.Sandbox.Services.Interfaces;
using Waves.Sandbox.ViewModels.UI.Color;

namespace Waves.Sandbox.Services;

/// <summary>
/// Что необходимо:
/// Иметь 2 темы: светлую и темную
/// Для каждой темы необходимо иметь 5 наборов цветов с оттенками от 100 до 900: основной (фон), цвет текста, акценты
/// </summary>
[WavesPlugin(typeof(IThemePrepareService), WavesLifetime.Singleton)]
public class ThemePrepareService : IThemePrepareService
{
    private readonly IColorGeneratorService _colorGeneratorService;
    private readonly IResourceDictionaryFactory _resourceDictionaryFactory;
    private readonly IWavesThemeService _wavesThemeService;

    public ThemePrepareService(
        IColorGeneratorService colorGeneratorService,
        IResourceDictionaryFactory resourceDictionaryFactory,
        IWavesThemeService wavesThemeService)
    {
        _colorGeneratorService = colorGeneratorService;
        _resourceDictionaryFactory = resourceDictionaryFactory;
        _wavesThemeService = wavesThemeService;
    }
    
    public async Task RandomTheme()
    {
        await UpdateColors();
    }

    public ColorSetViewModel GetColors()
    {
        throw new NotImplementedException();
    }

    public Task SetColors(ColorSetViewModel tintLists)
    {
        throw new NotImplementedException();
    }

    public void ApplyTheme()
    {
        throw new NotImplementedException();
    }

    private async Task UpdateColors()
    {
        var colors = await _colorGeneratorService.Generate();
    }
}