namespace BusyIndicatorControl
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class BooleanToVisibilityConverter : IValueConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var flag = CalculateFlag(value, parameter);

            return flag ? Visibility.Visible : Visibility.Collapsed;
        }

        protected static bool CalculateFlag(object value, object parameter)
        {
            var flag = false;

            if (value is bool)
            {
                flag = (bool)value;
            }

            if (parameter != null)
            {
                if (bool.Parse((string)parameter))
                {
                    flag = !flag;
                }
            }
            return flag;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}