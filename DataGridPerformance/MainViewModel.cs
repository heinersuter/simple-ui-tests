namespace DataGridPerformance
{
    using System.Collections.Generic;
    using Commons;
    using Commons.Mvvm;

    public class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            for (var i = 0; i < 500; i++)
            {
                Items.Add(new ItemViewModel("Item " + i));
            }
        }

        public List<ItemViewModel> Items
        {
            get { return BackingFields.GetValue(() => Items, () => new List<ItemViewModel>()); }
            set { BackingFields.SetValue(() => Items, value); }
        }
    }
}
