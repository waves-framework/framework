using Waves.Framework.Attributes;
using Waves.Framework.UI.WPF.Controls;
using Waves.Sandbox.ViewModels;

namespace Waves.Framework.Sandbox.WPF.Desktop.Views;

[WavesView(typeof(MainPageViewModel))]
public partial class MainPageView : WavesPage
{
    public MainPageView()
    {
        InitializeComponent();
    }
}