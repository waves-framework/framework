using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Logging;
using Waves.Framework.Sandbox.Avalonia.ViewModels;
using Waves.Framework.Sandbox.Avalonia.Views;
using Waves.Framework.UI.Avalonia;

namespace Waves.Framework.Sandbox.Avalonia;

public partial class App : WavesAvaloniaApplication
{
    public override void Initialize()
    {
        base.Initialize();
        AvaloniaXamlLoader.Load(this);
    }

    public async override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // desktop.MainWindow = new MainWindow
            // {
            //     DataContext = new MainViewModel()
            // };

            await NavigationService.NavigateAsync<MainWindowViewModel>();
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    protected override void ConfigureServices(WavesApplicationBuilder builder)
    {
        // logging
        builder.Logging = loggingBuilder =>
        {
            loggingBuilder.AddConsole();
        };
    }
}