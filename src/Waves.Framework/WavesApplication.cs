using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Waves.Framework.Interfaces;

namespace Waves.Framework;

public class WavesApplication : IWavesApplication
{
    /// <summary>
    /// Creates new instance of <see cref="WavesApplication"/>.
    /// </summary>
    /// <param name="services">Services.</param>
    /// <param name="configuration">Configuration.</param>
    internal WavesApplication(
        IWavesServiceProvider services,
        IConfiguration configuration)
    {
        Services = services;
        Configuration = configuration;
        Logger = services.GetInstance<ILogger<WavesApplication>>();
    }

    /// <inheritdoc />
    public IWavesServiceProvider Services { get; }
    
    /// <inheritdoc />
    public IConfiguration Configuration { get; }
    
    /// <inheritdoc />
    public ILogger<IWavesApplication> Logger { get; }
}