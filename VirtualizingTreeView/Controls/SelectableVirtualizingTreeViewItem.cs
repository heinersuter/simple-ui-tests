using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace VirtualizingTreeView.Controls {
    public class SelectableVirtualizingTreeViewItem : TreeViewItem {
        public SelectableVirtualizingTreeViewItem() {
            var panelfactory = new FrameworkElementFactory(typeof(SelectableVirtualizingStackPanel));
            panelfactory.SetValue(Panel.IsItemsHostProperty, true);
            var template = new ItemsPanelTemplate { VisualTree = panelfactory };
            ItemsPanel = template;

            SetBinding(IsSelectedProperty, new Binding("IsSelected"));
            SetBinding(IsExpandedProperty, new Binding("IsExpanded"));
        }

        protected override DependencyObject GetContainerForItemOverride() {
            return new SelectableVirtualizingTreeViewItem();
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item) {
            base.PrepareContainerForItemOverride(element, item);
            ((TreeViewItem)element).IsExpanded = true;
        }
    }
}
