namespace SuppressPreviewEvent
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    public partial class UserControl1
    {
        public UserControl1()
        {
            InitializeComponent();

            _textBox.PreviewKeyDown += OnTextBoxPreviewKeyDown;
            _textBox.KeyDown += OnTextBoxKeyDown;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            Console.WriteLine(@"UserControl Preview KeyDown '{0}'", e.Key);
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                var newArgs = new KeyEventArgs(e.KeyboardDevice, e.InputSource, e.Timestamp, e.Key)
                {
                    RoutedEvent = UIElement.KeyDownEvent,
                };
                this.RaiseEvent(newArgs);
            }
            base.OnPreviewKeyDown(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            Console.WriteLine(@"UserControl KeyDown '{0}'", e.Key);

            base.OnKeyDown(e);
        }

        private void OnTextBoxPreviewKeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine(@"TextBox Preview KeyDown '{0}'", e.Key);
        }

        private void OnTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine(@"TextBox KeyDown '{0}'", e.Key);
        }
    }
}
