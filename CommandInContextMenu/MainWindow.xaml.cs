using System;
using System.Collections;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace CommandInContextMenu {
    public partial class MainWindow {
        private Item _selectedItem;

        public MainWindow() {
            InitializeComponent();

            Items = new ArrayList{
                new Item { A = "Hello", B ="World" },
                new Item { A = "X1", B ="X2" },
            };

            WriteCommand = new DelegateCommand(Write, CanWrite);

            DataContext = this;
        }

        public IList Items { get; private set; }

        public Item SelectedItem {
            get {
                return _selectedItem;
            }
            set {
                _selectedItem = value;
                WriteCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateCommand WriteCommand { get; private set; }

        private bool CanWrite() {
            return SelectedItem != null && SelectedItem.A.Contains("1");
        }

        private void Write() {
            Console.Write(SelectedItem.A);
            Console.Write(@" ");
            Console.WriteLine(SelectedItem.B);
        }

        public class Item {
            public string A { get; set; }
            public string B { get; set; }
        }
    }
}
