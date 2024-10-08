using Avalonia;
using Microsoft.Extensions.DependencyInjection;
using Waves.Framework.Interfaces;
using Waves.Framework.Services.Interfaces;
using Waves.Framework.UI.Avalonia.Services;

namespace Waves.Framework.UI.Avalonia;

public class WavesAvaloniaApplication : Application
{
    /// <summary>
    /// Gets <see cref="WavesApplication"/>.
    /// </summary>
    public IWavesApplication App { get; protected set; }
    
    /// <summary>
    /// Gets navigation service.
    /// </summary>
    public IWavesNavigationService NavigationService { get; protected set; }
    
    public override void Initialize()
    {
        var builder = WavesApplicationBuilder.CreateBuilder();
        ConfigureServices(builder);
        App = builder.Build();
        NavigationService = App.Services.GetInstance<IWavesNavigationService>();
    }

    protected virtual void ConfigureServices(WavesApplicationBuilder builder)
    {
    }
}