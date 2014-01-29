namespace KeyValueSelection
{
    using Commons;
    using Commons.Mvvm;

    public class SelectableItem : ViewModel
    {
        public bool IsSelected
        {
            get { return BackingFields.GetValue(() => this.IsSelected); }
            set { BackingFields.SetValue(() => this.IsSelected, value); }
        }

        public object Item
        {
            get { return BackingFields.GetValue(() => this.Item); }
            set { BackingFields.SetValue(() => this.Item, value); }
        }

        public object Value
        {
            get { return BackingFields.GetValue(() => this.Value); }
            set { BackingFields.SetValue(() => this.Value, value); }
        }

        public string Text
        {
            get { return BackingFields.GetValue(() => this.Text); }
            set { BackingFields.SetValue(() => this.Text, value); }
        }

        public string Description
        {
            get { return BackingFields.GetValue(() => this.Description); }
            set { BackingFields.SetValue(() => this.Description, value); }
        }
    }
}
