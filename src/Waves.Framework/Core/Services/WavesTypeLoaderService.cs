using System.Reflection;
using Microsoft.Extensions.Logging;
using Waves.Framework.Core.Extensions;
using Waves.Framework.Core.Interfaces;

namespace Waves.Framework.Core.Services
{
    /// <summary>
    /// Service for load types from assemblies.
    /// </summary>
    /// <typeparam name="T">Attribute type.</typeparam>
    internal sealed class WavesTypeLoaderService<T> : IWavesTypeLoaderService<T>
    {
        private readonly Dictionary<Type, T> _types = new();
        private readonly ILogger<WavesTypeLoaderService<T>> _logger;
        private readonly string? _basePluginsDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        /// <summary>
        /// Creates new instance of <see cref="WavesTypeLoaderService{T}"/>.
        /// </summary>
        /// <param name="logger">Logger.</param>
        public WavesTypeLoaderService(ILogger<WavesTypeLoaderService<T>> logger)
        {
            _logger = logger;
        }

        public async Task<Dictionary<Type, T>> GetTypesAsync()
        {
            _types.Clear();

            var assemblies = new List<Assembly>();
            await assemblies.GetAssembliesAsync(_basePluginsDirectory, out var exceptions);

            foreach (var e in exceptions)
            {
                _logger.LogWarning(e, "Error occured while loading assembly: {E}", e);
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

                            _types.Add(type, typeAttribute);
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

            return _types;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return "Type Loader Service";
        }
    }
}
