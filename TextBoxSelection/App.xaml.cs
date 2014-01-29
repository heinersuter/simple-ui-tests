using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace TextBoxSelection
{
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            EventManager.RegisterClassHandler(typeof(TextBox), UIElement.GotKeyboardFocusEvent, new KeyboardFocusChangedEventHandler(OnGotKeyboardFocus));
            EventManager.RegisterClassHandler(typeof(TextBox), Control.MouseDoubleClickEvent, new RoutedEventHandler(OnTextBoxMouseDoubleClick));
        }

        private void OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (e.OldFocus is MenuItem)
            {
                return;
            }
            SelectTextInTextBox(sender);
        }

        private void OnTextBoxMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource.GetType().Name != "TextBoxView")
            {
                // Ignore clicks on chrome
                return;
            }
            SelectTextInTextBox(sender);
        }

        private static void SelectTextInTextBox(object sender)
        {
            var textBox = sender as TextBox;
            if (textBox == null)
            {
                return;
            }
            if (!string.IsNullOrEmpty(textBox.SelectedText))
            {
                return;
            }
            textBox.SelectAll();
        }
    }
}
