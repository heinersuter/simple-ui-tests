namespace OnScreenKeyboard.Controls
{
    public class OnScreenKeyboardService
    {
        public static int? GetIntValue()
        {
            IntKeyboardViewModel.Show();
            return 123;
        }

        public static IntKeyboardViewModel IntKeyboardViewModel { get; set; }
    }
}