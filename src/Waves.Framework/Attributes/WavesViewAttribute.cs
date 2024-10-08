﻿using Waves.Framework.Enums;
using Waves.Framework.Presentation.Interfaces;

namespace Waves.Framework.Attributes
{
    /// <summary>
    /// Attribute for all views.
    /// </summary>
    public class WavesViewAttribute : WavesPluginAttribute
    {
        /// <summary>
        /// Creates new instance of <see cref="WavesViewAttribute"/>.
        /// </summary>
        /// <param name="lifetimeType">Lifetime.</param>
        /// <param name="key">Key.</param>
        /// <param name="name">Name.</param>
        /// <param name="region">Region.</param>
        public WavesViewAttribute(
            object key,
            WavesLifetime lifetimeType = WavesLifetime.Transient,
            string? name = default,
            string? region = "Main")
            : base(typeof(IWavesView), lifetimeType, key, name)
        {
            Region = region;
        }

        /// <summary>
        ///     Gets region.
        /// </summary>
        public string? Region { get; }
    }
}
