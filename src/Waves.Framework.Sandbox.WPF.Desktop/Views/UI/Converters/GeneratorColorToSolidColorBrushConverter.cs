using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Waves.Sandbox.ViewModels.UI.Color;

namespace Waves.Framework.Sandbox.WPF.Desktop.Views.UI.Converters;

public class GeneratorColorToSolidColorBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not GeneratorColor color)
        {
            throw new Exception("Wrong input");
        }

        return new SolidColorBrush(Color.FromArgb(255, color.R, color.G, color.B));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Binding.DoNothing;
    }
}