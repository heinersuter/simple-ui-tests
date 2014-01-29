namespace HttpPostSender.View
{
    using System;
    using Commons.Dialog;
    using Commons.Mvvm;
    using HttpPostSender.Sender;

    public class MainViewModel : ViewModel
    {
        private readonly HttpSender _sender = new HttpSender();

        public MainViewModel()
        {
            // For testing
            UrlViewModel.Uri = new Uri("http://dev02.zrh.kessler.ch:8080/ValescoServer-web/resources/session");
            UrlViewModel.History.Add(new Uri("http://dev02.zrh.kessler.ch:8080/ValescoServer-web/resources/session"));
            UrlViewModel.History.Add(new Uri("http://www.google.com"));
            UrlViewModel.History.Add(new Uri("http://www.zuehlke.ch"));
            UrlViewModel.History.Add(new Uri("http://www.kessler.ch"));
            RequestViewModel.Content = "command=login&username=dev&password=rol";
        }

        public UrlViewModel UrlViewModel
        {
            get { return BackingFields.GetValue(() => UrlViewModel, () => new UrlViewModel()); }
        }

        public RequestViewModel RequestViewModel
        {
            get { return BackingFields.GetValue(() => RequestViewModel, () => new RequestViewModel()); }
        }

        public ResponseViewModel ResponseViewModel
        {
            get { return BackingFields.GetValue(() => ResponseViewModel, () => new ResponseViewModel()); }
        }

        public OutputViewModel OutputViewModel
        {
            get { return BackingFields.GetValue(() => OutputViewModel, () => new OutputViewModel()); }
        }

        public FavoriteListViewModel FavoriteListViewModel
        {
            get { return BackingFields.GetValue(() => FavoriteListViewModel, () => new FavoriteListViewModel()); }
        }

        public DelegateCommand AddToFavoritesCommand
        {
            get { return BackingFields.GetCommand(() => AddToFavoritesCommand, AddToFavorites, CanAddToFavorites); }
        }

        private void AddToFavorites()
        {
            var content = new FavoriteViewModel { Uri = UrlViewModel.Uri, Request = RequestViewModel.Content };
            var dialogViewModel = new DialogViewModel
                {
                    Content = content,
                    Callback = result =>
                    {
                        if (result == true)
                        {
                            FavoriteListViewModel.Favorites.Add(content);
                        }
                    }
                };
            dialogViewModel.ShowDialog();
        }

        private bool CanAddToFavorites()
        {
            return UrlViewModel.Uri != null;
        }

        public DelegateCommand SendCommand
        {
            get { return BackingFields.GetCommand(() => SendCommand, Send, CanSend); }
        }

        private void Send()
        {
            var responseObject = _sender.SendPost(new TransferObject { Content = RequestViewModel.Content, Uri = UrlViewModel.Uri });
            ResponseViewModel.Content = responseObject.Content;
            if (!string.IsNullOrWhiteSpace(responseObject.Cookies))
            {
                OutputViewModel.Messages.Add(string.Format("Cookies received: {0}", responseObject.Cookies));
            }
        }

        private bool CanSend()
        {
            return UrlViewModel.Uri != null && UrlViewModel.Uri.IsAbsoluteUri;
        }
    }
}
