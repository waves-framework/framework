using Microsoft.Extensions.Logging;
using Waves.Framework.Attributes;

namespace Waves.Framework.Sandbox.Console;

/// <summary>
/// Sandbox service.
/// </summary>
[WavesPlugin(typeof(SandboxService))]
public class SandboxService
{
    private readonly ILogger<SandboxService> _logger;

    public SandboxService(ILogger<SandboxService> logger)
    {
        _logger = logger;
    }
    
    public int GetRandom()
    {
        var random = new Random();
        _logger.LogInformation($"{nameof(GetRandom)} invoked");
        return random.Next();
    }
}