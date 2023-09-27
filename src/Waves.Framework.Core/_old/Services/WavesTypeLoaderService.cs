using System.Reflection;
using Microsoft.Extensions.Logging;
using Waves.Framework.Core._old.Extensions;
using Waves.Framework.Core._old.Services.Interfaces;

namespace Waves.Framework.Core._old.Services
{
    /// <summary>
    /// Service for load types from assemblies.
    /// </summary>
    /// <typeparam name="T">Attribute type.</typeparam>
    public sealed class WavesTypeLoaderService<T> : IWavesTypeLoaderService<T>
    {
        private readonly ILogger<WavesTypeLoaderService<T>> _logger;

        private readonly string _basePluginsDirectory = System.IO.Path.GetDirectoryName(
            Assembly.GetExecutingAssembly().Location);

        /// <summary>
        /// Creates new instance of <see cref="WavesTypeLoaderService{T}"/>.
        /// </summary>
        /// <param name="logger">Logger.</param>
        public WavesTypeLoaderService(ILogger<WavesTypeLoaderService<T>> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets types.
        /// </summary>
        public Dictionary<Type, T> Types { get; private set; }

        /// <summary>
        /// Updates types.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task UpdateTypesAsync()
        {
            Types ??= new Dictionary<Type, T>();
            Types.Clear();

            var assemblies = new List<Assembly>();
            await assemblies.GetAssembliesAsync(_basePluginsDirectory, out var exceptions);

            if (exceptions != null)
            {
                foreach (var e in exceptions)
                {
                    _logger.LogWarning(e, "Error occured while loading assembly: {E}", e);
                }
            }

            foreach (var assembly in assemblies)
            {
                try
                {
                    var count = 0;
                    foreach (var type in assembly.GetExportedTypes())
                    {
                        var attributes = type.GetCustomAttributes();
                        foreach (var attribute in attributes)
                        {
                            if (attribute is not T typeAttribute)
                            {
                                continue;
                            }

                            Types.Add(type, typeAttribute);
                            count++;
                        }
                    }

                    if (count > 0)
                    {
                        _logger.LogDebug("Assembly {AssemblyFullName} loaded with {Count} types", assembly.FullName, count);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogWarning(e, "Error occured while loading assembly {AssemblyFullName}", assembly.FullName);
                }
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return "Type Loader Service";
        }
    }
}
