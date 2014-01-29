namespace StringFormat
{
    using System.Windows;

    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public static readonly DependencyProperty DoubleValueProperty = DependencyProperty.Register(
            "DoubleValue", typeof(double), typeof(MainWindow), new PropertyMetadata(123.456));

        public double DoubleValue
        {
            get { return (double)GetValue(DoubleValueProperty); }
            set { SetValue(DoubleValueProperty, value); }
        }
    }
}
