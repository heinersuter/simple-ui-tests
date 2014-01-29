namespace DialogForMemoryTest
{
    using System.Windows;

    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new DialogWindow();
            dialog.ShowDialog();
        }
    }
}
