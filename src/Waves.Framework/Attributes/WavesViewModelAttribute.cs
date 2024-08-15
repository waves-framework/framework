using Waves.Framework.Enums;

namespace Waves.Framework.Attributes
{
    /// <summary>
    /// Attribute for all view models.
    /// </summary>
    public class WavesViewModelAttribute : WavesPluginAttribute
    {
        /// <summary>
        /// Creates new instance of <see cref="WavesViewModelAttribute"/>.
        /// </summary>
        /// <param name="pluginType">Plugin type.</param>
        /// <param name="lifetimeType">Lifetime.</param>
        /// <param name="key">Key.</param>
        /// <param name="name">Name.</param>
        public WavesViewModelAttribute(
            Type pluginType,
            WavesLifetime lifetimeType = WavesLifetime.Transient,
            object? key = null,
            string? name = default)
            : base(pluginType, lifetimeType, key, name)
        {
        }
    }
}
