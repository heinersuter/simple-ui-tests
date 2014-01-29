namespace Commons
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class BoolToVisibilityConverter : IValueConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isVisible = CalculateIsVisibile(value, parameter);
            return isVisible ? Visibility.Visible : Visibility.Collapsed;
        }

        protected static bool CalculateIsVisibile(object value, object parameter)
        {
            var isVisible = false;

            if (value is bool)
            {
                isVisible = (bool)value;
            }

            if (parameter != null)
            {
                    isVisible = !isVisible;
            }

            return isVisible;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}