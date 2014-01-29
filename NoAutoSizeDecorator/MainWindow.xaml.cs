namespace NoAutoSizeDecorator
{
    using System.Windows;

    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public static readonly DependencyProperty ButtonTextProperty = DependencyProperty.Register(
            "ButtonText", typeof(string), typeof(MainWindow), new PropertyMetadata("AAAA"));

        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            ButtonText = "AAAA " + ButtonText + "\nAAAA";
        }
    }
}
