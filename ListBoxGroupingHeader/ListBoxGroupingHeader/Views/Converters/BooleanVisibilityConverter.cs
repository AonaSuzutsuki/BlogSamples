using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace ListBoxGroupingHeader.Views.Converters
{
    public class BooleanVisibilityConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not bool isVisible)
                return Visibility.Collapsed;
            
            return isVisible ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not Visibility visibility)
                return false;

            if (visibility == Visibility.Collapsed || visibility == Visibility.Hidden)
                return false;

            return true;
        }
    }
}
