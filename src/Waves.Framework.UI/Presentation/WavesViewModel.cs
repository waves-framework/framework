using ReactiveUI;
using Waves.Framework.UI.Presentation.Interfaces;

namespace Waves.Framework.UI.Presentation
{
    /// <summary>
    /// Base view model.
    /// </summary>
#pragma warning disable SA1402 // File may only contain a single type
    public abstract class WavesViewModel :
        ReactiveObject,
        IWavesViewModel
    {
        /// <inheritdoc />
        public virtual Task ViewAppeared()
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual Task ViewDisappeared()
        {
            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// Base view model.
    /// </summary>
    /// <typeparam name="TResult">Type of result.</typeparam>
#pragma warning disable SA1402 // File may only contain a single type
    public abstract class WavesViewModel<TResult> :
        WavesViewModel,
        IWavesViewModel<TResult>
    {
        /// <inheritdoc />
        public event EventHandler ResultApproved;
        
        /// <inheritdoc />
        public TResult Result { get; set; }

        protected virtual void OnResultApproved()
        {
            ResultApproved?.Invoke(this, System.EventArgs.Empty);
        }
    }

    /// <summary>
    /// Base view model.
    /// </summary>
    /// <typeparam name="TParameter">Type of parameter.</typeparam>
#pragma warning disable SA1402 // File may only contain a single type
    public abstract class WavesParameterizedViewModel<TParameter> : WavesViewModel, IWavesParameterizedViewModel<TParameter>
    {
        /// <inheritdoc />
        public abstract Task Prepare(TParameter t);
    }

    /// <summary>
    /// Base view model.
    /// </summary>
    /// <typeparam name="TParameter">Type of parameter.</typeparam>
    /// <typeparam name="TResult">Type of result.</typeparam>
#pragma warning disable SA1402 // File may only contain a single type
    public abstract class WavesParameterizedViewModel<TParameter, TResult> : WavesViewModel<TResult>, IWavesParameterizedViewModel<TParameter, TResult>
    {
        /// <inheritdoc />
        public abstract Task Prepare(TParameter t);
    }
}
