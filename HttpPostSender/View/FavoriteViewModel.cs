namespace HttpPostSender.View
{
    using System;
    using Commons.Mvvm;

    public class FavoriteViewModel : ViewModel
    {
        public string Name
        {
            get { return BackingFields.GetValue(() => Name); }
            set { BackingFields.SetValue(() => Name, value); }
        }

        public Uri Uri
        {
            get { return BackingFields.GetValue(() => Uri); }
            set { BackingFields.SetValue(() => Uri, value); }
        }

        public string Request
        {
            get { return BackingFields.GetValue(() => Request); }
            set { BackingFields.SetValue(() => Request, value); }
        }
    }
}
