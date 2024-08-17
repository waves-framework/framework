using System.Windows;
using Waves.Framework.Interfaces;

namespace Waves.Framework.UI.WPF.Styles;

public class WavesResourceDictionary : ResourceDictionary, IWavesResourceDictionary
{
    public string Id { get; set; }
}