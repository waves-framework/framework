using Waves.Framework.Core._old.Base;
using Waves.Framework.Core._old.Base.Interfaces;

namespace Waves.Framework.Core._old.Extensions;

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
