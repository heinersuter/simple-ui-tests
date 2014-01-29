namespace Commons.Dialog
{
    using System.ComponentModel;
    using System.Windows;

    public partial class DialogView
    {
        public DialogView()
        {
            InitializeComponent();

            DataContextChanged += OnDataContectChanged;
        }

        private void OnDataContectChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var viewModel = e.NewValue as DialogViewModel;
            if (viewModel != null)
            {
                viewModel.CloseDelegate = Close;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            var viewModel = DataContext as DialogViewModel;
            if (viewModel != null)
            {
                viewModel.CloseDelegate = null;
                viewModel.OnDialogClosing();
            }
        }
    }
}
