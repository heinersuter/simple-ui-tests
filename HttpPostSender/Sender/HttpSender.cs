namespace HttpPostSender.Sender
{
    using System;
    using System.Net;

    public class HttpSender
    {
        private string _cookiesStore;
        
        public TransferObject SendPost(TransferObject request)
        {
            var webClient = new WebClient();

            if (_cookiesStore != null)
            {
                webClient.Headers.Add("Cookie", _cookiesStore);
            }

            if (request.Content == null)
            {
                request.Content = string.Empty;
            }

            var responseContent = webClient.UploadString(request.Uri, request.Content);

            _cookiesStore = webClient.ResponseHeaders["Set-Cookie"];

            return new TransferObject
            {
                Content = responseContent,
                Timestamp = DateTime.Now,
                Uri = request.Uri,
                Cookies = _cookiesStore,
            };
        }

        public void ClearCookies()
        {
            _cookiesStore = null;
        }
    }
}
