using Waves.Sandbox.Model.Color;

namespace Waves.Sandbox.Services.Interfaces;

public interface IColorGeneratorService
{
    Task<List<WavesColorTintList>> Generate();

    Task<string> GetColorTemplateKey(string name);
}