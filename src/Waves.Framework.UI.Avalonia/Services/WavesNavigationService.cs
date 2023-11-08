using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
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
    
    private WavesWindow? _mainWindow;
    private WavesPage? _mainPage;

    public WavesNavigationService(
        ILogger<IWavesNavigationService> logger,
        IWavesServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }
    
    public void GoBack(string region)
    {
        throw new NotImplementedException();
    }

    public void GoBack(IWavesViewModel viewModel)
    {
        throw new NotImplementedException();
    }

    public async void Navigate(IWavesViewModel viewModel, bool addToHistory = true)
    {
        try
        {
            var view = await _serviceProvider.GetInstanceAsync<IWavesView>(viewModel.GetType());

            switch (view)
            {
                case IWavesWindow<object> window:
                    InitializeWindow(window, viewModel);
                    break;
                case IWavesPage<object> page:
                    InitializePage(page, viewModel, addToHistory);
                    break;
                // case IWavesUserControl<TContent> userControl:
                //     await InitializeUserControlAsync(userControl, viewModel, addToHistory);
                //     break;

            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured while navigating");
        }
    }

    public void Navigate<TParameter>(IWavesParameterizedViewModel<TParameter> viewModel, TParameter parameter,
        bool addToHistory = true)
    {
        throw new NotImplementedException();
    }

    public TResult Navigate<TResult>(IWavesViewModel<TResult> viewModel, bool addToHistory = true)
    {
        throw new NotImplementedException();
    }

    public TResult Navigate<TParameter, TResult>(IWavesParameterizedViewModel<TParameter, TResult> viewModel,
        TParameter parameter,
        bool addToHistory = true)
    {
        throw new NotImplementedException();
    }

    public void Navigate<T>(bool addToHistory = true) where T : class
    {
        var viewModel = _serviceProvider.GetInstance<T>();
        Navigate((IWavesViewModel)viewModel, addToHistory);
    }

    public void Navigate<T, TParameter>(TParameter parameter, bool addToHistory = true) where T : class
    {
        var viewModel = _serviceProvider.GetInstance<T>();
        Navigate((IWavesParameterizedViewModel<TParameter>)viewModel, parameter, addToHistory);
    }

    public TResult Navigate<T, TResult>(bool addToHistory = true) where T : class
    {
        var viewModel = _serviceProvider.GetInstance<T>();
        return Navigate((IWavesViewModel<TResult>)viewModel, addToHistory);
    }

    public TResult Navigate<T, TParameter, TResult>(TParameter parameter, bool addToHistory = true) where T : class
    {
        var viewModel = _serviceProvider.GetInstance<T>();
        return Navigate((IWavesParameterizedViewModel<TParameter, TResult>)viewModel, parameter, addToHistory);
    }

    public void Navigate(Type type, bool addToHistory = true)
    {
        var viewModel = _serviceProvider.GetInstance(type);
        Navigate((IWavesViewModel)viewModel, addToHistory);
    }

    public void Navigate<TParameter>(Type type, TParameter parameter, bool addToHistory = true)
    {
        var viewModel = _serviceProvider.GetInstance(type);
        Navigate((IWavesParameterizedViewModel<TParameter>)viewModel, parameter, addToHistory);
    }

    public TResult Navigate<TResult>(Type type, bool addToHistory = true)
    {
        var viewModel = _serviceProvider.GetInstance(type);
        return Navigate((IWavesViewModel<TResult>)viewModel, addToHistory);
    }

    public TResult Navigate<TParameter, TResult>(Type type, TParameter parameter, bool addToHistory = true)
    {
        var viewModel = _serviceProvider.GetInstance(type);
        return Navigate((IWavesParameterizedViewModel<TParameter, TResult>)viewModel, parameter, addToHistory);
    }

    private void InitializeWindow(IWavesWindow<object> view, IWavesViewModel viewModel)
    {
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
        
        view.DataContext = viewModel;
        
        var contentControl = view as ContentControl;
        if (contentControl == null)
        {
            return;
        }
        
        view.Show();
        _openedWindows.Add(view);
        RegisterView(contentControl);
        AddContentControl(region, contentControl);
        
        _logger.LogDebug("Navigation to view {ViewType} with data context {ViewModelType} in region {Region} completed", view.GetType(), viewModel.GetType(), region);
    }
    
    private void InitializePage(IWavesPage<object> view, IWavesViewModel viewModel, bool addToHistory = true)
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
        
        view.DataContext = viewModel;
        
        AddToHistoryStack(region, viewModel, addToHistory);
        var contentControl = _contentControls[region];
        if (contentControl.Content != null && contentControl.Content.GetType() == view.GetType())
        {
            return;
        }

        UnregisterView(contentControl.Content);
        _contentControls[region].Content = view;
        RegisterView(contentControl.Content);

        _logger.LogDebug("Navigation to view {ViewType} with data context {ViewModelType} in region {Region} completed", view.GetType(), viewModel.GetType(), region);
    }
    
    /// <summary>
    /// Initializes View and ViewModel and return it's region.
    /// </summary>
    /// <param name="view">View.</param>
    /// <param name="viewModel">View model.</param>
    /// <returns>Returns region.</returns>
    private string? GetRegion(IWavesView view, IWavesViewModel viewModel)
    {
        var attribute = view.GetViewAttribute();
        if (attribute == null)
        {
            throw new NullReferenceException("Current view didn't marked as \"WavesView\" attribute");
        }

        return attribute.Region;
    }
    
    /// <summary>
    /// Adds new window to content control dictionary.
    /// </summary>
    /// <param name="region">Region.</param>
    /// <param name="view">Content control.</param>
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
    
    /// <summary>
    /// Invokes <see cref="IWavesViewModel.ViewAppeared"/> for <see cref="StyledElement"/> is current <see cref="ContentControl"/>.
    /// </summary>
    /// <param name="control">Instance of <see cref="ContentControl"/>.</param>
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

    /// <summary>
    /// Invokes <see cref="IWavesViewModel.ViewDisappeared"/> for <see cref="StyledElement"/> is current <see cref="ContentControl"/>.
    /// </summary>
    /// <param name="control">Instance of <see cref="ContentControl"/>.</param>
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