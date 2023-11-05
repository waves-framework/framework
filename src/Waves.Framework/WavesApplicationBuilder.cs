using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Waves.Framework.Core.Attributes;
using Waves.Framework.Core.Extensions;
using Waves.Framework.Core.Interfaces;
using Waves.Framework.Core.Services;

namespace Waves.Framework;

/// <summary>
/// Waves application builder.
/// </summary>
public class WavesApplicationBuilder : IWavesApplicationBuilder
{
    private readonly string[]? _args;
    
    private ContainerBuilder _containerBuilder;
    private ServiceProvider _serviceProvider;

    private IContainer _container;
    private IWavesServiceRegistry _serviceRegistry;
    private ILogger<WavesApplicationBuilder> _logger;

    /// <summary>
    /// Creates new instance of <see cref="WavesApplicationBuilder"/>
    /// </summary>
    /// <param name="args">Arguments.</param>
    private WavesApplicationBuilder(string[]? args = null)
    {
        _args = args;

        Configuration = new ConfigurationManager();
        Services = new ServiceCollection();
    }

    /// <inheritdoc />
    public IServiceCollection Services { get; }

    /// <inheritdoc />
    public ConfigurationManager Configuration { get; }

    public Action<ILoggingBuilder> Logging { get; set; }

/// <inheritdoc />
    public IWavesApplication Build()
    {
        Services.AddScoped(_ => Configuration);
        Services.AddSingleton<IWavesTypeLoaderService<WavesPluginAttribute>, WavesTypeLoaderService<WavesPluginAttribute>>();
        Services.AddLogging(Logging);
        Services.AddSingleton<IWavesServiceProvider, WavesServiceProvider>();
        
        _serviceProvider = Services.BuildServiceProvider();
        
        InitializeLogger();
        InitializeContainer();
        InitializePlugins();

        _container = _containerBuilder.Build();

        var provider = InitializeServiceProvider();

        return new WavesApplication(provider, Configuration);
    }

private WavesServiceProvider InitializeServiceProvider()
{
    var provider = _container.Resolve<IWavesServiceProvider>() as WavesServiceProvider;
    provider?.InitializeContainer(_container);

    if (provider == null)
    {
        throw new NullReferenceException("Service provider was not initialized");
    }
    
    return provider;
}

/// <summary>
    /// Creates new instance of <see cref="WavesApplicationBuilder"/>.
    /// </summary>
    /// <returns>Returns instance of <see cref="WavesApplicationBuilder"/>.</returns>
    public static WavesApplicationBuilder CreateBuilder()
    {
        return new WavesApplicationBuilder();
    }

    /// <summary>
    /// Creates new instance of <see cref="WavesApplicationBuilder"/>.
    /// </summary>
    /// <param name="args">Input arguments.</param>
    /// <returns>Returns instance of <see cref="WavesApplicationBuilder"/>.</returns>
    public static WavesApplicationBuilder CreateBuilder(string[] args)
    {
        return new WavesApplicationBuilder(args);
    }
    
    /// <summary>
    /// Initializes container.
    /// </summary>
    private void InitializeContainer()
    {
        _containerBuilder = new ContainerBuilder();
        _containerBuilder.Populate(Services);
    }
    
    /// <summary>
    /// Initializes plugins.
    /// </summary>
    private async void InitializePlugins()
    {
        var logger = _serviceProvider.GetService<ILogger<WavesServiceRegistry>>();
        _serviceRegistry = new WavesServiceRegistry(logger, _containerBuilder);
        var typeLoader = _serviceProvider.GetService<IWavesTypeLoaderService<WavesPluginAttribute>>();
        if (typeLoader != null)
        {
            var types = await typeLoader.GetTypesAsync();
            foreach (var pair in types)
            {
                var attribute = pair.Value;
                var registerType = attribute.Type;
                var type = pair.Key;
                var key = attribute.Key;
                var lifetime = attribute.Lifetime;

                await _serviceRegistry.RegisterType(type, registerType, lifetime, key);

                var keyMessage = key != null ? $" with key {key}" : string.Empty;
                _logger.LogDebug(
                    "{Type} registered as {RegisterType} with {Description} lifetime{KeyMessage}",
                    type.GetFriendlyName(),
                    registerType.GetFriendlyName(),
                    lifetime.ToDescription(),
                    keyMessage);
            }
        }
        else
        {
            throw new NullReferenceException("Type loader was not loaded.");
        }
    }
    
    private void InitializeLogger()
    {
        var logger = _serviceProvider.GetService<ILogger<WavesApplicationBuilder>>();
        if (logger != null)
        {
            _logger = logger;
        }
    }
}