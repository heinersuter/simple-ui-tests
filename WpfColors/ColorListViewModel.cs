namespace WpfColors
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Media;
    using WpfColors.Common;

    public class ColorListViewModel : ViewModel
    {
        private static readonly IEnumerable<ColorViewModel> _allColors;

        static ColorListViewModel()
        {
            _allColors = typeof(Colors).GetProperties(BindingFlags.Public | BindingFlags.Static).Select(
                propertyInfo => new ColorViewModel((Color)propertyInfo.GetValue(typeof(Colors), null), propertyInfo.Name));
        }

        public ColorListViewModel()
        {
            this.Colors = _allColors;
        }

        public IEnumerable<ColorViewModel> Colors
        {
            get { return GetValue(() => Colors); }
            set { SetValue(() => Colors, value); }
        }

        [Default(true)]
        public bool IncludeRed
        {
            get { return GetValue(() => IncludeRed); }
            set { SetValue(() => IncludeRed, value, this.FilterList); }
        }

        [Default(true)]
        public bool IncludeGreen
        {
            get { return GetValue(() => IncludeGreen); }
            set { SetValue(() => IncludeGreen, value, this.FilterList); }
        }

        [Default(true)]
        public bool IncludeBlue
        {
            get { return GetValue(() => IncludeBlue); }
            set { SetValue(() => IncludeBlue, value, this.FilterList); }
        }

        private void FilterList()
        {
            this.Colors = _allColors.Where((vm) => 
                (this.IncludeRed && vm.Color.R >= vm.Color.G && vm.Color.R >= vm.Color.B) 
                || (this.IncludeGreen && vm.Color.G >= vm.Color.R && vm.Color.G >= vm.Color.B)
                || (this.IncludeBlue && vm.Color.B >= vm.Color.R && vm.Color.B >= vm.Color.G));
        }
    }
}
