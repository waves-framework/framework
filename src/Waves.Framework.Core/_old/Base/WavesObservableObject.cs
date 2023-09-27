using ReactiveUI;
using Waves.Framework.Core._old.Base.Interfaces;

namespace Waves.Framework.Core._old.Base;

/// <summary>
/// Waves observable object that implement INotifyPropertyChanged.
/// </summary>
public class WavesObservableObject :
    ReactiveObject,
    IWavesObservableObject
{
}
