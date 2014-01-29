namespace DialogForMemoryTest
{
    using System.Windows;

    public partial class DialogWindow
    {
        public DialogWindow()
        {
            InitializeComponent();
            DataContext = new DialogDataContext();
        }

        public string DummyData { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
