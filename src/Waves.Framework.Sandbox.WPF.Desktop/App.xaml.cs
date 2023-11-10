using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Logging;
using Waves.Framework.UI.WPF;
using Waves.Sandbox.ViewModels;

namespace Waves.Framework.Sandbox.WPF.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : WavesWpfApplication
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            await NavigationService.NavigateAsync<MainWindowViewModel>();
            await NavigationService.NavigateAsync<MainPageViewModel>();
        }

        protected override void ConfigureServices(WavesApplicationBuilder builder)
        {
            base.ConfigureServices(builder);
            
            // logging
            builder.Logging = loggingBuilder =>
            {
                loggingBuilder.AddConsole();
            };
        }
    }
}
