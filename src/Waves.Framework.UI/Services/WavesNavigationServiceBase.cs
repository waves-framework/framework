using Microsoft.Extensions.Logging;
using Waves.Framework.Interfaces;
using Waves.Framework.UI.Extensions;
using Waves.Framework.UI.Presentation.Controls.Interfaces;
using Waves.Framework.UI.Presentation.Interfaces;
using Waves.Framework.UI.Services.Interfaces;

namespace Waves.Framework.UI.Services;

public abstract class WavesNavigationServiceBase<TContent> : IWavesNavigationService
{
    private readonly IWavesServiceProvider _serviceProvider;

    public WavesNavigationServiceBase(ILogger<IWavesNavigationService> logger, IWavesServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        Logger = logger;
    }
    
    protected ILogger<IWavesNavigationService> Logger { get; }

    /// <summary>
    /// Gets dictionary of view models keyed by region.
    /// </summary>
    protected Dictionary<string, Stack<IWavesViewModel>> History { get; } = new();
    
    /// <inheritdoc />
    public Task<bool> CanGoBack(string region)
    {
        var history = History.FirstOrDefault(x => x.Key.Equals(region)).Value;
        return history == null ? Task.FromResult(false) : Task.FromResult(history.Count > 1);
    }

    /// <inheritdoc />
    public async Task GoBackAsync(string region)
    {
        var history = History.FirstOrDefault(x => x.Key.Equals(region)).Value;
        if (history != null)
        {
            if (history.Count <= 1)
            {
                return;
            }

            history.Pop();
            await NavigateAsync(history.First(), false);
        }
    }
    
    /// <inheritdoc />
    public virtual async Task GoBackAsync(IWavesViewModel viewModel)
    {
        // TODO: ???
        foreach (var history in History
                     .Select(pair => pair.Value)
                     .Where(history => Enumerable.Contains(history, viewModel)))
        {
            if (history.Count <= 1)
            {
                return;
            }

            history.Pop();
            await NavigateAsync(history.First(), false);

            return;
        }
    }

    /// <inheritdoc />
    public async Task NavigateAsync<T>(bool addToHistory = true)
        where T : class
    {
        var viewModel = await _serviceProvider.GetInstanceAsync<T>();
        await NavigateAsync((IWavesViewModel)viewModel, addToHistory);
    }

    /// <inheritdoc />
    public async Task NavigateAsync<T, TParameter>(
        TParameter parameter,
        bool addToHistory = true)
        where T : class
    {
        var viewModel = await _serviceProvider.GetInstanceAsync<T>();
        await NavigateAsync((IWavesParameterizedViewModel<TParameter>)viewModel, parameter, addToHistory);
    }

    /// <inheritdoc />
    public async Task<TResult?> NavigateAsync<T, TResult>(
        bool addToHistory = true)
        where T : class
    {
        var viewModel = await _serviceProvider.GetInstanceAsync<T>();
        return await NavigateAsync((IWavesViewModel<TResult?>)viewModel, addToHistory);
    }

    /// <inheritdoc />
    public async Task<TResult?> NavigateAsync<T, TParameter, TResult>(
        TParameter parameter,
        bool addToHistory = true)
        where T : class
    {
        var viewModel = await _serviceProvider.GetInstanceAsync<T>();
        return await NavigateAsync((IWavesParameterizedViewModel<TParameter, TResult>)viewModel, parameter, addToHistory);
    }

    /// <inheritdoc />
    public async Task NavigateAsync(Type type, bool addToHistory = true)
    {
        var viewModel = await _serviceProvider.GetInstanceAsync(type);
        await NavigateAsync((IWavesViewModel)viewModel, addToHistory);
    }

    /// <inheritdoc />
    public async Task NavigateAsync<TParameter>(Type type, TParameter parameter, bool addToHistory = true)
    {
        var viewModel = await _serviceProvider.GetInstanceAsync(type);
        await NavigateAsync((IWavesParameterizedViewModel<TParameter>)viewModel, parameter, addToHistory);
    }

    /// <inheritdoc />
    public async Task<TResult?> NavigateAsync<TResult>(Type type, bool addToHistory = true)
    {
        var viewModel = await _serviceProvider.GetInstanceAsync(type);
        return await NavigateAsync((IWavesViewModel<TResult?>)viewModel, addToHistory);
    }

    /// <inheritdoc />
    public async Task<TResult?> NavigateAsync<TParameter, TResult>(Type type, TParameter parameter, bool addToHistory = true)
    {
        var viewModel = await _serviceProvider.GetInstanceAsync(type);
        return await NavigateAsync((IWavesParameterizedViewModel<TParameter, TResult>)viewModel, parameter, addToHistory);
    }

    /// <inheritdoc />
    public abstract void RegisterContentControl(string region, object contentControl);

    /// <inheritdoc />
    public abstract void UnregisterContentControl(string region);

    /// <inheritdoc />
    public async Task NavigateAsync(IWavesViewModel viewModel, bool addToHistory = true)
    {
        try
        {
            var view = await _serviceProvider.GetInstanceAsync<IWavesView>(viewModel.GetType());

            switch (view)
            {
                case IWavesWindow<TContent> window:
                    await InitializeWindowAsync(window, viewModel);
                    break;
                case IWavesUserControl<TContent> userControl:
                    await InitializeUserControlAsync(userControl, viewModel, addToHistory);
                    break;
                case IWavesPage<TContent> page:
                    await InitializePageAsync(page, viewModel, addToHistory);
                    break;
            }
        }
        catch (Exception e)
        {
            Logger.LogError(e, "An error occured while navigating");
        }
    }

    /// <inheritdoc />
    public async Task NavigateAsync<TParameter>(
        IWavesParameterizedViewModel<TParameter> viewModel,
        TParameter parameter,
        bool addToHistory = true)
    {
        try
        {
            var view = await _serviceProvider.GetInstanceAsync<IWavesView>(viewModel.GetType());

            switch (view)
            {
                case IWavesWindow<TContent> window:
                    await NavigateToWindowAsync(window, viewModel, parameter);
                    break;
                case IWavesUserControl<TContent> userControl:
                    await NavigateToUserControlAsync(userControl, viewModel, parameter, addToHistory);
                    break;
                case IWavesPage<TContent> page:
                    await NavigateToPageAsync(page, viewModel, parameter, addToHistory);
                    break;
            }
        }
        catch (Exception e)
        {
            Logger.LogError(e, "An error occured while navigating");
        }
    }

    /// <inheritdoc />
    public async Task<TResult?> NavigateAsync<TResult>(
        IWavesViewModel<TResult?> viewModel,
        bool addToHistory = true)
    {
        try
        {
            var view = await _serviceProvider.GetInstanceAsync<IWavesView>(viewModel.GetType());

            switch (view)
            {
                case IWavesWindow<TContent> window:
                    return await NavigateToWindowAsync(window, viewModel);
                case IWavesUserControl<TContent> userControl:
                    return await NavigateToUserControlAsync(userControl, viewModel, addToHistory);
                case IWavesPage<TContent> page:
                    return await NavigateToPageAsync(page, viewModel, addToHistory);
            }
        }
        catch (Exception e)
        {
            Logger.LogError(e, "An error occured while navigating");
        }

        return default;
    }

    /// <inheritdoc />
    public async Task<TResult?> NavigateAsync<TParameter, TResult>(
        IWavesParameterizedViewModel<TParameter, TResult> viewModel,
        TParameter parameter,
        bool addToHistory = true)
    {
        try
        {
            var view = await _serviceProvider.GetInstanceAsync<IWavesView>(viewModel.GetType());

            switch (view)
            {
                case IWavesWindow<TContent> window:
                    return await NavigateToWindowAsync(window, viewModel, parameter);
                case IWavesUserControl<TContent> userControl:
                    return await NavigateToUserControlAsync(userControl, viewModel, parameter, addToHistory);
                case IWavesPage<TContent> page:
                    return await NavigateToPageAsync(page, viewModel, parameter, addToHistory);
            }
        }
        catch (Exception e)
        {
            Logger.LogError(e, "An error occured while navigating");
        }

        return default;
    }

    /// <summary>
    /// Navigates to windows.
    /// </summary>
    /// <param name="view">Window view.</param>
    /// <param name="viewModel">ViewModel.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    protected abstract Task InitializeWindowAsync(
        IWavesWindow<TContent> view,
        IWavesViewModel viewModel);

    /// <summary>
    /// Navigates to page.
    /// </summary>
    /// <param name="view">Page view.</param>
    /// <param name="viewModel">View model.</param>
    /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    protected abstract Task InitializePageAsync(
        IWavesPage<TContent> view,
        IWavesViewModel viewModel,
        bool addToHistory = true);

    /// <summary>
    /// Navigates to user control.
    /// </summary>
    /// <param name="view">User control view.</param>
    /// <param name="viewModel">View model.</param>
    /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    protected abstract Task InitializeUserControlAsync(
        IWavesUserControl<TContent> view,
        IWavesViewModel viewModel,
        bool addToHistory = true);

    /// <summary>
    /// Adds viewModel to history stack or just create new history stack by region.
    /// </summary>
    /// <param name="region">Region.</param>
    /// <param name="viewModel">View model.</param>
    /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
    protected void AddToHistoryStack(string region, IWavesViewModel viewModel, bool addToHistory = true)
    {
        if (!History.ContainsKey(region))
        {
            History.Add(region, new Stack<IWavesViewModel>());
        }

        if (addToHistory)
        {
            History[region].Push(viewModel);
        }
    }

    /// <summary>
    /// Initializes View and ViewModel and return it's region.
    /// </summary>
    /// <param name="view">View.</param>
    /// <returns>Returns region.</returns>
    protected string? GetRegion(IWavesView view)
    {
        var attribute = view.GetViewAttribute();
        if (attribute == null)
        {
            throw new NullReferenceException("Current view didn't marked as \"WavesView\" attribute");
        }

        var region = attribute.Region;
        return region;
    }
    
    /// <summary>
    /// Appears view.
    /// </summary>
    /// <param name="control"></param>
    protected void AppearView(object? control)
    {
        if (control is not IWavesView view)
        {
            return;
        }
        
        if (view.DataContext is IWavesViewModel viewModel)
        {
            viewModel.ViewAppeared();
        }
    }
    
    protected void DisappearView(object? control)
    {
        if (control is not IWavesView view)
        {
            return;
        }
        if (view.DataContext is IWavesViewModel viewModel)
        {
            viewModel.ViewDisappeared();
        }
        if (view.DataContext is IDisposable disposable)
        {
            disposable.Dispose();
        }
        
        view.Dispose();
    }

    /// <summary>
    /// Navigates to windows.
    /// </summary>
    /// <param name="view">Window view.</param>
    /// <param name="viewModel">ViewModel.</param>
    private async Task<TResult> NavigateToWindowAsync<TResult>(
        IWavesWindow<TContent> view,
        IWavesViewModel<TResult> viewModel)
    {
        var completionSource = new TaskCompletionSource<TResult>();
        await InitializeWindowAsync(view, viewModel);
        InitializeControlWithResult(viewModel, completionSource);
        return await completionSource.Task;
    }

    /// <summary>
    ///     Navigates to window.
    /// </summary>
    /// <param name="view">Window view.</param>
    /// <param name="viewModel">ViewModel.</param>
    /// <param name="parameter">Parameter.</param>
    private async Task NavigateToWindowAsync<TParameter>(
        IWavesWindow<TContent> view,
        IWavesParameterizedViewModel<TParameter> viewModel,
        TParameter parameter)
    {
        await viewModel.Prepare(parameter);
        await InitializeWindowAsync(view, viewModel);
    }

    /// <summary>
    ///     Navigates to window.
    /// </summary>
    /// <param name="view">Window view.</param>
    /// <param name="viewModel">ViewModel.</param>
    /// <param name="parameter">Parameter.</param>
    private async Task<TResult> NavigateToWindowAsync<TParameter, TResult>(
        IWavesWindow<TContent> view,
        IWavesParameterizedViewModel<TParameter, TResult> viewModel,
        TParameter parameter)
    {
        var completionSource = new TaskCompletionSource<TResult>();
        await viewModel.Prepare(parameter);
        await InitializeWindowAsync(view, viewModel);
        InitializeControlWithResult(viewModel, completionSource);
        return await completionSource.Task;
    }

    /// <summary>
    ///     Navigates to page.
    /// </summary>
    /// <param name="view">Page view.</param>
    /// <param name="viewModel">View model.</param>
    /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
    private async Task<TResult> NavigateToPageAsync<TResult>(
        IWavesPage<TContent> view,
        IWavesViewModel<TResult> viewModel,
        bool addToHistory = true)
    {
        var completionSource = new TaskCompletionSource<TResult>();
        await InitializePageAsync(view, viewModel, addToHistory);
        InitializeControlWithResult(viewModel, completionSource);
        return await completionSource.Task;
    }

    /// <summary>
    ///     Navigates to page.
    /// </summary>
    /// <param name="view">Page view.</param>
    /// <param name="viewModel">View model.</param>
    /// <param name="parameter">Parameter.</param>
    /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
    private async Task NavigateToPageAsync<TParameter>(
        IWavesPage<TContent> view,
        IWavesParameterizedViewModel<TParameter> viewModel,
        TParameter parameter,
        bool addToHistory = true)
    {
        await viewModel.Prepare(parameter);
        await InitializePageAsync(view, viewModel, addToHistory);
    }

    /// <summary>
    ///     Navigates to page.
    /// </summary>
    /// <param name="view">Page view.</param>
    /// <param name="viewModel">View model.</param>
    /// <param name="parameter">Parameter.</param>
    /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
    private async Task<TResult> NavigateToPageAsync<TParameter, TResult>(
        IWavesPage<TContent> view,
        IWavesParameterizedViewModel<TParameter, TResult> viewModel,
        TParameter parameter,
        bool addToHistory = true)
    {
        var completionSource = new TaskCompletionSource<TResult>();
        await viewModel.Prepare(parameter);
        await InitializePageAsync(view, viewModel, addToHistory);
        InitializeControlWithResult(viewModel, completionSource);
        return await completionSource.Task;
    }

    /// <summary>
    ///     Navigates to user control.
    /// </summary>
    /// <param name="view">User control view.</param>
    /// <param name="viewModel">View model.</param>
    /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
    private async Task<TResult> NavigateToUserControlAsync<TResult>(
        IWavesUserControl<TContent> view,
        IWavesViewModel<TResult> viewModel,
        bool addToHistory = true)
    {
        var completionSource = new TaskCompletionSource<TResult>();
        await InitializeUserControlAsync(view, viewModel, addToHistory);
        InitializeControlWithResult(viewModel, completionSource);
        return await completionSource.Task;
    }

    /// <summary>
    ///     Navigates to user control.
    /// </summary>
    /// <param name="view">User control view.</param>
    /// <param name="viewModel">View model.</param>
    /// <param name="parameter">Parameter.</param>
    /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
    private async Task NavigateToUserControlAsync<TParameter>(
        IWavesUserControl<TContent> view,
        IWavesParameterizedViewModel<TParameter> viewModel,
        TParameter parameter,
        bool addToHistory = true)
    {
        await viewModel.Prepare(parameter);
        await InitializeUserControlAsync(view, viewModel, addToHistory);
    }

    /// <summary>
    ///     Navigates to user control.
    /// </summary>
    /// <param name="view">User control view.</param>
    /// <param name="viewModel">View model.</param>
    /// <param name="parameter">Parameter.</param>
    /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
    private async Task<TResult> NavigateToUserControlAsync<TParameter, TResult>(
        IWavesUserControl<TContent> view,
        IWavesParameterizedViewModel<TParameter, TResult> viewModel,
        TParameter parameter,
        bool addToHistory = true)
    {
        var completionSource = new TaskCompletionSource<TResult>();
        await viewModel.Prepare(parameter);
        await InitializeUserControlAsync(view, viewModel, addToHistory);
        InitializeControlWithResult(viewModel, completionSource);
        return await completionSource.Task;
    }

    /// <summary>
    /// Initializes control with result.
    /// </summary>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="viewModel">View model.</param>
    /// <param name="completionSource">Completion source.</param>
    private void InitializeControlWithResult<TResult>(
        IWavesViewModel<TResult> viewModel,
        TaskCompletionSource<TResult> completionSource)
    {
        void OnResultApproved(object sender, System.EventArgs e)
        {
            Unsubscribe();
            if (viewModel.Result != null)
            {
                completionSource.SetResult(viewModel.Result);
            }
        }
        
        void Unsubscribe()
        {
            viewModel.ResultApproved -= OnResultApproved;
        }
        
        viewModel.ResultApproved += OnResultApproved;
    }
}