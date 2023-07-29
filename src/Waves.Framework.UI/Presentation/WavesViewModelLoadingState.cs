using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Waves.Framework.UI.Presentation.Interfaces.ViewModel;

namespace Waves.Framework.UI.Presentation;

/// <summary>
/// Waves view model loading state.
/// </summary>
public class WavesViewModelLoadingState : ReactiveObject, IWavesViewModelLoadingState
{
    /// <inheritdoc />
    [Reactive]
    public bool IsLoading { get; set; }

    /// <inheritdoc />
    [Reactive]
    public bool IsIndeterminate { get; set; }

    /// <inheritdoc />
    [Reactive]
    public int ProgressValue { get; set; }
}
