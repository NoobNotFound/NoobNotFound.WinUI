using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;

namespace NoobSharp.Common.WinUI.Converters
{
    public class StringToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // Reversed result
            if (parameter is string param)
            {
                if (param == "0")
                {
                    return (value is string val && val.Length > 0) ? Visibility.Collapsed : Visibility.Visible;
                }
            }

            return (value is string str && str.Length > 0) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new InvalidOperationException("Use the boolean to visibility converter in this situation. " +
                "A string is very likely unnecessary in this case.");
        }
    }
    public class BoolToVisibility : IValueConverter
    {
        public bool IsInverted { get; set; } = false;
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (value is bool b && b && !IsInverted ) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (value is bool b && b && !IsInverted) ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}