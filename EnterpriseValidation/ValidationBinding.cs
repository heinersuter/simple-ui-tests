namespace EnterpriseValidation
{
    using System.Windows;

    public class ValidationBinding : DependencyObject
    {
        public static readonly DependencyProperty PropertyProperty = DependencyProperty.RegisterAttached(
            "Property", 
            typeof(object), 
            typeof(ValidationBinding), 
            new FrameworkPropertyMetadata(null));

        public static void SetProperty(UIElement element, object value)
        {
            element.SetValue(PropertyProperty, value);
        }

        public static object GetProperty(UIElement element)
        {
            return element.GetValue(PropertyProperty);
        }
    }
}
