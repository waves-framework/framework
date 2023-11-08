namespace Waves.Framework.Interfaces;

internal interface IWavesTypeLoaderService<T>
{
    /// <summary>
    /// Updates types async.
    /// </summary>
    /// <returns>Task.</returns>
    Task<Dictionary<Type, T>> GetTypesAsync();
}