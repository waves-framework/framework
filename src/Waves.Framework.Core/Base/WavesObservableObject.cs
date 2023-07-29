using ReactiveUI;
using Waves.Framework.Core.Base.Interfaces;

namespace Waves.Framework.Core.Base;

/// <summary>
/// Waves observable object that implement INotifyPropertyChanged.
/// </summary>
public class WavesObservableObject :
    ReactiveObject,
    IWavesObservableObject
{
}
