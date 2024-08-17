using Waves.Framework.Presentation;

namespace Waves.Sandbox.ViewModels.Navigation.Window;

public class WindowNavigationViewModelBase : WavesViewModel
{
}

public class WindowNavigationWithResultViewModelBase : WavesViewModel<string>
{
}

public class WindowNavigationWithParameterViewModelBase : WavesParameterizedViewModel<string>
{
    public string Parameter { get; private set; }
    
    public override Task Prepare(string t)
    {
        Parameter = t;
        return Task.CompletedTask;
    }
}

public class WindowNavigationWithParameterWithResultViewModelBase : WavesParameterizedViewModel<string, string>
{
    public string Parameter { get; private set; }
    
    public override Task Prepare(string t)
    {
        Parameter = t;
        return Task.CompletedTask;
    }
}