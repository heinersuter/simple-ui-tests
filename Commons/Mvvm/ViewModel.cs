namespace Commons.Mvvm
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;

    public abstract class ViewModel : INotifyPropertyChanged
    {
        protected ViewModel()
        {
            PropertyChanged += OnPropertyChanged;
        }

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }

        private BackingFields _backingFields;

        public BackingFields BackingFields
        {
            get { return PropertyHelper.CreateIfNeeded(ref _backingFields, () => new BackingFields(RaisePropertyChanged)); }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpresssion)
        {
            var propertyName = GetPropertyName(propertyExpresssion);
            this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected string GetPropertyName<T>(Expression<Func<T>> propertyExpresssion)
        {
            return PropertyHelper.GetName(propertyExpresssion);
        }

        public T CreateIfNeeded<T>(Expression<Func<T>> propertyExpresssion, Func<T> newInstanceCreateMethod)
        {
            if (Equals(BackingFields.GetValue(propertyExpresssion), default(T)))
            {
                BackingFields.SetValue(propertyExpresssion, newInstanceCreateMethod.Invoke());
            }
            return BackingFields.GetValue(propertyExpresssion);
        }
    }
}
