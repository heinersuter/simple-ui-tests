using System.Windows;

namespace ExpandableGridControl {
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();
        }

        private void OnExpandAllClicked(object sender, RoutedEventArgs e) {
            _grid.ExpandAll();
        }

        private void OnCollapseAllClicked(object sender, RoutedEventArgs e) {
            _grid.CollapseAll();
        }
    }
}
