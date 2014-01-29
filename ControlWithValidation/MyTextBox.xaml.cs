namespace ControlWithValidation
{
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;

    public partial class MyTextBox : IDataErrorInfo
    {
        public MyTextBox()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(MyTextBox), new PropertyMetadata(default(string)));

        public string Text
        {
            get { return (string)this.GetValue(TextProperty); }
            set { this.SetValue(TextProperty, value); }
        }

        public string this[string propertyName]
        {
            get
            {
                if (propertyName == "Text")
                {
                    return this.ValidateText();
                }
                return null;
            }
        }

        public string Error
        {
            // Is not required by the binding validation but by the interface
            get { return null; }
        }

        private string ValidateText()
        {
            string result = null;
            if (this.Text != null && this.Text.Contains('x'))
            {
                result = "The value must not contain an 'x'!";
            }
            return result;
        }
    }
}
