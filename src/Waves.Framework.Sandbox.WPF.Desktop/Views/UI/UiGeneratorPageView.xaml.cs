using System.Windows.Controls;
using Waves.Framework.UI.Attributes;
using Waves.Framework.UI.WPF.Controls;
using Waves.Sandbox.ViewModels.UI;

namespace Waves.Framework.Sandbox.WPF.Desktop.Views.UI;

[WavesView(typeof(UiGeneratorPageViewModel))]
public partial class UiGeneratorView : WavesPage
{
    public UiGeneratorView()
    {
        InitializeComponent();
    }
}