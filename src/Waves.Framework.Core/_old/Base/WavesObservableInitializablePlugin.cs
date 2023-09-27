using Microsoft.Extensions.Logging;
using Waves.Framework.Core._old.Base.Interfaces;

namespace Waves.Framework.Core._old.Base;

/// <summary>
/// Interface for observable / initializable plugin.
/// </summary>
public abstract class WavesObservableInitializablePlugin :
    WavesObservableInitializableObject,
    IWavesPlugin
{
    /// <summary>
    /// Creates new instance of <see cref="WavesObservableInitializablePlugin"/>.
    /// </summary>
    /// <param name="logger">Logger.</param>
    protected WavesObservableInitializablePlugin(ILogger<WavesObservableInitializablePlugin> logger)
        : base(logger)
    {
    }
}
