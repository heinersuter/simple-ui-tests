namespace AsyncAwait
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public partial class MainWindow : INotifyPropertyChanged
    {
        private readonly Service _service;
        private bool _isBusy;

        public MainWindow()
        {
            InitializeComponent();
            _service = new Service();
        }

        private async void Clicked(object sender, RoutedEventArgs args)
        {
            IsBusy = true;
            TextBox.Text += await _service.LoadTextAsync();
            TextBox.Text += await _service.LoadTextAsync();
            TextBox.Text += await _service.LoadTextAsync();
            TextBox.Text += await _service.LoadTextAsync();
            IsBusy = false;
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
