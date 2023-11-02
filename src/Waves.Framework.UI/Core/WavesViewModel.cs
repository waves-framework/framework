using Microsoft.Extensions.Logging;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Waves.Framework.UI.Core.Interfaces;

namespace Waves.Framework.UI.Core
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
        public TResult? Result { get; protected set; }
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
