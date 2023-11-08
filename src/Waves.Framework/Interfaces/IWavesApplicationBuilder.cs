using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Waves.Framework.Interfaces;

/// <summary>
/// Interface of Waves application builder.
/// </summary>
internal interface IWavesApplicationBuilder
{
    /// <summary>
    /// Gets service collection.
    /// </summary>
    IServiceCollection Services { get; }   
    
    /// <summary>
    /// Gets configuration manager.
    /// </summary>
    ConfigurationManager Configuration { get; }
    
    /// <summary>
    /// Builds application.
    /// </summary>
    /// <returns>Returns instance of <see cref="IWavesApplication"/>.</returns>
    IWavesApplication Build();
}