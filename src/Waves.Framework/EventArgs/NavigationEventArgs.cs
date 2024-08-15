namespace Waves.Framework.EventArgs;

/// <summary>
/// Event arguments for can go back navigation.
/// </summary>
public class NavigationEventArgs : System.EventArgs
{
    /// <summary>
    /// Creates new instance of <see cref="NavigationEventArgs"/>.
    /// </summary>
    /// <param name="canGoBack">Set whether we can navigate back.</param>
    /// <param name="contentControl">Sets content control.</param>
    public NavigationEventArgs(
        bool canGoBack,
        object contentControl)
    {
        CanGoBack = canGoBack;
        ContentControl = contentControl;
    }

    /// <summary>
    /// Gets whether navigation can go back.
    /// </summary>
    public bool CanGoBack { get; }

    /// <summary>
    /// Gets content control which can navigate back.
    /// </summary>
    public object ContentControl { get; }
}
