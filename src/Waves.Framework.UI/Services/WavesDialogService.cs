using Microsoft.Extensions.Logging;
using Waves.Framework.Core._old.Base;
using Waves.Framework.Core._old.Base.Attributes;
using Waves.Framework.Core._old.Base.Interfaces;
using Waves.Framework.Core._old.Extensions;
using Waves.Framework.Core.Base;
using Waves.Framework.UI.Dialogs;
using Waves.Framework.UI.Dialogs.Enums;
using Waves.Framework.UI.Dialogs.Parameters;
using Waves.Framework.UI.Services.Interfaces;

namespace Waves.Framework.UI.Services;

/// <summary>
/// Creates new instance of <see cref="WavesDialogService"/>.
/// </summary>
[WavesPlugin(typeof(IWavesDialogService))]
public class WavesDialogService :
    WavesPlugin,
    IWavesDialogService
{
    private readonly IWavesNavigationService _navigationService;
    private readonly ILogger<WavesDialogService> _logger;

    /// <summary>
    /// Creates new instance of <see cref="WavesDialogService"/>.
    /// </summary>
    /// <param name="navigationService">Navigation service.</param>
    /// <param name="logger">Logger.</param>
    public WavesDialogService(
        IWavesNavigationService navigationService,
        ILogger<WavesDialogService> logger)
    {
        _navigationService = navigationService;
        _logger = logger;

        SubscribeEvents();
    }

    /// <inheritdoc />
    public event EventHandler DialogsShown;

    /// <inheritdoc />
    public event EventHandler DialogsHidden;

    /// <inheritdoc />
    public Task<WavesDialogResult> ShowDialogAsync(
        string text,
        string title = null,
        string sender = null,
        WavesDialogMessageType type = WavesDialogMessageType.Information,
        WavesDialogButtons buttons = WavesDialogButtons.Ok)
    {
        return _navigationService.NavigateAsync<WavesMessageDialogViewModel, WavesMessageDialogParameter, WavesDialogResult>(
            new WavesMessageDialogParameter(text, title, sender, null, type, buttons));
    }

    /// <inheritdoc />
    public Task<WavesDialogResult> ShowDialogAsync(
        string text,
        string title = null,
        IWavesObject sender = null,
        WavesDialogMessageType type = WavesDialogMessageType.Information,
        WavesDialogButtons buttons = WavesDialogButtons.Ok)
    {
        return _navigationService.NavigateAsync<WavesMessageDialogViewModel, WavesMessageDialogParameter, WavesDialogResult>(
            new WavesMessageDialogParameter(text, title, sender?.GetType().GetFriendlyName(), null, type, buttons));
    }

    /// <inheritdoc />
    public Task<WavesDialogResult> ShowDialogAsync(
        string text,
        Exception exception,
        IWavesObject sender = null,
        WavesDialogMessageType type = WavesDialogMessageType.Error,
        WavesDialogButtons buttons = WavesDialogButtons.Ok)
    {
        return _navigationService.NavigateAsync<WavesMessageDialogViewModel, WavesMessageDialogParameter, WavesDialogResult>(
            new WavesMessageDialogParameter(text, "An exception occured", sender?.GetType().GetFriendlyName(), exception, type, buttons));
    }

    /// <param name="filter"></param>
    /// <inheritdoc />
    public Task<WavesOpenFileDialogResult> ShowOpenFileDialogAsync(IEnumerable<WavesFileDialogFilter> filter = null)
    {
        return _navigationService.ShowOpenFileDialogAsync(filter);
    }

    /// <summary>
    /// Callback when dialogs shown.
    /// </summary>
    protected virtual void OnDialogsShown()
    {
        DialogsShown?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Callback when dialogs hidden.
    /// </summary>
    protected virtual void OnDialogsHidden()
    {
        DialogsHidden?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Subscribes to events.
    /// </summary>
    private void SubscribeEvents()
    {
        _navigationService.DialogsShown += OnDialogShown;
        _navigationService.DialogsHidden += OnDialogHidden;
    }

    /// <summary>
    /// Callback when dialogs shown.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">Arguments.</param>
    private void OnDialogShown(object sender, EventArgs e)
    {
        DialogsShown?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Callback when dialog hidden.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">Arguments.</param>
    private void OnDialogHidden(object sender, EventArgs e)
    {
        DialogsHidden?.Invoke(this, EventArgs.Empty);
    }
}
