namespace HttpPostSender.View
{
    using System.Windows.Controls;
    using System.Windows.Data;

    public partial class UrlView
    {
        public UrlView()
        {
            InitializeComponent();
        }

        private void OnComboBoxSourceUpdated(object sender, DataTransferEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox != null)
            {
                comboBox.IsDropDownOpen = true;
            }
        }
    }
}
