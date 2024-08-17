using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Microsoft.Extensions.Logging;
using Waves.Framework.Attributes;
using Waves.Framework.Enums;
using Waves.Framework.Interfaces;
using Waves.Framework.Presentation.Controls.Interfaces;
using Waves.Framework.Presentation.Interfaces;
using Waves.Framework.Services;
using Waves.Framework.Services.Interfaces;
using Waves.Framework.UI.WPF.Controls;

namespace Waves.Framework.UI.WPF.Services;

[WavesPlugin(typeof(IWavesNavigationService), WavesLifetime.Singleton)]
public class WavesNavigationService : WavesNavigationServiceBase<object>
{
    private readonly List<IWavesView> _openedWindows = new();
    private readonly Dictionary<string, ContentControl> _contentControls = new();
    private readonly Dictionary<string, Stack<IWavesViewModel>> _history = new();
    private readonly Dictionary<string, Action> _pendingActions = new();
    private WavesWindow? _mainWindow;
    private WavesPage? _mainPage;
    
    /// <summary>
    /// Creates new instance of <see cref="WavesNavigationService"/>
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="serviceProvider">Service provider.</param>
    public WavesNavigationService(ILogger<IWavesNavigationService> logger, IWavesServiceProvider serviceProvider) : base(logger, serviceProvider)
    {
    }

    /// <inheritdoc />
    public override void RegisterContentControl(string region, object contentControl)
    {
        if (contentControl is not ContentControl view)
        {
            throw new Exception("Content control is not implemented by IWavesView");
        }
        
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

    /// <inheritdoc />
    public override void UnregisterContentControl(string region)
    {
        if (_contentControls.ContainsKey(region))
        {
            _contentControls.Remove(region);
        }
    }

    /// <inheritdoc />
    protected override async Task InitializeWindowAsync(IWavesWindow<object> view, IWavesViewModel viewModel)
    {
        // first invoke of this method
        if (Application.Current.MainWindow == null && _mainWindow == null)
        {
            _mainWindow = view as WavesWindow;
            if (_mainWindow != null)
            {
                Application.Current.MainWindow  = _mainWindow;
            }
        }
        var region = GetRegion(view);
        if (region == null)
        {
            throw new NullReferenceException($"Region for control {view.GetType().Name} was not set");
        }
        var contentControl = view as ContentControl;
        if (contentControl == null)
        {
            return;
        }
        
        RegisterContentControl(region, contentControl);
        AppearView(contentControl);
        view.DataContext = viewModel;
        void Action()
        {
            view.Show();
            _openedWindows.Add(view);
            Logger.LogDebug("Navigation to view {ViewType} with data context {ViewModelType} in region {Region} completed", view.GetType(), viewModel.GetType(), region);
        }

        await Dispatcher.CurrentDispatcher.InvokeAsync(Action);
    }

    /// <inheritdoc />
    protected override async Task InitializePageAsync(IWavesPage<object> view, IWavesViewModel viewModel, bool addToHistory = true)
    {
        var region = GetRegion(view);
        if (region == null)
        {
            throw new NullReferenceException($"Region for control {view.GetType().Name} was not set");
        }
        
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
        
        DisappearView(contentControl.Content);
        view.DataContext = viewModel;
        contentControl.Content = view;
        AppearView(contentControl.Content);
        
        Logger.LogDebug("Navigation to view {ViewType} with data context {ViewModelType} in region {Region} completed", view.GetType(), viewModel.GetType(), region);
    }

    /// <inheritdoc />
    protected override Task InitializeUserControlAsync(IWavesUserControl<object> view, IWavesViewModel viewModel, bool addToHistory = true)
    {
        throw new NotImplementedException();
    }
}