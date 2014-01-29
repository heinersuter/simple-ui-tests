namespace Alsolos.RegexTester.View
{
    using Commons.Mvvm;

    public class MainWindowViewModel : ViewModel
    {
        public MainViewModel MainViewModel
        {
            get { return BackingFields.GetValue(() => MainViewModel, () => new MainViewModel()); }
            set { BackingFields.SetValue(() => MainViewModel, value); }
        }
    }
}
