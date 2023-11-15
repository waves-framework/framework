using System.Text;
using RestSharp;
using Waves.Framework.Attributes;
using Waves.Sandbox.Model.API.Color;
using Waves.Sandbox.Model.Color;
using Waves.Sandbox.Services.Interfaces;

namespace Waves.Sandbox.Services.API;

[WavesPlugin(typeof(IColorApiService))]
public class ColormindApiService : IColorApiService
{
    public async Task<List<WavesColor>> GetColors()
    {
        var result = new List<WavesColor>();
        var client = new RestClient();
        var request = new RestRequest("http://colormind.io/api/", Method.Post);
        request.AddParameter("application/octet-stream", "{\"model\":\"default\"}", ParameterType.RequestBody);
        var response = await client.ExecuteAsync<ColormindApiResponse>(request);
        
        if (response.Data is { Result: not null})
        {
            result.AddRange(from item in response.Data.Result
                let a = 255
                let r = item[0]
                let g = item[1]
                let b = item[2]
                select WavesColor.FromArgb((byte)a, (byte)r, (byte)g, (byte)b));
        }
        else
        {
            throw new Exception("An error occured");
        }

        return result;
    }

    public async Task<List<WavesColor>> GetColors(List<WavesColor> input)
    {
        var result = new List<WavesColor>();
        var client = new RestClient();
        var request = new RestRequest("http://colormind.io/api/", Method.Post);

        // TODO: do this!
        var sb = new StringBuilder();
        request.AddParameter("application/octet-stream", "'{\"input\":[[44,43,44],[90,83,82],\"N\",\"N\",\"N\"],\"model\":\"default\"}'", ParameterType.RequestBody);
        var response = await client.ExecuteAsync<ColormindApiResponse>(request);
        
        if (response.Data is { Result: not null})
        {
            result.AddRange(from item in response.Data.Result
                let a = 255
                let r = item[0]
                let g = item[1]
                let b = item[2]
                select WavesColor.FromArgb((byte)a, (byte)r, (byte)g, (byte)b));
        }
        else
        {
            throw new Exception("An error occured");
        }

        return result;
    }
}