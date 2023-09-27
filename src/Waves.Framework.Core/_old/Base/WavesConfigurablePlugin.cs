using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Waves.Framework.Core._old.Base.Interfaces;

namespace Waves.Framework.Core._old.Base;

/// <summary>
/// Interface for configurable plugins.
/// </summary>
public abstract class WavesConfigurablePlugin :
    WavesConfigurableObject,
    IWavesConfigurablePlugin
{
    /// <summary>
    /// Creates new instance of <see cref="WavesConfigurablePlugin"/>.
    /// </summary>
    /// <param name="configuration">Configuration.</param>
    /// <param name="logger">Logger.</param>
    protected WavesConfigurablePlugin(
        IConfiguration configuration,
        ILogger<WavesConfigurablePlugin> logger)
        : base(configuration, logger)
    {
    }
}
