using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace VirtualizingTreeView {
    public class MainViewModel : INotifyPropertyChanged {
        private TreeItemViewModel _selectedItem;

        public MainViewModel() {
            TreeItemViewModels = new List<TreeItemViewModel> { new TreeItemViewModel { Name = "Item" } };
            for (var i = 0; i < 30; i++) {
                TreeItemViewModels[0].AddChildInitial();
            }
            TreeItemViewModels[0].IsSelected = true;
            TreeItemViewModels[0].DescendantSelected += OnDescendantSelected;
        }

        public event EventHandler DescendantSelected;

        public event PropertyChangedEventHandler PropertyChanged;

        public List<TreeItemViewModel> TreeItemViewModels { get; private set; }

        public TreeItemViewModel SelectedItem {
            get {
                return _selectedItem;
            }
            set {
                if (_selectedItem == value) {
                    return;
                }
                _selectedItem = value;
                var handler = PropertyChanged;
                if (handler != null) {
                    handler.Invoke(this, new PropertyChangedEventArgs("SelectedItem"));
                }
            }
        }

        private void OnDescendantSelected(object sender, EventArgs eventArgs) {
            var handler = DescendantSelected;
            if (handler != null) {
                handler.Invoke(sender, eventArgs);
            }
        }
    }
}
