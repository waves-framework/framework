using Waves.Framework.UI.Interfaces;

namespace Waves.Sandbox.Services.Interfaces;

public interface IResourceDictionaryFactory
{
    IWavesResourceDictionary CreateFromString(string str);
}