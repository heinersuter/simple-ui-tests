namespace OnScreenKeyboard.Controls
{
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class IntTextBox : TextBox
    {
        static IntTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IntTextBox), new FrameworkPropertyMetadata(typeof(IntTextBox)));
        }

        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnGotKeyboardFocus(e);
            var result = OnScreenKeyboardService.GetIntValue();
            if (result != null)
            {
                Text = result.Value.ToString(CultureInfo.CurrentCulture);
            }
        }
    }
}
