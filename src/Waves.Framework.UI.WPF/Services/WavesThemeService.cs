using System.Windows;
using Waves.Framework.Attributes;
using Waves.Framework.UI.Interfaces;
using Waves.Framework.UI.Services.Interfaces;
using Waves.Framework.UI.WPF.Styles;

namespace Waves.Framework.UI.WPF.Services;

[WavesPlugin(typeof(IWavesThemeService))]
public class WavesThemeService : IWavesThemeService
{
    private readonly IWavesUiApplication _application;

    public WavesThemeService(IWavesUiApplication application)
    {
        _application = application;
    }
    
    public void SwitchTheme()
    {
        throw new NotImplementedException();
    }

    public void SetDictionary(IWavesResourceDictionary dictionary)
    {
        if (_application is not Application app)
        {
            throw new Exception("Application has not been initialized");
        }

        if (dictionary is not ResourceDictionary newResourceDictionary)
        {
            throw new Exception("Wrong resource dictionary");
        }

        WavesResourceDictionary? oldResourceDictionary = null;
        foreach (var resource in app.Resources.MergedDictionaries)
        {
            try
            {
                var wavesResourceDictionary = (WavesResourceDictionary)resource;
                if (wavesResourceDictionary == null)
                {
                    continue;
                }

                var oldKey = (string)wavesResourceDictionary["Key"];
                var newKey = (string)newResourceDictionary["Key"];

                if (oldKey == newKey)
                {
                    oldResourceDictionary = wavesResourceDictionary;
                    break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        if (oldResourceDictionary != null)
        {
            app.Resources.MergedDictionaries.Remove((ResourceDictionary)oldResourceDictionary);
            app.Resources.MergedDictionaries.Add(newResourceDictionary);
        }
    }
}