using Waves.Framework.Attributes;
using Waves.Sandbox.Model.Color;
using Waves.Sandbox.Services.Interfaces;

namespace Waves.Sandbox.Services;

[WavesPlugin(typeof(IColorGeneratorService))]
public class ColorGeneratorService : IColorGeneratorService
{
    private readonly IColorApiService _colorApiService;
    private readonly IColorTintService _colorTintService;
    
    private readonly List<Tuple<string, string>> _colorMapping = new()
    {
        new Tuple<string, string>("Primary", PrimaryTemplateKey),
        new Tuple<string, string>("PrimaryText", PrimaryTextTemplateKey),
        new Tuple<string, string>("Accent1", Accent1TemplateKey),
        new Tuple<string, string>("Accent2", Accent2TemplateKey),
        new Tuple<string, string>("Accent3", Accent3TemplateKey),
    };
    
    private const string PrimaryTemplateKey = "primary_"; 
    private const string PrimaryTextTemplateKey = "primary_text_"; 
    private const string Accent1TemplateKey = "accent_1_";
    private const string Accent2TemplateKey = "accent_2_";
    private const string Accent3TemplateKey = "accent_3_";

    public ColorGeneratorService(
        IColorApiService colorApiService,
        IColorTintService colorTintService)
    {
        _colorApiService = colorApiService;
        _colorTintService = colorTintService;
    }

    
    public async Task<List<WavesColorTintList>> Generate()
    {
        var result = await _colorApiService.GetColors();
        
        var list = new List<WavesColorTintList>();

        for (var i = 0; i < result.Count; i++)
        {
            var color = result[i];
            var tints = await _colorTintService.GenerateTints(color);
            tints.Name = _colorMapping[i].Item1;
            list.Add(tints);
        }

        return list;
    }

    public Task<string> GetColorTemplateKey(string name)
    {
        return Task.FromResult(_colorMapping.FirstOrDefault(x => x.Item1.Equals(name))!.Item2);
    }
}