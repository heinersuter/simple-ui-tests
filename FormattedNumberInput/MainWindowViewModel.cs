namespace FormattedNumberInput
{
    using Commons;
    using Commons.Mvvm;

    public class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
            DoubleValue = 20000000;
        }

        private double? _value;

        public double? DoubleValue
        {
            get { return _value; }
            set { ChangeAndNotify(ref _value, value, () => DoubleValue); }
        }
    }
}
