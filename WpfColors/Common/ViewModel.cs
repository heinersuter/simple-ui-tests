namespace WpfColors.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public class ViewModel : INotifyPropertyChanged
    {
        private readonly Dictionary<string, object> _properties = new Dictionary<string, object>();

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected T GetValue<T>(Expression<Func<T>> propertyExpression)
        {
            object value;
            if (this._properties.TryGetValue(this.GetPropertyName(propertyExpression), out value))
            {
                return (T)value;
            }
            return this.GetDefaultValueByAttribute(propertyExpression);
        }

        protected void SetValue<T>(Expression<Func<T>> propertyExpression, object value, Action callback = null)
        {
            var previousValue = this.GetValue(propertyExpression);

            if (!Equals(previousValue, value))
            {
                this._properties[this.GetPropertyName(propertyExpression)] = value;
                this.RaisePropertyChanged(propertyExpression);
            }
            if (callback != null)
            {
                callback.Invoke();
            }
        }

        protected bool ChangeAndNotify<T>(ref T field, T newValue, Expression<Func<T>> propertyExpression)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyExpression.Name));
                return true;
            }
            return false;
        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            RaisePropertyChanged(this.GetPropertyName(propertyExpression));
        }

        protected string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            return PropertyHelper.GetName(propertyExpression);
        }

        protected T CreateIfNeeded<T>(Expression<Func<T>> propertyExpression, Func<T> newInstanceCreateMethod)
        {
            if (Equals(this.GetValue(propertyExpression), default(T)))
            {
                this.SetValue(propertyExpression, newInstanceCreateMethod.Invoke());
            }
            return this.GetValue(propertyExpression);
        }

        private void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));

            this.RaisePropertyChangedOnDependantProperties(propertyName);
        }
        
        private T GetDefaultValueByAttribute<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyInfo = this.GetType().GetProperty(this.GetPropertyName(propertyExpression));
            var defaultAttribute = propertyInfo.GetCustomAttributes(typeof(DefaultAttribute), true).FirstOrDefault() as DefaultAttribute;
            if (defaultAttribute != null && defaultAttribute.Value is T)
            {
                return (T)defaultAttribute.Value;
            }
            return default(T);
        }

        private void RaisePropertyChangedOnDependantProperties(string propertyName)
        {
            var dependantProperties = new List<PropertyInfo>();
            foreach (var propertyInfo in this.GetType().GetProperties())
            {
                var attribute = propertyInfo.GetCustomAttributes(typeof(DependsUponAttribute), true).FirstOrDefault() as DependsUponAttribute;
                if (attribute != null && attribute.PropertyName == propertyName)
                {
                    dependantProperties.Add(propertyInfo);
                }
            }
            foreach (var dependantProperty in dependantProperties)
            {
                var property = dependantProperty;
                this.RaisePropertyChanged(property.Name);
            }
        }
    }
}
