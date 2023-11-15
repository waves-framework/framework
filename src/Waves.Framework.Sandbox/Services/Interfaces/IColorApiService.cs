using System.Drawing;
using Waves.Sandbox.Model.Color;

namespace Waves.Sandbox.Services.Interfaces;

public interface IColorApiService
{
    Task<List<WavesColor>> GetColors();
    
    Task<List<WavesColor>> GetColors(List<WavesColor> input);
}