using ReactiveUI;

namespace Waves.Framework.Core._old.Base.Interfaces;

/// <summary>
/// Interface for observable object to implement INotifyPropertyChanged.
/// </summary>
public interface IWavesObservableObject :
    IWavesObject,
    IReactiveObject
{
}
