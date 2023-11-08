using Waves.Framework.UI.Presentation.Interfaces;

namespace Waves.Framework.UI.Services.Interfaces;

/// <summary>
///     Interface for navigation service.
/// </summary>
public interface IWavesNavigationService
{
    /// <summary>
    /// Goes back on current region.
    /// </summary>
    /// <param name="region">Name of region.</param>
    /// <returns>A<see cref="Task"/> representing the asynchronous operation.</returns>
    void GoBack(string region);

    /// <summary>
    /// Goes back.
    /// </summary>
    /// <param name="viewModel">Instance of <see cref="IWavesViewModel"/>.</param>
    /// <returns>A<see cref="Task"/> representing the asynchronous operation.</returns>
    void GoBack(IWavesViewModel viewModel);

    /// <summary>
    ///     Navigates to current view model.
    /// </summary>
    /// <param name="viewModel">View model.</param>
    /// <param name="addToHistory">Sets whether view model needed to be add to View history.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    void Navigate(IWavesViewModel viewModel, bool addToHistory = true);

    /// <summary>
    ///     Navigates to current view model which returns Result.
    /// </summary>
    /// <typeparam name="TParameter">Type of parameter.</typeparam>
    /// <param name="viewModel">View model.</param>
    /// <param name="parameter">Parameter.</param>
    /// <param name="addToHistory">Sets whether view model needed to be add to View history.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    void Navigate<TParameter>(
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
    TResult Navigate<TResult>(
        IWavesViewModel<TResult> viewModel,
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
    TResult Navigate<TParameter, TResult>(
        IWavesParameterizedViewModel<TParameter, TResult> viewModel,
        TParameter parameter,
        bool addToHistory = true);

    /// <summary>
    ///     Navigates to view model with current type.
    /// </summary>
    /// <typeparam name="T">Type of view model.</typeparam>
    /// <param name="addToHistory">Sets whether view model needed to be add to View history.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    void Navigate<T>(bool addToHistory = true)
        where T : class;

    /// <summary>
    ///     Navigates to view model with current type.
    /// </summary>
    /// <typeparam name="T">Type of view model.</typeparam>
    /// <typeparam name="TParameter">Type of parameter.</typeparam>
    /// <param name="parameter">Parameter.</param>
    /// <param name="addToHistory">Sets whether view model needed to be add to View history.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    void Navigate<T, TParameter>(
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
    TResult Navigate<T, TResult>(bool addToHistory = true)
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
    TResult Navigate<T, TParameter, TResult>(
        TParameter parameter,
        bool addToHistory = true)
        where T : class;

    /// <summary>
    ///     Navigates to view model with current type.
    /// </summary>
    /// <param name="type">Type of view model.</param>
    /// <param name="addToHistory">Sets whether view model needed to be add to View history.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    void Navigate(Type type, bool addToHistory = true);

    /// <summary>
    ///     Navigates to view model with current type.
    /// </summary>
    /// <typeparam name="TParameter">Type of parameter.</typeparam>
    /// <param name="type">Type of view model.</param>
    /// <param name="parameter">Parameter.</param>
    /// <param name="addToHistory">Sets whether view model needed to be add to View history.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    void Navigate<TParameter>(
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
    TResult Navigate<TResult>(Type type, bool addToHistory = true);

    /// <summary>
    ///     Navigates to view model with Parameter with current type which returns result Result.
    /// </summary>
    /// <param name="type">Type of view model.</param>
    /// <param name="parameter">Parameter.</param>
    /// <param name="addToHistory">Sets whether view model needed to be add to View history.</param>
    /// <typeparam name="TParameter">Type of parameter.</typeparam>
    /// <typeparam name="TResult">Type of result..</typeparam>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    TResult Navigate<TParameter, TResult>(
        Type type,
        TParameter parameter,
        bool addToHistory = true);
}
