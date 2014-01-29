namespace HttpPostSender.View
{
    using System;
    using System.Collections.ObjectModel;
    using Commons.Mvvm;

    public class UrlViewModel : ViewModel
    {
        public Uri Uri
        {
            get { return BackingFields.GetValue(() => Uri); }
            set { BackingFields.SetValue(() => Uri, value); }
        }

        public ObservableCollection<Uri> History
        {
            get { return BackingFields.GetValue(() => History, () => new ObservableCollection<Uri>()); }
        }

        public void UpdateHistory()
        {
            if (Uri != null)
            {
                History.Insert(0, Uri);
                if (History.Count > 20)
                {
                    History.RemoveAt(20);
                }
            }
        }
    }
}
