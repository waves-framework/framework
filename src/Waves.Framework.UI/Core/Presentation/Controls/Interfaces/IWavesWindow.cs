using System.Windows.Input;

namespace Waves.Framework.UI.Core.Presentation.Controls.Interfaces
{
    /// <summary>
    /// Interface for window controls.
    /// </summary>
    /// <typeparam name="TContent">Content control type.</typeparam>
    public interface IWavesWindow<TContent> : IWavesContentControl<TContent>
    {
        /// <summary>
        /// Shows window.
        /// </summary>
        void Show();
    }
}
