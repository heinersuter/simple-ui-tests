namespace DialogForMemoryTest
{
    using System.ComponentModel;

    public class DialogDataContext //: INotifyPropertyChanged
    {
        public DialogDataContext()
        {
            DummyData = new string('a', 100000000);
            DisplayText = "Hello";
        }

        public string DummyData { get; set; }

        public string DisplayText { get; set; }

        // public event PropertyChangedEventHandler PropertyChanged;
    }
}
