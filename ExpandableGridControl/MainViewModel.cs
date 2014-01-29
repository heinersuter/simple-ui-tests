using System.Collections.Generic;
using Alsolos.Commons.Mvvm;
using ExpandableGridControl.Control;

namespace ExpandableGridControl {
    public class MainViewModel : ViewModel {
        public MainViewModel() {
            Items.Add(CreateItem("A"));
            Items.Add(CreateItem("B"));
            Items.Add(CreateItem("C"));

            RaisePropertyChanged(() => Items);
        }

        public IList<IHierarchicalDataGridItem> Items {
            get { return BackingFields.GetValue(() => Items, () => new List<IHierarchicalDataGridItem>()); }
        }

        private static MyItem CreateItem(string name) {
            var item1 = new MyItem { CompanyName = "Company " + name, FirstName = "First " + name, LastName = "Last " + name };

            var item1SubItem1 = new MyItem { CompanyName = "Company " + name + "1", FirstName = "First " + name + "1", LastName = "Last " + name + "1" };

            var item1SubItem1SubItem1 = new MyItem { CompanyName = "Company " + name + "1.1", FirstName = "First " + name + "1.1", LastName = "Last " + name + "1.1" };

            var item1SubItem1SubItem1SubItem1 = new MyItem { CompanyName = "Company " + name + "1.1.1", FirstName = "First " + name + "1.1.1", LastName = "Last " + name + "1.1.1" };
            item1SubItem1SubItem1.SubItems.Add(item1SubItem1SubItem1SubItem1);
            item1SubItem1.SubItems.Add(item1SubItem1SubItem1);

            var item1SubItem1SubItem2 = new MyItem { CompanyName = "Company " + name + "1.2", FirstName = "First " + name + "1.2", LastName = "Last " + name + "1.2" };
            item1SubItem1.SubItems.Add(item1SubItem1SubItem2);
            item1.SubItems.Add(item1SubItem1);

            var item1SubItem2 = new MyItem { CompanyName = "Company " + name + "2", FirstName = "First " + name + "2", LastName = "Last " + name + "2" };

            var item1SubItem2SubItem1 = new MyItem { CompanyName = "Company " + name + "2.1", FirstName = "First " + name + "2.1", LastName = "Last " + name + "2.1" };
            item1SubItem2.SubItems.Add(item1SubItem2SubItem1);

            var item1SubItem2SubItem2 = new MyItem { CompanyName = "Company " + name + "2.2", FirstName = "First " + name + "2.2", LastName = "Last " + name + "2.2" };
            item1SubItem2.SubItems.Add(item1SubItem2SubItem2);
            item1.SubItems.Add(item1SubItem2);

            return item1;
        }
    }
}
