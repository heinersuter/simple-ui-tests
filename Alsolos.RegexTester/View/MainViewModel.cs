namespace Alsolos.RegexTester.View
{
    using System.Text.RegularExpressions;
    using Alsolos.RegexTester.Controller;
    using Commons.Mvvm;

    public class MainViewModel : ViewModel
    {
        private readonly RegexEvaluator _evaluator = new RegexEvaluator();

        public string InputText
        {
            get { return BackingFields.GetValue(() => InputText); }
            set { BackingFields.SetValue(() => InputText, value); }
        }

        public string RegexPattern
        {
            get { return BackingFields.GetValue(() => RegexPattern); }
            set { BackingFields.SetValue(() => RegexPattern, value); }
        }

        public string OutputText
        {
            get { return BackingFields.GetValue(() => OutputText); }
            set { BackingFields.SetValue(() => OutputText, value); }
        }

        public DelegateCommand ExecuteCommand
        {
            get { return BackingFields.GetCommand(() => ExecuteCommand, Execute, CanExecute); }
        }

        private void Execute()
        {
            OutputText = _evaluator.Evaluate(InputText, new Regex(RegexPattern));
        }

        private bool CanExecute()
        {
            return !string.IsNullOrWhiteSpace(InputText) && !string.IsNullOrWhiteSpace(RegexPattern);
        }

        protected override void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(sender, e);

            ExecuteCommand.RaiseCanExecuteChanged();
        }
    }
}
