using System.Collections.Generic;

namespace VirtualizingTreeView.Presentation {
    public interface ITreeItem {
        ITreeItem Parent { get; }

        IList<ITreeItem> Children { get; }

        bool IsSelected { get; set; }
     
        bool IsExpanded { get; set; }
    }
}
