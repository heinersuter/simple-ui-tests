namespace HttpPostSender.View
{
    using Commons.Mvvm;

    public class ResponseViewModel : ViewModel
    {
        public string Content
        {
            get { return BackingFields.GetValue(() => Content); }
            set { BackingFields.SetValue(() => Content, value); }
        }
    }
}
