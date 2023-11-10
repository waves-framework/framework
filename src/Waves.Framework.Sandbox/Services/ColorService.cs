using RestSharp;
using Waves.Framework.Attributes;
using Waves.Sandbox.Model.Color;

namespace Waves.Sandbox.Services;

[WavesPlugin(typeof(ColorService))]
public class ColorService
{
    private static int _numberOfTints = 9;
    
    public async Task<ColormindApiResponse> GenerateAsync()
    {
        // Create a new RestClient instance
        var client = new RestClient();

        // Create a new RestRequest instance with the POST method
        var request = new RestRequest("http://colormind.io/api/", Method.Post);

        // Set the request body as binary data
        request.AddParameter("application/octet-stream", "{\"model\":\"default\"}", ParameterType.RequestBody);

        // Execute the request and get the response
        var response = await client.ExecuteAsync<ColormindApiResponse>(request);

        if (response.Data != null)
        {
            return response.Data;
        }
        else
        {
            throw new Exception("An error occured");
        }
    }

    public Task<GeneratorColorTints> GenerateTints(List<int> color)
    {
        if (color.Count != 3)
        {
            throw new Exception("Wrong input");
        }

        var result = new GeneratorColorTints();

        var red = color[0];
        var green = color[1];
        var blue = color[2];
        var tint = 100;

        // Determine the darkness or lightness of the original color
        var brightness = (int)Math.Sqrt(red * red * 0.299 + green * green * 0.587 + blue * blue * 0.114);

        // Calculate the step size for tint calculation
        var step = brightness <= 128 ? -brightness / (_numberOfTints + 1) : (255 - brightness) / (_numberOfTints + 1);

        for (var i = 1; i <= _numberOfTints; i++)
        {
            var tintRed = Clamp(red + i * step, 0, 255);
            var tintGreen = Clamp(green + i * step, 0, 255);
            var tintBlue = Clamp(blue + i * step, 0, 255);
            result.Tints.Add(new GeneratorColor
            {
                R = (byte)tintRed,
                G = (byte)tintGreen,
                B = (byte)tintBlue,
                Tint=tint
            });

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