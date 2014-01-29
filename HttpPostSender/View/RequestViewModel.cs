namespace HttpPostSender.View
{
    using Commons.Mvvm;

    public class RequestViewModel : ViewModel
    {
        public string Content
        {
            get { return BackingFields.GetValue(() => Content); }
            set { BackingFields.SetValue(() => Content, value); }
        }
    }
}
