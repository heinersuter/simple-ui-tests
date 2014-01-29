namespace FormattedNumberInput
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class NullableNumericToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }

            if (parameter is string && !string.IsNullOrWhiteSpace((string)parameter))
            {
                return string.Format((string)parameter, value);
            }

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var s = value as string;

            if (s == null)
            {
                throw new ArgumentException(@"Parameter 'value' must be of type 'string'.", "value");
            }

            if (string.IsNullOrWhiteSpace(s))
            {
                return null;
            }

            if (targetType == typeof(int?))
            {
                int i;
                if (int.TryParse(s, out i))
                {
                    return i;
                }

                return null;
            }

            if (targetType == typeof(long?))
            {
                long i;
                if (long.TryParse(s, out i))
                {
                    return i;
                }

                return null;
            }

            if (targetType == typeof(double?))
            {
                double d;
                if (double.TryParse(s, out d))
                {
                    return d;
                }

                return null;
            }

            throw new NotImplementedException("This converter does not support the specified TargetType.");
        }
    }
}
