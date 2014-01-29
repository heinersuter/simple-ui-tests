using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BinarySizes {
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();

            for (var i = 2; i <= 1024; i = i * 2) {
                _container.Children.Add(CreateElement(i, Brushes.Red));
                _container.Children.Add(CreateElement(i + (i / 2), Brushes.Green));
            }
        }

        private static Border CreateElement(int size, SolidColorBrush brush) {
            var border = new Border {
                BorderBrush = brush,
                BorderThickness = new Thickness(0, 0, 1, 0),
                Width = size,
                Height = 128,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
            };
            var textBlock = new TextBlock {
                Text = size.ToString(CultureInfo.InvariantCulture),
                FontSize = size < 32 ? size / 2 : 12,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
            };
            border.Child = textBlock;
            return border;
        }
    }
}
