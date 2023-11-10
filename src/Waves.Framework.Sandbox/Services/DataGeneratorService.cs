using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using Waves.Framework.Attributes;

namespace Waves.Sandbox.Services;

[WavesPlugin(typeof(DataGeneratorService))]
public class DataGeneratorService
{
    public string? Generate()
    {
        var randomizerTextRegex = RandomizerFactory.GetRandomizer(new FieldOptionsTextRegex { Pattern = @"^[0-9]{4}[A-Z]{2}" });
        return randomizerTextRegex.Generate(); 
    }
}