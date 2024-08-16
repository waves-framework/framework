using Avalonia.Controls;
using Waves.Framework.Presentation.Controls.Interfaces;

namespace Waves.Framework.UI.Avalonia.Controls;

public class WavesWindow : Window, IWavesWindow<object>
{
    public virtual void Dispose()
    {
        // TODO release managed resources here
    }
}