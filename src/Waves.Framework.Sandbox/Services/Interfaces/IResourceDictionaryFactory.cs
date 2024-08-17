using Waves.Framework.Interfaces;

namespace Waves.Sandbox.Services.Interfaces;

public interface IResourceDictionaryFactory
{
    IWavesResourceDictionary CreateFromString(string str);
}