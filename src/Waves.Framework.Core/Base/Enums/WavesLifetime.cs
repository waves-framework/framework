using System.ComponentModel;

namespace Waves.Framework.Core.Base.Enums;

/// <summary>
/// Enum for lifetime of plugins.
/// </summary>
public enum WavesLifetime
{
    /// <summary>
    /// Transient.
    /// </summary>
    [Description("Trasnient")]
    Transient,

    /// <summary>
    /// Scoped.
    /// </summary>
    [Description("Scoped")]
    Scoped,

    /// <summary>
    /// Singleton.
    /// </summary>
    [Description("Singleton")]
    Singleton,
}
