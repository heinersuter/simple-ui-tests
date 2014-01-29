namespace DocLayoutCalc
{
    using System;
    using Commons.Mvvm;

    public class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
            CalculateAndSetValues();
        }

        public double PageWidth
        {
            get { return BackingFields.GetValue(() => PageWidth, () => 210.0); }
            set
            {
                if (BackingFields.SetValue(() => PageWidth, value))
                {
                    CalculateAndSetValues();
                }
            }
        }

        public double PageBorderWidth
        {
            get { return BackingFields.GetValue(() => PageBorderWidth, () => 8.0); }
            set
            {
                if (BackingFields.SetValue(() => PageBorderWidth, value))
                {
                    CalculateAndSetValues();
                }
            }
        }

        public double PageBorderWidthSum
        {
            get { return BackingFields.GetValue(() => PageBorderWidthSum); }
            set { BackingFields.SetValue(() => PageBorderWidthSum, value); }
        }

        public uint ColumnCount
        {
            get { return BackingFields.GetValue<uint>(() => ColumnCount, () => 6); }
            set
            {
                if (BackingFields.SetValue(() => ColumnCount, value))
                {
                    CalculateAndSetValues();
                }
            }
        }

        public double ColumnSpacing
        {
            get { return BackingFields.GetValue(() => ColumnSpacing, () => 4.0); }
            set
            {
                if (BackingFields.SetValue(() => ColumnSpacing, value))
                {
                    CalculateAndSetValues();
                }
            }
        }

        public double ColumnSpacingSum
        {
            get { return BackingFields.GetValue(() => ColumnSpacingSum); }
            set { BackingFields.SetValue(() => ColumnSpacingSum, value); }
        }

        public double ColumnWidth
        {
            get { return BackingFields.GetValue(() => ColumnWidth); }
            set { BackingFields.SetValue(() => ColumnWidth, value); }
        }

        public double ColumnWidthSum
        {
            get { return BackingFields.GetValue(() => ColumnWidthSum); }
            set { BackingFields.SetValue(() => ColumnWidthSum, value); }
        }

        private void CalculateAndSetValues()
        {
            PageBorderWidthSum = 2 * PageBorderWidth;
            ColumnSpacingSum = (ColumnCount - 1) * ColumnSpacing;
            ColumnWidthSum = PageWidth - PageBorderWidthSum - ColumnSpacingSum;
            ColumnWidth = ColumnWidthSum / ColumnCount;

            PrintTabstops();
            PrintImageSizes();
        }

        private void PrintTabstops()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(@"TabStops:");
            Console.Write(@"Left: ");
            for (var i = 1; i < ColumnCount; i++)
            {
                Console.Write(i * (ColumnWidth + ColumnSpacing));
                Console.Write(@" ");
            }
            Console.WriteLine();
            Console.Write(@"Rigth: ");
            for (var i = 0; i < ColumnCount; i++)
            {
                Console.Write(((i + 1) * ColumnWidth) + (i * ColumnSpacing));
                Console.Write(@" ");
            }
        }

        private void PrintImageSizes()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(@"Image Sizes:");
            for (var i = 1; i < ColumnCount; i++)
            {
                Console.Write(i * (ColumnWidth + ColumnSpacing));
                Console.Write(@" ");
            }
        }
    }
}
