namespace ControlWithValidation
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    public partial class MainWindow : ICommand
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            StringValue = "a1";
        }

        public static readonly DependencyProperty StringValueProperty = DependencyProperty.Register(
            "StringValue", typeof(string), typeof(MainWindow), new PropertyMetadata(default(string), OnStringValuePropertyChanged));

        public string StringValue
        {
            get
            {
                return (string)GetValue(StringValueProperty);
            }
            set
            {
                SetValue(StringValueProperty, value);
            }
        }

        private static void OnStringValuePropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
        }

        public void Execute(object parameter)
        {
            Console.WriteLine(StringValue);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
    }
}
