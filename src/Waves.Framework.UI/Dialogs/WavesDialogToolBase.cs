using ReactiveUI;
using Waves.Framework.UI.Dialogs.Interfaces;

namespace Waves.Framework.UI.Dialogs
{
    /// <summary>
    /// Dialog action base.
    /// </summary>
    public abstract class WavesDialogToolBase : ReactiveObject, IWavesDialogTool
    {
        /// <inheritdoc />
        public abstract string Caption { get; }

        /// <inheritdoc />
        public abstract string ToolTip { get; }

        /// <inheritdoc />
        public abstract void Initialize();
    }
}
