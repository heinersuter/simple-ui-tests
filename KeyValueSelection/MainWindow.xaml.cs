namespace KeyValueSelection
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Items = new List<TestItem1>();
            Items.Add(new TestItem1 { Id = 1, Content = "Januar", Date = new DateTime(2012, 1, 1) });
            Items.Add(new TestItem1 { Id = 2, Content = "Februar", Date = new DateTime(2012, 2, 1) });
            Items.Add(new TestItem1 { Id = 3, Content = "März", Date = new DateTime(2012, 3, 1) });
            Items.Add(new TestItem1 { Id = 11, Content = "November", Date = new DateTime(2012, 11, 1) });
            Items.Add(new TestItem1 { Id = 12, Content = "Dezember", Date = new DateTime(2012, 12, 1) });

            this.DataContext = this;
        }

        public List<TestItem1> Items { get; private set; }

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem", typeof(TestItem1), typeof(MainWindow), new PropertyMetadata(default(TestItem1)));

        public TestItem1 SelectedItem
        {
            get { return (TestItem1)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly DependencyProperty DateProperty = DependencyProperty.Register(
            "Date", typeof(DateTime?), typeof(MainWindow), new PropertyMetadata(default(DateTime?)));

        public DateTime? Date
        {
            get { return (DateTime?)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }
    }
}
