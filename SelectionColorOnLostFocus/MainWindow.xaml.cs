namespace SelectionColorOnLostFocus
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public partial class MainWindow : INotifyPropertyChanged
    {
        private string _selectedItem;
        private int _selectedIndex1;
        private int _selectedIndex2;

        public MainWindow()
        {
            InitializeComponent();
            SelectedIndex1 = -1;
            SelectedIndex2 = -1;
            Items1 = new List<string> { "A", "B", "C", "D" };
            Items2 = new List<string> { "E", "F" };
            DataContext = this;
        }

        public List<string> Items1 { get; set; }

        public List<string> Items2 { get; set; }

        public string SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        public int SelectedIndex1
        {
            get { return _selectedIndex1; }
            set
            {
                if (_selectedIndex1 == value)
                {
                    return;
                }
                _selectedIndex1 = value;
                OnPropertyChanged();
                if (_selectedIndex1 >= 0)
                {
                    SelectedIndex2 = -1;
                    SelectedItem = Items1[_selectedIndex1];
                }
            }
        }

        public int SelectedIndex2
        {
            get { return _selectedIndex2; }
            set
            {
                if (_selectedIndex2 == value)
                {
                    return;
                }
                _selectedIndex2 = value;
                OnPropertyChanged();
                if (_selectedIndex2 >= 0)
                {
                    SelectedIndex1 = -1;
                    SelectedItem = Items2[_selectedIndex2];
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
