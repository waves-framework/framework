using Waves.Framework.UI.Presentation.Interfaces;

namespace Waves.Framework.UI.Presentation.Controls.Interfaces
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
