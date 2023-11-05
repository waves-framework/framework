using System.Runtime.CompilerServices;
using Waves.Framework.Core.Enums;

namespace Waves.Framework.Core.Attributes;

/// <summary>
///     Attribute for plugin.
/// </summary>
public class WavesPluginAttribute : Attribute
{
    /// <summary>
    ///     Creates new instance of <see cref="WavesPluginAttribute" />.
    /// </summary>
    /// <param name="key">Registration key.</param>
    /// <param name="pluginType">Plugin type.</param>
    /// <param name="lifetimeType">Plugin lifetime type.</param>
    /// <param name="name">Name of plugin.</param>
    public WavesPluginAttribute(
        Type pluginType,
        WavesLifetime lifetimeType = WavesLifetime.Transient,
        object? key = null,
        [CallerMemberName] string? name = default)
    {
        Type = pluginType;
        Lifetime = lifetimeType;
        Key = key;
        Name = pluginType.Name;
    }

    /// <summary>
    ///     Gets whether plugin must has single instance when registering in container.
    /// </summary>
    public WavesLifetime Lifetime { get; }
    
    /// <summary>
    /// Gets name.
    /// </summary>
    public string Name { get; protected set; }

    /// <summary>
    ///     Gets key.
    /// </summary>
    public object? Key { get; }

    /// <summary>
    ///     Gets plugin type.
    /// </summary>
    public Type Type { get; }
}
