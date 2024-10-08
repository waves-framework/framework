using Autofac;
using Microsoft.Extensions.Logging;
using Waves.Framework.Enums;
using Waves.Framework.Extensions;
using Waves.Framework.Interfaces;

namespace Waves.Framework.Services;

/// <summary>
/// Waves service registry.
/// </summary>
internal class WavesServiceRegistry : IWavesServiceRegistry
{
    private readonly ContainerBuilder _containerBuilder;
    private readonly ILogger<WavesServiceRegistry> _logger;

    /// <summary>
    /// Creates new instance of <see cref="WavesServiceRegistry"/>.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="containerBuilder">Container builder.</param>
    public WavesServiceRegistry(
        ILogger<WavesServiceRegistry> logger,
        ContainerBuilder containerBuilder)
    {
        _containerBuilder = containerBuilder;
        _logger = logger;
    }

    /// <summary>
    /// Registers type.
    /// </summary>
    /// <param name="type">Type.</param>
    /// <param name="registerType">Registration type.</param>
    /// <param name="lifetime">Lifetime type.</param>
    /// <param name="key">Register key, may be null.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task RegisterType(Type type, Type registerType, WavesLifetime lifetime, object? key)
    {
        try
        {
            switch (lifetime)
            {
                case WavesLifetime.Transient:
                    _containerBuilder.RegisterTransientType(type, registerType, key);
                    break;
                case WavesLifetime.Scoped:
                    _containerBuilder.RegisterScopedType(type, registerType, key);
                    break;
                case WavesLifetime.Singleton:
                    _containerBuilder.RegisterSingletonType(type, registerType, key);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while register type {Name}", type.GetFriendlyName());
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Registers instance.
    /// </summary>
    /// <param name="obj">Current object.</param>
    /// <param name="registerType">Registration type.</param>
    /// <param name="lifetime">Lifetime type.</param>
    /// <param name="key">Register key, may be null.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task RegisterInstance(object obj, Type registerType, WavesLifetime lifetime, object? key)
    {
        try
        {
            switch (lifetime)
            {
                case WavesLifetime.Transient:
                    _containerBuilder.RegisterTransientInstance(obj, registerType, key);
                    break;
                case WavesLifetime.Scoped:
                    _containerBuilder.RegisterScopedInstance(obj, registerType, key);
                    break;
                case WavesLifetime.Singleton:
                    _containerBuilder.RegisterSingletonInstance(obj, registerType, key);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while register instance {Name}", obj.GetType().GetFriendlyName());
        }

        return Task.CompletedTask;
    }
}
