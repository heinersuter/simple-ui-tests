namespace KeyValueSelection
{
    public static class ReflectionHelper
    {
        public static T GetProperty<T>(object item, string propertyPath)
        {
            return GetProperty(item, propertyPath, default(T));
        }

        public static T GetProperty<T>(object item, string propertyPath, T defaultValue)
        {
            if (string.IsNullOrWhiteSpace(propertyPath))
            {
                return defaultValue;
            }
            var info = item.GetType().GetProperty(propertyPath);
            if (info == null)
            {
                return defaultValue;
            }
            var value = info.GetValue(item, null);
            if (value is T)
            {
                return (T)value;
            }
            if (typeof(T) == typeof(string))
            {
                object obj = value.ToString();
                return (T)obj;
            }
            return defaultValue;
        }
    }
}
