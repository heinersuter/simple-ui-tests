namespace WpfLocalizeExtensionTest
{
    using System.Globalization;
    using System.Windows;

    using WPFLocalizeExtension.Engine;

    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            OnButtonOsClick(this, null);
        }

        private void OnButtonOsClick(object sender, RoutedEventArgs e)
        {
            LocalizeDictionary.Instance.Culture = CultureInfo.CurrentCulture;
        }

        private void OnButtonDeClick(object sender, RoutedEventArgs e)
        {
            LocalizeDictionary.Instance.Culture = CultureInfo.GetCultureInfo("de");
        }

        private void OnButtonEnClick(object sender, RoutedEventArgs e)
        {
            LocalizeDictionary.Instance.Culture = CultureInfo.GetCultureInfo("en");
        }
    }
}
