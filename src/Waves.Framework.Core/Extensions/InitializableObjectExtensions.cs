using Waves.Framework.Core.Base;
using Waves.Framework.Core.Base.Interfaces;

namespace Waves.Framework.Core.Extensions;

/// <summary>
/// Extensions for <see cref="WavesInitializableObject"/>.
/// </summary>
internal static class InitializableObjectExtensions
{
    /// <summary>
    /// Checks that object is <see cref="WavesInitializableObject"/> and initialize it if it is.
    /// </summary>
    /// <param name="obj">Object.</param>
    internal static async void CheckInitializable(this object obj)
    {
        if (obj is IWavesInitializableObject initializableObject)
        {
            await initializableObject.InitializeAsync();
        }
    }
}
