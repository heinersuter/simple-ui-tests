using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VirtualizingTreeView.Presentation;

namespace VirtualizingTreeView.Controls {
    /// <summary>
    /// Allows settings selection in a virtualized tree view.
    /// </summary>
    /// <remarks>See <see cref="http://code.msdn.microsoft.com/Changing-selection-in-a-6a6242c8"/></remarks>
    public class SelectableVirtualizingTreeView : TreeView {
        public SelectableVirtualizingTreeView() {
            VirtualizingStackPanel.SetIsVirtualizing(this, true);
            VirtualizingStackPanel.SetVirtualizationMode(this, VirtualizationMode.Recycling);
        
            var panelfactory = new FrameworkElementFactory(typeof(SelectableVirtualizingStackPanel));
            panelfactory.SetValue(Panel.IsItemsHostProperty, true);
            var template = new ItemsPanelTemplate { VisualTree = panelfactory };
            ItemsPanel = template;
        }

        public void BringItemIntoView(ITreeItem treeItemViewModel) {
            if (treeItemViewModel == null) {
                return;
            }

            var stack = new Stack<ITreeItem>();
            stack.Push(treeItemViewModel);
            while (treeItemViewModel.Parent != null) {
                stack.Push(treeItemViewModel.Parent);
                treeItemViewModel = treeItemViewModel.Parent;
            }

            ItemsControl containerControl = this;
            while (stack.Count > 0) {
                var viewModel = stack.Pop();
                var treeViewItem = containerControl.ItemContainerGenerator.ContainerFromItem(viewModel);
                var virtualizingPanel = FindVisualChild<SelectableVirtualizingStackPanel>(containerControl);
                if (virtualizingPanel != null) {
                    var index = viewModel.Parent != null ? viewModel.Parent.Children.IndexOf(viewModel) : Items.IndexOf(treeViewItem);
                    virtualizingPanel.BringIntoView(index);
                    Focus();
                }
                containerControl = (ItemsControl)treeViewItem;
            }
        }
        
        protected override DependencyObject GetContainerForItemOverride() {
            return new SelectableVirtualizingTreeViewItem();
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item) {
            base.PrepareContainerForItemOverride(element, item);
            ((TreeViewItem)element).IsExpanded = true;
        }

        private static T FindVisualChild<T>(Visual visual) where T : Visual {
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(visual); i++) {
                var child = (Visual)VisualTreeHelper.GetChild(visual, i);
                if (child == null) {
                    continue;
                }
                var correctlyTyped = child as T;
                if (correctlyTyped != null) {
                    return correctlyTyped;
                }
                var descendent = FindVisualChild<T>(child);
                if (descendent != null) {
                    return descendent;
                }
            }
            return null;
        }
    }
}
