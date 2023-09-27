using Microsoft.Extensions.Logging;

namespace Waves.Framework.Core.Core.Interfaces;

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
    IWavesConfiguration Configuration { get; }
    
    /// <summary>
    /// Gets logger.
    /// </summary>
    ILogger<IWavesApplication> Logger { get; }
}