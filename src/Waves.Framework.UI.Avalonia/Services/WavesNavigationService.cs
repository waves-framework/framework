using Microsoft.Extensions.Logging;
using Waves.Framework.Core.Attributes;
using Waves.Framework.Core.Enums;
using Waves.Framework.Core.Interfaces;
using Waves.Framework.UI.Core.Presentation.Controls.Interfaces;
using Waves.Framework.UI.Core.Presentation.Interfaces;
using Waves.Framework.UI.Core.Services.Interfaces;

namespace Waves.Framework.UI.Avalonia.Services;

/// <summary>
/// Waves Avalonia Navigation Service.
/// </summary>
[WavesPlugin(typeof(IWavesNavigationService), WavesLifetime.Singleton)]
public class WavesNavigationService : IWavesNavigationService
{
    private readonly ILogger<IWavesNavigationService> _logger;
    private readonly IWavesServiceProvider _serviceProvider;

    public WavesNavigationService(
        ILogger<IWavesNavigationService> logger,
        IWavesServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }
    
    public Task GoBackAsync(string region)
    {
        throw new NotImplementedException();
    }

    public Task GoBackAsync(IWavesViewModel viewModel)
    {
        throw new NotImplementedException();
    }

    public async Task NavigateAsync(IWavesViewModel viewModel, bool addToHistory = true)
    {
        try
        {
            var view = await _serviceProvider.GetInstanceAsync<IWavesView>(viewModel.GetType());

            switch (view)
            {
                case IWavesWindow<object> window:
                    await InitializeWindowAsync(window, viewModel);
                    break;
                // case IWavesUserControl<TContent> userControl:
                //     await InitializeUserControlAsync(userControl, viewModel, addToHistory);
                //     break;
                // case IWavesPage<TContent> page:
                //     await InitializePageAsync(page, viewModel, addToHistory);
                //     break;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured while navigating");
        }
    }

    public Task NavigateAsync<TParameter>(IWavesParameterizedViewModel<TParameter> viewModel, TParameter parameter, bool addToHistory = true)
    {
        throw new NotImplementedException();
    }

    public Task<TResult> NavigateAsync<TResult>(IWavesViewModel<TResult> viewModel, bool addToHistory = true)
    {
        throw new NotImplementedException();
    }

    public Task<TResult> NavigateAsync<TParameter, TResult>(IWavesParameterizedViewModel<TParameter, TResult> viewModel, TParameter parameter,
        bool addToHistory = true)
    {
        throw new NotImplementedException();
    }

    public async Task NavigateAsync<T>(bool addToHistory = true) where T : class
    {
        var viewModel = await _serviceProvider.GetInstanceAsync<T>();
        await NavigateAsync((IWavesViewModel)viewModel, addToHistory);
    }

    public async Task NavigateAsync<T, TParameter>(TParameter parameter, bool addToHistory = true) where T : class
    {
        var viewModel = await _serviceProvider.GetInstanceAsync<T>();
        await NavigateAsync((IWavesParameterizedViewModel<TParameter>)viewModel, parameter, addToHistory);
    }

    public async Task<TResult> NavigateAsync<T, TResult>(bool addToHistory = true) where T : class
    {
        var viewModel = await _serviceProvider.GetInstanceAsync<T>();
        return await NavigateAsync((IWavesViewModel<TResult>)viewModel, addToHistory);
    }

    public async Task<TResult> NavigateAsync<T, TParameter, TResult>(TParameter parameter, bool addToHistory = true) where T : class
    {
        var viewModel = await _serviceProvider.GetInstanceAsync<T>();
        return await NavigateAsync((IWavesParameterizedViewModel<TParameter, TResult>)viewModel, parameter, addToHistory);
    }

    public async Task NavigateAsync(Type type, bool addToHistory = true)
    {
        var viewModel = await _serviceProvider.GetInstanceAsync(type);
        await NavigateAsync((IWavesViewModel)viewModel, addToHistory);
    }

    public async Task NavigateAsync<TParameter>(Type type, TParameter parameter, bool addToHistory = true)
    {
        var viewModel = await _serviceProvider.GetInstanceAsync(type);
        await NavigateAsync((IWavesParameterizedViewModel<TParameter>)viewModel, parameter, addToHistory);
    }

    public async Task<TResult> NavigateAsync<TResult>(Type type, bool addToHistory = true)
    {
        var viewModel = await _serviceProvider.GetInstanceAsync(type);
        return await NavigateAsync((IWavesViewModel<TResult>)viewModel, addToHistory);
    }

    public async Task<TResult> NavigateAsync<TParameter, TResult>(Type type, TParameter parameter, bool addToHistory = true)
    {
        var viewModel = await _serviceProvider.GetInstanceAsync(type);
        return await NavigateAsync((IWavesParameterizedViewModel<TParameter, TResult>)viewModel, parameter, addToHistory);
    }
    
    /// <inheritdoc />
    protected async Task InitializeWindowAsync(IWavesWindow<object> view, IWavesViewModel viewModel)
    {
        // // first invoke of this method
        // if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop && _mainWindow == null)
        // {
        //     _mainWindow = view as Window;
        //
        //     if (_mainWindow != null)
        //     {
        //         desktop.MainWindow = _mainWindow;
        //     }
        // }
        //
        // var region = await InitializeComponents(view, viewModel);
        // var contentControl = view as ContentControl;
        // if (contentControl == null)
        // {
        //     return;
        // }
        //
        // void Action()
        // {
        //     view.Show();
        //     OpenedWindows.Add(viewModel, view);
        //     RegisterView(contentControl);
        //     Logger.LogDebug("Navigation to view {ViewType} with data context {ViewModelType} in region {Region} completed", view.GetType(), viewModel.GetType(), region);
        //     viewModel.RunPostInitializationAsync().FireAndForget();
        // }
        //
        // await Dispatcher.UIThread.InvokeAsync(Action);
        //
        // AddContentControl(region, contentControl);
    }
}