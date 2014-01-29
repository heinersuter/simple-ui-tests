using System.Collections.Generic;
using Alsolos.Commons.Mvvm;

namespace ExpandableGridControl.Control {
    public class HierachicalDataGridItemWrapper : BackingFieldsHolder {
        private HierachicalDataGridItemWrapper(IHierarchicalDataGridItem item) {
            Value = item;
            Children = new List<HierachicalDataGridItemWrapper>();
        }

        public static HierachicalDataGridItemWrapper CreateRecursively(IHierarchicalDataGridItem item) {
            return CreateRecursively(item, -1);
        }

        private static HierachicalDataGridItemWrapper CreateRecursively(IHierarchicalDataGridItem item, int parentLevel) {
            var wrapper = new HierachicalDataGridItemWrapper(item) {
                Level = parentLevel + 1
            };
            foreach (var childItem in item.Children) {
                var childWrapper = CreateRecursively(childItem, wrapper.Level);
                wrapper.Children.Add(childWrapper);
            }
            return wrapper;
        }

        public IHierarchicalDataGridItem Value { get; private set; }

        public IList<HierachicalDataGridItemWrapper> Children { get; private set; }

        public bool IsExpanded {
            get { return BackingFields.GetValue(() => IsExpanded); }
            set { BackingFields.SetValue(() => IsExpanded, value, OnIsExpandedChanged); }
        }

        public bool IsParentExpanded {
            get { return BackingFields.GetValue(() => IsParentExpanded); }
            set { BackingFields.SetValue(() => IsParentExpanded, value); }
        }

        public int Level { get; set; }

        public void ExpandRecursively() {
            IsExpanded = true;
            foreach (var child in Children) {
                child.ExpandRecursively();
            }
        }

        public void CollapseRecursively() {
            IsExpanded = false;
            foreach (var child in Children) {
                child.CollapseRecursively();
            }
        }

        private void OnIsExpandedChanged(bool newValue) {
            SetIsParentExpandedToAllSubItemsRecursively(newValue);
        }

        private void SetIsParentExpandedToAllSubItemsRecursively(bool isExpanded) {
            foreach (var child in Children) {
                child.IsParentExpanded = isExpanded;
                if (!isExpanded) {
                    child.SetIsParentExpandedToAllSubItemsRecursively(false);
                } else {
                    child.SetIsParentExpandedToAllSubItemsRecursively(child.IsExpanded);
                }
            }
        }
    }
}
