namespace OnScreenKeyboard.Controls
{
    using Commons.Mvvm;
    using Xceed.Wpf.Toolkit;

    public class IntKeyboardViewModel : ViewModel
    {
        public IntKeyboardViewModel()
        {
            OnScreenKeyboardService.IntKeyboardViewModel = this;
        }

        public DelegateCommand OkCommand
        {
            get { return BackingFields.GetCommand(() => OkCommand, () => { WindowState = WindowState.Closed; }); }
        }

        public DelegateCommand EscCommand
        {
            get { return BackingFields.GetCommand(() => EscCommand, () => { WindowState = WindowState.Closed; }); }
        }

        public DelegateCommand<string> AppendCommand
        {
            get { return BackingFields.GetValue(() => AppendCommand, () => new DelegateCommand<string>(s => { Text += s; })); }
        }

        public DelegateCommand BackspaceCommand
        {
            get { return BackingFields.GetCommand(() => BackspaceCommand, () => { Text = Text.Substring(0, (Text.Length - 1)); }); }
        }

        public DelegateCommand ClearCommand
        {
            get { return BackingFields.GetCommand(() => ClearCommand, () => { Text = string.Empty; }); }
        }

        public string Text
        {
            get { return BackingFields.GetValue(() => Text); }
            set { BackingFields.SetValue(() => Text, value); }
        }

        public WindowState WindowState
        {
            get { return BackingFields.GetValue(() => WindowState); }
            set { BackingFields.SetValue(() => WindowState, value); }
        }

        public void Show()
        {
            WindowState = WindowState.Open;
        }
    }
}
