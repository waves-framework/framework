using Avalonia;
using Waves.Framework.Core.Interfaces;
using Waves.Framework.UI.Core.Services.Interfaces;

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