namespace Commons
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class DescriptionControl : ContentControl
    {
        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
            "Description", typeof(string), typeof(DescriptionControl), new PropertyMetadata(default(string)));

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DependencyProperty DescriptionMemberPathProperty = DependencyProperty.Register(
            "DescriptionMemberPath", typeof(string), typeof(DescriptionControl), new PropertyMetadata(default(string), OnDescriptionMemberPathPropertyChanged));

        public string DescriptionMemberPath
        {
            get { return (string)GetValue(DescriptionMemberPathProperty); }
            set { SetValue(DescriptionMemberPathProperty, value); }
        }

        private static void OnDescriptionMemberPathPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var control = dependencyObject as DescriptionControl;
            if (control != null)
            {
                control.SetBindingToDescription();
            }
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
            SetBindingToDescription();
        }

        private void SetBindingToDescription()
        {
            if (Content == null || string.IsNullOrWhiteSpace(DescriptionMemberPath))
            {
                return;
            }

            var binding = new Binding(DescriptionMemberPath) { Source = this.Content };
            this.SetBinding(DescriptionProperty, binding);
        }
    }
}
