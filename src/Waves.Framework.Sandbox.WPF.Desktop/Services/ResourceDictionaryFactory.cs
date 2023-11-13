using System;
using System.IO;
using System.Text;
using System.Windows.Markup;
using Waves.Framework.Attributes;
using Waves.Framework.UI.Interfaces;
using Waves.Framework.UI.WPF.Styles;
using Waves.Sandbox.Services.Interfaces;

namespace Waves.Framework.Sandbox.WPF.Desktop.Services;

[WavesPlugin(typeof(IResourceDictionaryFactory))]
public class ResourceDictionaryFactory : IResourceDictionaryFactory
{
    public IWavesResourceDictionary CreateFromString(string str)
    {
        try
        {
            var reader = new XamlReader();
            var bytes = Encoding.UTF8.GetBytes(str);
            using var ms = new MemoryStream(bytes);
            return (WavesResourceDictionary)reader.LoadAsync(ms);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
}