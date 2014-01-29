using System.ComponentModel;
using VirtualizingTreeView.Model;

namespace VirtualizingTreeView {
    public class TreeItemViewModel : TreeItemBase, INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; set; }

        private bool _isSelected;

        public override bool IsSelected {
            get {
                return _isSelected;
            }
            set {
                if (_isSelected == value) {
                    return;
                }
                _isSelected = value;
                RaisePropertyChanged("IsSelected");
                RaiseDescendantSelected(this);
            }
        }

        private bool _isExpanded;

        public override bool IsExpanded {
            get {
                return _isExpanded;
            }
            set {
                if (_isExpanded == value) {
                    return;
                }
                _isExpanded = value;
                RaisePropertyChanged("IsExpanded");
            }
        }

        public void AddChild() {
            IsExpanded = true;
            var subItemViewModel = Add();
            subItemViewModel.IsSelected = true;
        }

        public void AddChildInitial() {
            IsExpanded = true;
            Add();
        }

        private TreeItemViewModel Add() {
            var subItemViewModel = new TreeItemViewModel { Parent = this, Name = Name + "_" + Children.Count };
            Children.Add(subItemViewModel);
            return subItemViewModel;
        }

        private void RaisePropertyChanged(string propertyName) {
            var handler = PropertyChanged;
            if (handler != null) {
                handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
