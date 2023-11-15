using Waves.Framework.Attributes;
using Waves.Sandbox.Model.Color;
using Waves.Sandbox.Services.Interfaces;
using Waves.Sandbox.ViewModels.UI.Color;

namespace Waves.Sandbox.Services;

[WavesPlugin(typeof(IColorTintService))]
public class ColorTintService : IColorTintService
{
    public Task<WavesColorTintList> GenerateTints(WavesColor color, int numberOfTints = 9)
    {
        var result = new WavesColorTintList();

        var red = color.R;
        var green = color.G;
        var blue = color.B;
        var tint = 100;

        // Determine the darkness or lightness of the original color
        var brightness = (int)Math.Sqrt(red * red * 0.299 + green * green * 0.587 + blue * blue * 0.114);

        // Calculate the step size for tint calculation
        var step = brightness >= 128 ? -brightness / (numberOfTints + 1) : (255 - brightness) / (numberOfTints + 1);

        for (var i = 1; i <= numberOfTints; i++)
        {
            var tintRed = Clamp(red + i * step, 0, 255);
            var tintGreen = Clamp(green + i * step, 0, 255);
            var tintBlue = Clamp(blue + i * step, 0, 255);
            result.Tints.Add(
                new WavesColorTint(
                    new WavesColor(
                        (byte)tintRed,
                        (byte)tintGreen,
                        (byte)tintBlue),
                    tint));

            tint += 100;
        }

        return Task.FromResult(result);
    }
    
    static int Clamp(int value, int min, int max)
    {
        if (value < min)
            return min;
        if (value > max)
            return max;
        return value;
    }
}