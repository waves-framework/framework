using System.Windows;
using Waves.Framework.UI.Attributes;
using Waves.Framework.UI.WPF.Controls;
using Waves.Sandbox.ViewModels;

namespace Waves.Framework.Sandbox.WPF.Desktop.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [WavesView(typeof(MainWindowViewModel))]
    public partial class MainWindowView : WavesWindow
    {
        public MainWindowView()
        {
            InitializeComponent();
        }
    }
}
