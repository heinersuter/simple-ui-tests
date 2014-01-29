using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using VirtualizingTreeView.Presentation;

namespace VirtualizingTreeView.Model {
    public abstract class TreeItemBase : ITreeItem {
        protected TreeItemBase() {
            Children = new ObservableCollection<ITreeItem>();
        }

        public ITreeItem Parent { get; protected set; }

        public IList<ITreeItem> Children { get; protected set; }
        
        public abstract bool IsSelected { get; set; }
        
        public abstract bool IsExpanded { get; set; }

        public event EventHandler DescendantSelected;

        protected void RaiseDescendantSelected(TreeItemViewModel newItem) {
            if (Parent != null) {
                ((TreeItemViewModel)Parent).RaiseDescendantSelected(newItem);
            } else {
                var handler = DescendantSelected;
                if (handler != null) {
                    handler.Invoke(newItem, EventArgs.Empty);
                }
            }
        }
    }
}
