using Waves.Framework.UI.Core.Presentation.Interfaces;

namespace Waves.Framework.UI.Core.Services.Interfaces;

public interface IWavesWindowService
{
    IReadOnlyCollection<IWavesView> OpenedWindows { get; set; }
    
    void AddWindow(bool isMain = false);
}