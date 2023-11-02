using Waves.Framework.Core.Attributes;
using Waves.Framework.Core.Enums;

namespace Waves.Framework.UI.Core.Attributes
{
    /// <summary>
    /// Attribute for all views.
    /// </summary>
    public class WavesViewAttribute : WavesPluginAttribute
    {
        /// <summary>
        /// Creates new instance of <see cref="WavesViewAttribute"/>.
        /// </summary>
        /// <param name="pluginType">Plugin type.</param>
        /// <param name="lifetimeType">Lifetime.</param>
        /// <param name="key">Key.</param>
        /// <param name="name">Name.</param>
        /// <param name="region">Region.</param>
        public WavesViewAttribute(
            Type pluginType,
            WavesLifetime lifetimeType = WavesLifetime.Transient,
            object? key = null,
            string? name = default,
            string? region = "Main")
            : base(pluginType, lifetimeType, key, name)
        {
            Region = region;
        }

        /// <summary>
        ///     Gets region.
        /// </summary>
        public string? Region { get; }
    }
}
