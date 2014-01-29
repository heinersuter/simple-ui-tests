using SaveActualHeightInSettings.Properties;

namespace SaveActualHeightInSettings {
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();
            DataContext = this;
        }

        public int RowHeight {
            get {
                return Settings.Default.RowHeight;
            }
            set {
                Settings.Default.RowHeight = value;
                Settings.Default.Save();
            }
        }
    }
}
