namespace Waves.Framework.UI.Core.Interfaces
{
    /// <summary>
    /// Interface for all views.
    /// </summary>
    public interface IWavesView :
        IDisposable
    {
        /// <summary>
        ///     Gets or sets view model context.
        /// </summary>
        object DataContext { get; set; }
    }
}
