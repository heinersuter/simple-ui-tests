namespace Alsolos.RegexTester.View
{
    using System;
    using System.Windows;

    public partial class MainWindowView
    {
        public MainWindowView()
        {
            InitializeComponent();

            var uri = new Uri(@"/View/help/help.html", UriKind.RelativeOrAbsolute);
            var streamResourceInfo = Application.GetResourceStream(uri);
            if (streamResourceInfo == null)
            {
                throw new ApplicationException(@"'help.html' resource not found!");
            }
            var source = streamResourceInfo.Stream;
            _browser.NavigateToStream(source);
        }
    }
}
