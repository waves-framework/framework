using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Waves.Framework.Interfaces;
using Waves.Framework.Services.Interfaces;

namespace Waves.Framework.UI.WPF;

public class WavesWpfApplication : Application, IWavesUiApplication
{
    /// <summary>
    /// Gets <see cref="WavesApplication"/>.
    /// </summary>
    public IWavesApplication App { get; protected set; }
    
    /// <summary>
    /// Gets navigation service.
    /// </summary>
    public IWavesNavigationService NavigationService { get; protected set; }
    
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        var builder = WavesApplicationBuilder.CreateBuilder();
        ConfigureServices(builder);
        App = builder.Build();
        NavigationService = App.Services.GetInstance<IWavesNavigationService>();
    }
    
    protected virtual void ConfigureServices(WavesApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IWavesUiApplication>(this);
    }
}