using System.Windows.Controls;

namespace VirtualizingTreeView.Controls {
    public class SelectableVirtualizingStackPanel : VirtualizingStackPanel {
        public void BringIntoView(int index) {
            if (index < 0) {
                return;
            }
            BringIndexIntoView(index);
        }
    }
}
