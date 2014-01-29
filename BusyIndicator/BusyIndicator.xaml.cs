namespace BusyIndicator
{
    using System.Windows;
    using System.Windows.Media.Animation;

    public partial class BusyIndicator
    {
        private readonly Storyboard _storyBoard;

        public BusyIndicator()
        {
            this.InitializeComponent();
            this._storyBoard = (Storyboard)this.FindResource("Spin360");
            this.StartOrStopAnimation();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property.Name == "Visibility")
            {
                this.StartOrStopAnimation();
            }
        }

        private void StartOrStopAnimation()
        {
            if (this.Visibility == Visibility.Visible)
            {
                this._storyBoard.Begin();
            }
            else
            {
                this._storyBoard.Stop();
            }
        }
    }
}
