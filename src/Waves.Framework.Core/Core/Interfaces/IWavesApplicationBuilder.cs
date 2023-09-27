namespace Waves.Framework.Core.Core.Interfaces;

/// <summary>
/// Interface of Waves application builder.
/// </summary>
public interface IWavesApplicationBuilder
{
    /// <summary>
    /// Builds application.
    /// </summary>
    /// <returns>Returns instance of <see cref="IWavesApplication"/>.</returns>
    IWavesApplication Build();
}