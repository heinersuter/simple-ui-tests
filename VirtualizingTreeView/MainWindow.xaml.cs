using System;
using System.Windows;
using System.Windows.Controls;

namespace VirtualizingTreeView {
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();
            var mainViewModel = (MainViewModel)DataContext;
            mainViewModel.DescendantSelected += OnMainViewModelDescendantSelected;
        }

        private void OnAddButtonClick(object sender, RoutedEventArgs e) {
            var mainViewModel = (MainViewModel)DataContext;
            var treeItemViewModel = mainViewModel.SelectedItem;
            if (treeItemViewModel != null) {
                treeItemViewModel.AddChild();
            }
        }

        private void OnMainViewModelDescendantSelected(object sender, EventArgs eventArgs) {
            _treeView.BringItemIntoView(sender as TreeItemViewModel);
        }

        private void OnTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
            if (e.OldValue == e.NewValue) {
                return;
            }
            var treeView = (TreeView)sender;
            var treeItemviewModel = treeView.SelectedItem as TreeItemViewModel;
            var mainViewModel = (MainViewModel)DataContext;
            mainViewModel.SelectedItem = treeItemviewModel;
        }
    }
}
