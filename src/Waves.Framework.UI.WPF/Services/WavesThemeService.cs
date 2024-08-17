using System.Windows;
using Waves.Framework.Attributes;
using Waves.Framework.Interfaces;
using Waves.Framework.Services.Interfaces;
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
                var currentDictionary = (WavesResourceDictionary)resource;
                if (currentDictionary == null)
                {
                    continue;
                }

                var oldKey = (string)currentDictionary["Key"] ?? throw new InvalidOperationException();
                var newKey = (string)newResourceDictionary["Key"] ?? throw new InvalidOperationException();

                if (oldKey == newKey)
                {
                    oldResourceDictionary = currentDictionary;
                    break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        if (oldResourceDictionary == null)
        {
            return;
        }
        
        app.Resources.MergedDictionaries.Remove(oldResourceDictionary);
        app.Resources.MergedDictionaries.Add(newResourceDictionary);
    }
}