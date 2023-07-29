using ReactiveUI;

namespace Waves.Framework.Core.Base.Interfaces;

/// <summary>
/// Interface for observable object to implement INotifyPropertyChanged.
/// </summary>
public interface IWavesObservableObject :
    IWavesObject,
    IReactiveObject
{
}
