using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Logging;
using Waves.Framework.Sandbox.Avalonia.Views;
using Waves.Framework.UI.Avalonia;
using Waves.Sandbox.ViewModels;

namespace Waves.Framework.Sandbox.Avalonia;

public partial class App : WavesAvaloniaApplication
{
    public override void Initialize()
    {
        base.Initialize();
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            NavigationService.NavigateAsync<MainWindowViewModel>();
            NavigationService.NavigateAsync<MainPageViewModel>();
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            NavigationService.NavigateAsync<MainPageViewModel>();
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