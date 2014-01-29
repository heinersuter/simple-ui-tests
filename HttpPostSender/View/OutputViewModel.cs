namespace HttpPostSender.View
{
    using System.Collections.ObjectModel;
    using Commons.Mvvm;

    public class OutputViewModel : ViewModel
    {
        public ObservableCollection<string> Messages
        {
            get { return BackingFields.GetValue(() => Messages, () => new ObservableCollection<string>()); }
        }
    }
}
