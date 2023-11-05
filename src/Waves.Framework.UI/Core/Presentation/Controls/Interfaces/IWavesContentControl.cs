using Waves.Framework.UI.Core.Presentation.Interfaces;

namespace Waves.Framework.UI.Core.Presentation.Controls.Interfaces
{
    /// <summary>
    /// Interface for content controls.
    /// </summary>
    /// <typeparam name="TContent">Content control type.</typeparam>
    public interface IWavesContentControl<TContent> :
        IWavesView
    {
        /// <summary>
        /// Gets or sets content.
        /// </summary>
        TContent Content { get; set; }
    }
}
