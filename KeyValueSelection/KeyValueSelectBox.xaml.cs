namespace KeyValueSelection
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Commons;
    using Commons.Mvvm;

    public partial class KeyValueSelectBox : INotifyPropertyChanged
    {
        private List<SelectableItem> _allListItems;

        public KeyValueSelectBox()
        {
            this.InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #region Dependecy Properties

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            "ItemsSource", typeof(IEnumerable), typeof(KeyValueSelectBox), new PropertyMetadata(OnItemsSourcePropertyChanged));

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)this.GetValue(ItemsSourceProperty); }
            set { this.SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem", typeof(object), typeof(KeyValueSelectBox), new PropertyMetadata(OnSelectedItemPropertyChanged));

        public object SelectedItem
        {
            get { return this.GetValue(SelectedItemProperty); }
            set { this.SetValue(SelectedItemProperty, value); }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(KeyValueSelectBox), new PropertyMetadata(OnTextPropertyChanged));

        public string Text
        {
            get { return (string)this.GetValue(TextProperty); }
            set { this.SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextPathProperty = DependencyProperty.Register(
            "TextPath", typeof(string), typeof(KeyValueSelectBox), new PropertyMetadata(OnTextPathPropertyChanged));

        public string TextPath
        {
            get { return (string)this.GetValue(TextPathProperty); }
            set { this.SetValue(TextPathProperty, value); }
        }

        public static readonly DependencyProperty SelectedValueProperty = DependencyProperty.Register(
            "SelectedValue", typeof(object), typeof(KeyValueSelectBox), new PropertyMetadata(OnSelectedValuePropertyChanged));

        public object SelectedValue
        {
            get { return this.GetValue(SelectedValueProperty); }
            set { this.SetValue(SelectedValueProperty, value); }
        }

        public static readonly DependencyProperty SelectedValuePathProperty = DependencyProperty.Register(
            "SelectedValuePath", typeof(string), typeof(KeyValueSelectBox), new PropertyMetadata(OnSelectedValuePathPropertyChanged));

        public string SelectedValuePath
        {
            get { return (string)this.GetValue(SelectedValuePathProperty); }
            set { this.SetValue(SelectedValuePathProperty, value); }
        }

        public static readonly DependencyProperty SelectedDescriptionProperty = DependencyProperty.Register(
            "SelectedDescription", typeof(string), typeof(KeyValueSelectBox), new PropertyMetadata(OnSelectedDescriptionPropertyChanged));

        public string SelectedDescription
        {
            get { return (string)this.GetValue(SelectedDescriptionProperty); }
            set { this.SetValue(SelectedDescriptionProperty, value); }
        }

        public static readonly DependencyProperty SelectedDescriptionPathProperty = DependencyProperty.Register(
            "SelectedDescriptionPath", typeof(string), typeof(KeyValueSelectBox), new PropertyMetadata(OnSelectedDescriptionPathPropertyChanged));

        public string SelectedDescriptionPath
        {
            get { return (string)this.GetValue(SelectedDescriptionPathProperty); }
            set { this.SetValue(SelectedDescriptionPathProperty, value); }
        }

        #endregion Dependecy Properties

        public IEnumerable<SelectableItem> FilteredListItems { get; private set; }

        private SelectableItem _selectedListItem;

        public SelectableItem SelectedListItem
        {
            get { return this._selectedListItem; }
            set
            {
                if (this._selectedListItem != value)
                {
                    this._selectedListItem = value;
                    this.RaisePropertyChanged(() => this.SelectedItem);
                    this.SetPublicValuesBySelecedItem();
                }
            }
        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpresssion)
        {
            var propertyName = PropertyHelper.GetName(propertyExpresssion);
            this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private static void OnItemsSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (KeyValueSelectBox)d;
            control.InitializeAllListItems();
        }

        private static void OnSelectedItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (KeyValueSelectBox)d;
            control.SelectedListItem = control._allListItems.FirstOrDefault((listItem) => listItem.Item == control.SelectedItem);
        }

        private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private static void OnTextPathPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (KeyValueSelectBox)d;
            control.InitializeAllListItems();
        }

        private static void OnSelectedValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private static void OnSelectedValuePathPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (KeyValueSelectBox)d;
            control.InitializeAllListItems();
        }

        private static void OnSelectedDescriptionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private static void OnSelectedDescriptionPathPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (KeyValueSelectBox)d;
            control.InitializeAllListItems();
        }

        private void SetPublicValuesBySelecedItem()
        {
            if (this.SelectedListItem != null)
            {
                var item = this.SelectedListItem.Item;
                this.SelectedItem = item;
                this.Text = ReflectionHelper.GetProperty(item, this.TextPath, item.ToString());
                this.SelectedValue = ReflectionHelper.GetProperty(item, this.SelectedValuePath, item);
                this.SelectedDescription = ReflectionHelper.GetProperty<string>(item, this.SelectedDescriptionPath);
            }
            else
            {
                this.SelectedItem = null;
                this.Text = null;
                this.SelectedValue = null;
                this.SelectedDescription = null;
            }
        }

        private void InitializeAllListItems()
        {
            this._allListItems = new List<SelectableItem>();
            if (this.ItemsSource != null)
            {
                foreach (var item in this.ItemsSource)
                {
                    var text = ReflectionHelper.GetProperty(item, this.TextPath, item.ToString());
                    var value = ReflectionHelper.GetProperty(item, this.SelectedValuePath, item);
                    var description = ReflectionHelper.GetProperty<string>(item, this.SelectedDescriptionPath);
                    this._allListItems.Add(new SelectableItem { Item = item, Value = value, Text = text, Description = description });
                }
            }
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            this.OpenPopup(false);
        }

        private void OnItemClick(object sender, EventArgs e)
        {
            this.ClosePopup();
        }

        private void OnItemKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                this.ClosePopup();
            }
        }

        private void OnTextBoxPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up || e.Key == Key.Down)
            {
                e.Handled = true;
                this.OpenPopup(false);
            }
        }

        private void OnTextBoxTextInput(object sender, TextCompositionEventArgs e)
        {
            this.OpenPopup(true);
        }

        private void OpenPopup(bool useTextFilter)
        {
            if (useTextFilter && !string.IsNullOrWhiteSpace(Text))
            {
                FilteredListItems = _allListItems.Where((item) => item.Text.Trim().ToLowerInvariant().Contains(this.Text.Trim().ToLowerInvariant()));
            }
            else
            {
                FilteredListItems = _allListItems;
            }
            this.RaisePropertyChanged(() => FilteredListItems);

            this._popup.IsOpen = true;
            if (SelectedListItem == null)
            {
                SelectedListItem = FilteredListItems.FirstOrDefault();
            }
            var listBoxItem = (ListBoxItem)_listBox.ItemContainerGenerator.ContainerFromItem(SelectedListItem);
            if (listBoxItem != null)
            {
                listBoxItem.Focus();
            }
        }

        private void ClosePopup()
        {
            this._popup.IsOpen = false;
            this._textBox.Focus();
        }
    }
}
