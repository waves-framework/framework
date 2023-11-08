using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Waves.Framework.Interfaces;

/// <summary>
/// Interface of Waves Application.
/// </summary>
public interface IWavesApplication
{
    /// <summary>
    /// Gets services provider.
    /// </summary>
    IWavesServiceProvider Services { get; }
    
    /// <summary>
    /// Gets configuration.
    /// </summary>
    IConfiguration Configuration { get; }
    
    /// <summary>
    /// Gets logger.
    /// </summary>
    ILogger<IWavesApplication> Logger { get; }
}