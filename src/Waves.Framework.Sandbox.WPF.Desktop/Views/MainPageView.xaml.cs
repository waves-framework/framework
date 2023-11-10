using System.Windows.Controls;
using Waves.Framework.UI.Attributes;
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