using System.Collections.ObjectModel;
using System.Windows.Input;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Waves.Framework.UI.Attributes;
using Waves.Framework.UI.Services.Interfaces;
using Waves.Sandbox.Services;
using Waves.Sandbox.Services.Interfaces;
using Waves.Sandbox.ViewModels.Base;
using Waves.Sandbox.ViewModels.UI.Color;

namespace Waves.Sandbox.ViewModels.UI;

[WavesViewModel(typeof(UiGeneratorPageViewModel))]
public class UiGeneratorPageViewModel : PageViewModelBase
{
    private readonly ColorService _colorService;
    private readonly IResourceDictionaryFactory _resourceDictionaryFactory;
    private readonly IWavesThemeService _themeService;

    private const string PrimaryLightKey = "primary_light_"; 
    private const string PrimaryDarkKey = "primary_dark_"; 
    private const string Accent1Key = "accent_1_";
    private const string Accent2Key = "accent_2_";
    private const string Accent3Key = "accent_3_";

    public UiGeneratorPageViewModel(
        IWavesNavigationService navigationService,
        ColorService colorService,
        IResourceDictionaryFactory resourceDictionaryFactory,
        IWavesThemeService themeService) : base(navigationService)
    {
        _colorService = colorService;
        _resourceDictionaryFactory = resourceDictionaryFactory;
        _themeService = themeService;
        GenerateCommand = ReactiveCommand.CreateFromTask(OnGenerate);
        Colors = new ObservableCollection<GeneratorColorTintsList>();
    }
    
    [Reactive]
    public ObservableCollection<GeneratorColorTintsList> Colors { get; set; }

    public ICommand GenerateCommand { get; protected set; }
    
    public static List<string> AvailableColorNames { get; private set; } = new()
    {
        "PrimaryLight",
        "PrimaryDark",
        "Accent1",
        "Accent2",
        "Accent3",
    };

    public static Dictionary<string, string> ColorMapping { get; set; } = new()
    {
        { "PrimaryLight", PrimaryLightKey },
        { "PrimaryDark", PrimaryDarkKey },
        { "Accent1", Accent1Key },
        { "Accent2", Accent2Key },
        { "Accent3", Accent3Key },
    };
    
    private async Task OnGenerate()
    {
        var result = await _colorService.GenerateAsync();
        
        var list = new List<GeneratorColorTintsList>();

        for (var i = 0; i < result.Result.Count; i++)
        {
            var color = result.Result[i];
            var tints = await _colorService.GenerateTints(color);
            tints.Name = AvailableColorNames[i];
            list.Add(tints);
        }

        if (Colors.Count != 0 && Colors.Count == 5)
        {
            for (var i = 0; i < Colors.Count; i++)
            {
                if (Colors[i].Lock)
                {
                    continue;
                }

                Colors[i].OrderChanged -= OnColorOrderChanged;
                Colors[i] = list[i];
                Colors[i].OrderChanged += OnColorOrderChanged;
            }
        }
        else
        {
            foreach (var color in list)
            {
                color.OrderChanged += OnColorOrderChanged;
            }
            
            Colors = new ObservableCollection<GeneratorColorTintsList>(list);
        }
        
        UpdateColors(Colors);
    }

    private void UpdateColors(IEnumerable<GeneratorColorTintsList> colors)
    {
        var currentDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
        var templatePath = Path.Join(currentDirectory + "/Styles/Templates/Colors.template");
        var template = File.ReadAllText(templatePath);

        var colorList = colors as GeneratorColorTintsList[] ?? colors.ToArray();
        foreach (var mapping in ColorMapping)
        {
            var currentColor = colorList.FirstOrDefault(x => x.Name.Equals(mapping.Key));
            if (currentColor != null)
            {
                foreach (var tint in currentColor.Tints)
                {
                    var str = $"{{{{{mapping.Value}{tint.Tint}}}}}";
                    template = template.Replace(str, tint.ToHex());
                }
            } }

        var resourceDictionary = _resourceDictionaryFactory.CreateFromString(template);
        _themeService.SetDictionary(resourceDictionary);
    }

    private void OnColorOrderChanged(object? sender, string e)
    {
        var oldElement = Colors.FirstOrDefault(x => x.Name.Equals(e) && x != sender);
        if (oldElement != null && sender is GeneratorColorTintsList newElement)
        {
            var indexOfOld = Colors.IndexOf(oldElement);
            var indexOfNew = Colors.IndexOf(newElement);
            (Colors[indexOfOld], Colors[indexOfNew]) = (Colors[indexOfNew], Colors[indexOfOld]);
            Colors[indexOfOld].Name = AvailableColorNames[indexOfOld];
            Colors[indexOfNew].Name = AvailableColorNames[indexOfNew];
            UpdateColors(Colors);
        }
    }
}