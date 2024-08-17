using System.Windows.Controls;
using Waves.Framework.Presentation.Controls.Interfaces;

namespace Waves.Framework.UI.WPF.Controls;

public class WavesPage : UserControl, IWavesPage<object>
{
    public virtual void Dispose()
    {
        // TODO release managed resources here
    }
}