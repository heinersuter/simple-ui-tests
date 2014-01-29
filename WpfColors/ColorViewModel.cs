namespace WpfColors
{
    using System.Windows.Media;

    public class ColorViewModel
    {
        public ColorViewModel(Color color, string name)
        {
            this.Color = color;
            this.Name = name;
            
            this.Brush = new SolidColorBrush(this.Color);
            this.OldColor = System.Drawing.Color.FromArgb(this.Color.A, this.Color.R, this.Color.G, this.Color.B);
        }

        public Color Color { get; private set; }

        public string Name { get; private set; }

        public Brush Brush { get; private set; }

        public System.Drawing.Color OldColor { get; private set; }
    }
}
