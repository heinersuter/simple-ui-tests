using System.Collections.Generic;

namespace ExpandableGridControl.Control {
    public interface IHierarchicalDataGridItem {
        IList<IHierarchicalDataGridItem> Children { get; }
    }
}
