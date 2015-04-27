namespace AsyncAwait
{
    using Alsolos.Commons.Controls.Progress;
    using Alsolos.Commons.Mvvm;

    public class MainWindowViewModel : BusyViewModel
    {
        private readonly Service _service;

        public MainWindowViewModel()
        {
            _service = new Service();
        }

        public DelegateCommand LoadCommand
        {
            get { return BackingFields.GetCommand(Load, CanLoad); }
        }

        public string Text
        {
            get { return BackingFields.GetValue<string>(); }
            set { BackingFields.SetValue(value); }
        }

        private bool CanLoad()
        {
            return true;
        }

        private async void Load()
        {
            using (BusyHelper.Enter("Loading first text..."))
            {
                Text += await _service.LoadTextAsync();

                using (BusyHelper.Enter("Loading second text..."))
                {
                    Text += await _service.LoadTextAsync();

                    using (BusyHelper.Enter("Loading third text..."))
                    {
                        Text += await _service.LoadTextAsync();
                    }
                }
                using (BusyHelper.Enter("Loading fourth text..."))
                {
                    Text += await _service.LoadTextAsync();
                }
            }
        }
    }
}
