using System.Diagnostics;
using Autofac;
using Microsoft.Extensions.Logging;
using Waves.Framework.Extensions;
using Waves.Framework.Interfaces;

namespace Waves.Framework.Services;

/// <summary>
/// Waves service provider.
/// </summary>
internal class WavesServiceProvider : IWavesServiceProvider
{
    private IContainer _container;
    private ILogger<IWavesServiceProvider>? _logger;

    /// <summary>
    /// Gets instance by type and key.
    /// </summary>
    /// <typeparam name="T">Type.</typeparam>
    /// <param name="key">Key.</param>
    /// <returns>Returns instance.</returns>
    public T GetInstance<T>(object key = null)
    {
        var result = key == null
            ? _container.Resolve<T>()
            : _container.ResolveKeyed<T>(key);

#if DEBUG
        _logger?.LogDebug("{Name} resolved from container", result.GetType().GetFriendlyName());
#endif

        return result;
    }

    /// <summary>
    /// Gets instance by type and key.
    /// </summary>
    /// <typeparam name="T">Type.</typeparam>
    /// <param name="key">Key.</param>
    /// <returns>Returns instance.</returns>
    public Task<T> GetInstanceAsync<T>(object key = null)
    {
        var result = key == null
            ? _container.Resolve<T>()
            : _container.ResolveKeyed<T>(key);

#if DEBUG
        _logger?.LogDebug("{Name} resolved from container", result.GetType().GetFriendlyName());
#endif

        return Task.FromResult(result);
    }

    /// <summary>
    /// Gets instance by type and key.
    /// </summary>
    /// <param name="type">Type.</param>
    /// <param name="key">Key.</param>
    /// <returns>Returns instance.</returns>
    public object GetInstance(Type type, object key = null)
    {
        var result = key == null
            ? _container.Resolve(type)
            : _container.ResolveKeyed(key, type);

#if DEBUG
        _logger?.LogDebug("{Name} resolved from container", result.GetType().GetFriendlyName());
#endif

        return result;
    }

    /// <summary>
    /// Gets instance by type and key.
    /// </summary>
    /// <param name="type">Type.</param>
    /// <param name="key">Key.</param>
    /// <returns>Returns instance.</returns>
    public Task<object> GetInstanceAsync(Type type, object key = null)
    {
        var result = key == null
            ? _container.Resolve(type)
            : _container.ResolveKeyed(key, type);

#if DEBUG
        _logger?.LogDebug("{Name} resolved from container", result.GetType().GetFriendlyName());
#endif

        return Task.FromResult(result);
    }

    /// <summary>
    /// Gets instances by type and key.
    /// </summary>
    /// <typeparam name="T">Type.</typeparam>
    /// <param name="key">Key.</param>
    /// <returns>Returns instance.</returns>
    public IEnumerable<T> GetInstances<T>(object key = null)
    {
        var results = key == null
            ? _container.Resolve<IEnumerable<T>>()
            : _container.ResolveKeyed<IEnumerable<T>>(key);

        var e = results.ToList();
        foreach (var result in e)
        {
#if DEBUG
            _logger?.LogDebug("{Name} resolved from container", result.GetType().GetFriendlyName());
#endif
        }

        return e.AsEnumerable();
    }

    /// <summary>
    /// Gets instances by type and key.
    /// </summary>
    /// <typeparam name="T">Type.</typeparam>
    /// <param name="key">Key.</param>
    /// <returns>Returns instance.</returns>
    public Task<IEnumerable<T>> GetInstancesAsync<T>(object key = null)
        where T : class
    {
        var results = key == null
            ? _container.Resolve<IEnumerable<T>>()
            : _container.ResolveKeyed<IEnumerable<T>>(key);

        var e = results.ToList();
        foreach (var result in e)
        {
#if DEBUG
            var stackTrace = new StackTrace(1, false);
            var type = stackTrace.GetFrame(1)?.GetMethod()?.DeclaringType;
            _logger?.LogDebug("{Type} resolved {Name} from container", type.GetFriendlyName(), result.GetType().GetFriendlyName());
#endif
        }

        return Task.FromResult(e.AsEnumerable());
    }

    internal void InitializeContainer(IContainer container)
    {
        _container = container;
        _container.TryResolve(out _logger);
    }
}
