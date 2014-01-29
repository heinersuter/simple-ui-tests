namespace WpfColors.Common
{
    using System;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class DependsUponAttribute : Attribute
    {
        public DependsUponAttribute(string propertyName)
        {
            this.PropertyName = propertyName;
        }

        public string PropertyName { get; set; }
    }
}
