using Waves.Framework.Presentation.Interfaces;

namespace Waves.Framework.Services.Interfaces;

/// <summary>
///     Interface for navigation service.
/// </summary>
public interface IWavesNavigationService
{
    /// <summary>
    /// Returns can go back in current region or not.
    /// </summary>
    /// <param name="region">Region name.</param>
    /// <returns>Returns can go back or not.</returns>
    Task<bool> CanGoBack(string region);
    
    /// <summary>
    /// Goes back on current region.
    /// </summary>
    /// <param name="region">Name of region.</param>
    /// <returns>A<see cref="Task"/> representing the asynchronous operation.</returns>
    Task GoBackAsync(string region);

    /// <summary>
    /// Goes back.
    /// </summary>
    /// <param name="viewModel">Instance of <see cref="IWavesViewModel"/>.</param>
    /// <returns>A<see cref="Task"/> representing the asynchronous operation.</returns>
    Task GoBackAsync(IWavesViewModel viewModel);

    /// <summary>
    ///     Navigates to current view model.
    /// </summary>
    /// <param name="viewModel">View model.</param>
    /// <param name="addToHistory">Sets whether view model needed to be add to View history.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task NavigateAsync(IWavesViewModel viewModel, bool addToHistory = true);

    /// <summary>
    ///     Navigates to current view model which returns Result.
    /// </summary>
    /// <typeparam name="TParameter">Type of parameter.</typeparam>
    /// <param name="viewModel">View model.</param>
    /// <param name="parameter">Parameter.</param>
    /// <param name="addToHistory">Sets whether view model needed to be add to View history.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task NavigateAsync<TParameter>(
        IWavesParameterizedViewModel<TParameter> viewModel,
        TParameter parameter,
        bool addToHistory = true);

    /// <summary>
    ///     Navigates to current view model which returns Result.
    /// </summary>
    /// <typeparam name="TResult">Type of result.</typeparam>
    /// <param name="viewModel">View model.</param>
    /// <param name="addToHistory">Sets whether view model needed to be add to View history.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task<TResult?> NavigateAsync<TResult>(
        IWavesViewModel<TResult?> viewModel,
        bool addToHistory = true);

    /// <summary>
    ///     Navigates to current view model with Parameter with current type which returns result Result.
    /// </summary>
    /// <typeparam name="TParameter">Type of parameter.</typeparam>
    /// <typeparam name="TResult">Type of result.</typeparam>
    /// <param name="viewModel">View model.</param>
    /// <param name="parameter">Parameter.</param>
    /// <param name="addToHistory">Sets whether view model needed to be add to View history.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task<TResult?> NavigateAsync<TParameter, TResult>(
        IWavesParameterizedViewModel<TParameter, TResult> viewModel,
        TParameter parameter,
        bool addToHistory = true);

    /// <summary>
    ///     Navigates to view model with current type.
    /// </summary>
    /// <typeparam name="T">Type of view model.</typeparam>
    /// <param name="addToHistory">Sets whether view model needed to be add to View history.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task NavigateAsync<T>(bool addToHistory = true)
        where T : class;

    /// <summary>
    ///     Navigates to view model with current type.
    /// </summary>
    /// <typeparam name="T">Type of view model.</typeparam>
    /// <typeparam name="TParameter">Type of parameter.</typeparam>
    /// <param name="parameter">Parameter.</param>
    /// <param name="addToHistory">Sets whether view model needed to be add to View history.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task NavigateAsync<T, TParameter>(
        TParameter parameter,
        bool addToHistory = true)
        where T : class;

    /// <summary>
    ///     Navigates to view model with current type which returns result Result.
    /// </summary>
    /// <param name="addToHistory">Sets whether view model needed to be add to View history.</param>
    /// <typeparam name="T">Type of view model.</typeparam>
    /// <typeparam name="TResult">Type of result.</typeparam>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task<TResult?> NavigateAsync<T, TResult>(bool addToHistory = true)
        where T : class;

    /// <summary>
    ///     Navigates to view model with Parameter with current type which returns result Result.
    /// </summary>
    /// <param name="parameter">Parameter.</param>
    /// <param name="addToHistory">Sets whether view model needed to be add to View history.</param>
    /// <typeparam name="T">Type of view model.</typeparam>
    /// <typeparam name="TParameter">Type of parameter.</typeparam>
    /// <typeparam name="TResult">Type of result..</typeparam>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task<TResult?> NavigateAsync<T, TParameter, TResult>(
        TParameter parameter,
        bool addToHistory = true)
        where T : class;

    /// <summary>
    ///     Navigates to view model with current type.
    /// </summary>
    /// <param name="type">Type of view model.</param>
    /// <param name="addToHistory">Sets whether view model needed to be add to View history.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task NavigateAsync(Type type, bool addToHistory = true);

    /// <summary>
    ///     Navigates to view model with current type.
    /// </summary>
    /// <typeparam name="TParameter">Type of parameter.</typeparam>
    /// <param name="type">Type of view model.</param>
    /// <param name="parameter">Parameter.</param>
    /// <param name="addToHistory">Sets whether view model needed to be add to View history.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task NavigateAsync<TParameter>(
        Type type,
        TParameter parameter,
        bool addToHistory = true);

    /// <summary>
    ///     Navigates to view model with current type which returns result Result.
    /// </summary>
    /// <param name="type">Type of view model.</param>
    /// <param name="addToHistory">Sets whether view model needed to be add to View history.</param>
    /// <typeparam name="TResult">Type of result.</typeparam>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task<TResult?> NavigateAsync<TResult>(Type type, bool addToHistory = true);

    /// <summary>
    ///     Navigates to view model with Parameter with current type which returns result Result.
    /// </summary>
    /// <param name="type">Type of view model.</param>
    /// <param name="parameter">Parameter.</param>
    /// <param name="addToHistory">Sets whether view model needed to be add to View history.</param>
    /// <typeparam name="TParameter">Type of parameter.</typeparam>
    /// <typeparam name="TResult">Type of result..</typeparam>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task<TResult?> NavigateAsync<TParameter, TResult>(
        Type type,
        TParameter parameter,
        bool addToHistory = true);
    
    /// <summary>
    /// Registers content control.
    /// </summary>
    /// <param name="region">Region.</param>
    /// <param name="contentControl">Content control.</param>
    void RegisterContentControl(string region, object contentControl);

    /// <summary>
    /// Unregisters content control.
    /// </summary>
    /// <param name="region">Region.</param>
    void UnregisterContentControl(string region);
}
