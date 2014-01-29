namespace SuppressPreviewEvent
{
    using System;
    using System.Windows.Input;

    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            Console.WriteLine(@"Window KeyDown '{0}'", e.Key);

            base.OnKeyDown(e);
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            Console.WriteLine(@"Window Preview KeyDown '{0}'", e.Key);

            base.OnPreviewKeyDown(e);
        }

        protected override void OnAccessKey(AccessKeyEventArgs e)
        {
            Console.WriteLine(@"Window AccessKey '{0}'", e.Key);

            base.OnAccessKey(e);
        }

        private void OnButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Console.WriteLine(@"Window Button Clicked");
        }
    }
}
