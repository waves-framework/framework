using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Waves.Framework.Core.Base.Interfaces;
using Waves.Framework.Core.Extensions;

namespace Waves.Framework.Core.Base;

/// <summary>
/// Abstraction for configurable object.
/// </summary>
public abstract class WavesConfigurableObject :
    WavesInitializableObject,
    IWavesConfigurableObject
{
    private readonly Dictionary<string, string> _configurations;

    /// <summary>
    /// Creates new instances of <see cref="WavesConfigurableObject"/>.
    /// </summary>
    /// <param name="configuration">Configuration.</param>
    /// <param name="logger">Logger.</param>
    protected WavesConfigurableObject(
        IConfiguration configuration,
        ILogger<WavesConfigurableObject> logger)
        : base(logger)
    {
        _configurations = ConfigurableExtensions.InitializeConfiguration(this, configuration);
    }

    /// <inheritdoc />
    public override async Task InitializeAsync()
    {
        if (IsInitialized)
        {
            return;
        }

        try
        {
            await LoadConfigurationAsync();
            await RunInitializationAsync();
            IsInitialized = true;
        }
        catch (Exception e)
        {
            IsInitialized = false;
            Logger.LogError(e, "Object initialization error");
        }
    }

    /// <summary>
    /// Loads configuration.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    protected virtual Task LoadConfigurationAsync()
    {
        return this.Configure(_configurations, Logger);
    }
}
