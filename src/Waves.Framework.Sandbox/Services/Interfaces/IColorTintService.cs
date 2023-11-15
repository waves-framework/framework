using Waves.Sandbox.Model.Color;

namespace Waves.Sandbox.Services.Interfaces;

public interface IColorTintService
{
    Task<WavesColorTintList> GenerateTints(WavesColor color, int numberOfTints = 9);
}