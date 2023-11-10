using System.Windows;
using Waves.Framework.UI.Presentation.Controls.Interfaces;

namespace Waves.Framework.UI.WPF.Controls;

public class WavesWindow : Window, IWavesWindow<object>
{
    public virtual void Dispose()
    {
        // TODO release managed resources here
    }
}