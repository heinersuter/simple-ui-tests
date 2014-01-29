namespace Commons
{
    using System;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Interactivity;

    public class InputRegexBahavior : Behavior<TextBox>
    {
        public static readonly DependencyProperty RegularExpressionProperty = DependencyProperty.Register(
            "RegularExpression", typeof(string), typeof(InputRegexBahavior), new FrameworkPropertyMetadata("*"));

        public string RegularExpression
        {
            get { return (string)GetValue(RegularExpressionProperty); }
            set { SetValue(RegularExpressionProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.PreviewTextInput += this.OnPreviewTextInput;
            DataObject.AddPastingHandler(this.AssociatedObject, this.OnPaste);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.PreviewTextInput -= this.OnPreviewTextInput;
            DataObject.RemovePastingHandler(this.AssociatedObject, this.OnPaste);
        }

        private void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                string text = Convert.ToString(e.DataObject.GetData(DataFormats.Text));

                if (!Regex.IsMatch(text, this.RegularExpression))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string text = this.AssociatedObject.Text;
            if (this.AssociatedObject.SelectionStart + this.AssociatedObject.SelectionLength > this.AssociatedObject.Text.Length)
            {
                text = text + e.Text;
            }
            else
            {
                text = text.Remove(this.AssociatedObject.SelectionStart, this.AssociatedObject.SelectionLength);
                text = text.Insert(this.AssociatedObject.SelectionStart, e.Text);
            }
            e.Handled = !Regex.IsMatch(text, this.RegularExpression);
        }
    }
}
