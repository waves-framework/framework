using Waves.Framework.Enums;

namespace Waves.Framework.Interfaces;

/// <summary>
/// Waves service registry.
/// </summary>
internal interface IWavesServiceRegistry
{
    /// <summary>
    /// Registers type.
    /// </summary>
    /// <param name="type">Type.</param>
    /// <param name="registerType">Registration type.</param>
    /// <param name="lifetime">Lifetime type.</param>
    /// <param name="key">Register key, may be null.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task RegisterType(Type type, Type registerType, WavesLifetime lifetime, object? key = null);

    /// <summary>
    /// Registers instance.
    /// </summary>
    /// <param name="obj">Current object.</param>
    /// <param name="registerType">Registration type.</param>
    /// <param name="lifetime">Lifetime type.</param>
    /// <param name="key">Register key, may be null.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task RegisterInstance(object obj, Type registerType, WavesLifetime lifetime, object? key = null);
}
