namespace WpfColors.Common
{
    using System;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class DefaultAttribute : Attribute
    {
        public DefaultAttribute(object value)
        {
            Value = value;
        }

        public object Value { get; set; }
    }
}
