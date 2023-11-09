using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using Microsoft.Extensions.Logging;
using Waves.Framework.Attributes;
using Waves.Framework.Enums;
using Waves.Framework.Interfaces;
using Waves.Framework.UI.Avalonia.Controls;
using Waves.Framework.UI.Extensions;
using Waves.Framework.UI.Presentation.Controls.Interfaces;
using Waves.Framework.UI.Presentation.Interfaces;
using Waves.Framework.UI.Services.Interfaces;

namespace Waves.Framework.UI.Avalonia.Services;

/// <summary>
/// Waves Avalonia Navigation Service.
/// </summary>
[WavesPlugin(typeof(IWavesNavigationService), WavesLifetime.Singleton)]
public class WavesNavigationService : IWavesNavigationService
{
    private readonly ILogger<IWavesNavigationService> _logger;
    private readonly IWavesServiceProvider _serviceProvider;
    private readonly List<IWavesView> _openedWindows = new();
    private readonly Dictionary<string, ContentControl> _contentControls = new();
    private readonly Dictionary<string, Stack<IWavesViewModel>> _history = new();
    private readonly Dictionary<string, Action> _pendingActions = new();
    
    private WavesWindow? _mainWindow;
    private WavesPage? _mainPage;

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
                case IWavesPage<object> page:
                    await InitializePageAsync(page, viewModel, addToHistory);
                    break;
                case IWavesUserControl<object> userControl:
                    await InitializeUserControlAsync(userControl, viewModel, addToHistory);
                    break;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured while navigating");
        }
    }

    public Task NavigateAsync<TParameter>(IWavesParameterizedViewModel<TParameter> viewModel, TParameter parameter,
        bool addToHistory = true)
    {
        throw new NotImplementedException();
    }

    public Task<TResult> NavigateAsync<TResult>(IWavesViewModel<TResult> viewModel, bool addToHistory = true)
    {
        throw new NotImplementedException();
    }

    public Task<TResult> NavigateAsync<TParameter, TResult>(IWavesParameterizedViewModel<TParameter, TResult> viewModel,
        TParameter parameter,
        bool addToHistory = true)
    {
        throw new NotImplementedException();
    }

    public async Task NavigateAsync<T>(bool addToHistory = true) where T : class
    {
        var viewModel = _serviceProvider.GetInstance<T>();
        await NavigateAsync((IWavesViewModel)viewModel, addToHistory);
    }

    public async Task NavigateAsync<T, TParameter>(TParameter parameter, bool addToHistory = true) where T : class
    {
        var viewModel = _serviceProvider.GetInstance<T>();
        await NavigateAsync((IWavesParameterizedViewModel<TParameter>)viewModel, parameter, addToHistory);
    }

    public async Task<TResult> NavigateAsync<T, TResult>(bool addToHistory = true) where T : class
    {
        var viewModel = _serviceProvider.GetInstance<T>();
        return await NavigateAsync((IWavesViewModel<TResult>)viewModel, addToHistory);
    }

    public async Task<TResult> NavigateAsync<T, TParameter, TResult>(TParameter parameter, bool addToHistory = true) where T : class
    {
        var viewModel = _serviceProvider.GetInstance<T>();
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

    private async Task InitializeWindowAsync(IWavesWindow<object> view, IWavesViewModel viewModel)
    {
        // first invoke of this method
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop && _mainWindow == null)
        {
            _mainWindow = view as WavesWindow;

            if (_mainWindow != null)
            {
                desktop.MainWindow = _mainWindow;
            }
        }

        var region = GetRegion(view, viewModel);
        if (region == null)
        {
            throw new NullReferenceException($"Region for control {view.GetType().Name} was not set");
        }
        
        var contentControl = view as ContentControl;
        if (contentControl == null)
        {
            return;
        }

        await Dispatcher.UIThread.InvokeAsync(Action);

        AddContentControl(region, contentControl);
        return;

        void Action()
        {
            view.Show();
            _openedWindows.Add(view);
            RegisterView(contentControl);
            _logger.LogDebug("Navigation to view {ViewType} with data context {ViewModelType} in region {Region} completed", view.GetType(), viewModel.GetType(), region);
        }
    }
    
    private async Task InitializePageAsync(IWavesPage<object> view, IWavesViewModel viewModel, bool addToHistory = true)
    {
        // first invoke of this method
        if (Application.Current?.ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform && _mainPage == null)
        {
            _mainPage = view as WavesPage;

            if (_mainPage != null)
            {
                singleViewPlatform.MainView = _mainPage;
            }
        }

        var region = GetRegion(view, viewModel);
        if (region == null)
        {
            throw new NullReferenceException($"Region for control {view.GetType().Name} was not set");
        }

        if (!_contentControls.ContainsKey(region))
        {
            _pendingActions.Add(region, Action);
        }
        else
        {
            await Dispatcher.UIThread.InvokeAsync(Action);
        }

        return;

        void Action()
        {
            AddToHistoryStack(region, viewModel, addToHistory);
            var contentControl = _contentControls[region];
            if (contentControl is WavesWindow window)
            {
                // window.FrontContent = null;
            }

            if (contentControl.Content != null && contentControl.Content.GetType() == view.GetType())
            {
                return;
            }

            UnregisterView(contentControl.Content);
            _contentControls[region].Content = view;
            RegisterView(contentControl.Content);

            // OnGoBackChanged(
            //     new GoBackNavigationEventArgs(
            //         Histories[region].Count > 1,
            //         _contentControls[region]));

            _logger.LogDebug("Navigation to view {ViewType} with data context {ViewModelType} in region {Region} completed", view.GetType(), viewModel.GetType(), region);
        }
    }
    
    protected async Task InitializeUserControlAsync(IWavesUserControl<object> view, IWavesViewModel viewModel, bool addToHistory = true)
    {
        // var region = await GetRegion(view, viewModel);
        //
        // void Action()
        // {
        //     AddToHistoryStack(region, viewModel, addToHistory);
        //     var contentControl = _contentControls[region];
        //     if (contentControl is WavesWindow window)
        //     {
        //         window.FrontContent = null;
        //     }
        //
        //     if (contentControl.Content != null && contentControl.Content.GetType() == view.GetType())
        //     {
        //         return;
        //     }
        //
        //     UnregisterView(contentControl.Content);
        //     _contentControls[region].Content = view;
        //     RegisterView(contentControl.Content);
        //
        //     OnGoBackChanged(
        //         new GoBackNavigationEventArgs(
        //             Histories[region].Count > 1,
        //             _contentControls[region]));
        //
        //     Logger.LogDebug("Navigation to view {ViewType} with data context {ViewModelType} in region {Region} completed", view.GetType(), viewModel.GetType(), region);
        //     viewModel.RunPostInitializationAsync().FireAndForget();
        // }
        //
        // if (!_contentControls.ContainsKey(region))
        // {
        //     PendingActions.Add(region, Action);
        // }
        // else
        // {
        //     await Dispatcher.UIThread.InvokeAsync(Action);
        // }
    }
    
    private string? GetRegion(IWavesView view, IWavesViewModel viewModel)
    {
        var attribute = view.GetViewAttribute();
        if (attribute == null)
        {
            throw new NullReferenceException("Current view didn't marked as \"WavesView\" attribute");
        }

        return attribute.Region;
    }
    
    private void AddContentControl(string region, ContentControl view)
    {
        if (!_contentControls.ContainsKey(region))
        {
            _contentControls.Add(region, view);
        }
        else
        {
            // rewrite if controls with same region are not equal.
            if (_contentControls[region].Equals(view))
            {
                return;
            }

            _contentControls[region] = view;
        }
    }
    
    private void RegisterView(object? control)
    {
        if (control is not ContentControl contentControl)
        {
            return;
        }

        if (contentControl.DataContext is IWavesViewModel viewModel)
        {
            viewModel.ViewAppeared();
        }
    }
    
    private void UnregisterView(object? control)
    {
        if (control is not ContentControl contentControl)
        {
            return;
        }

        if (contentControl.DataContext is IWavesViewModel viewModel)
        {
            viewModel.ViewDisappeared();
        }

        if (contentControl.DataContext is IDisposable disposable)
        {
            disposable.Dispose();
        }

        if (contentControl is IWavesView view)
        {
            view.Dispose();
        }
    }
    
    private void AddToHistoryStack(string region, IWavesViewModel viewModel, bool addToHistory = true)
    {
        if (!_history.ContainsKey(region))
        {
            _history.Add(region, new Stack<IWavesViewModel>());
        }

        if (addToHistory)
        {
            _history[region].Push(viewModel);
        }
    }
}