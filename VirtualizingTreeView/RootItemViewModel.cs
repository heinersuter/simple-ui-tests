namespace TreeViewTest {
    public class RootItemViewModel : TreeItemViewModel {
        public void AddChild() {
            //IsExpanded = true;
            var subItemViewModel = new SubItemViewModel { Name = "SubItem_" + Children.Count };
            Children.Add(subItemViewModel);
            //subItemViewModel.IsSelected = true;
        }
    }
}
