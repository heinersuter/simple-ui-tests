namespace HttpPostSender.Sender
{
    using System;

    public class TransferObject
    {
        public string Content { get; set; }

        public string ContentType { get; set; }

        public string Cookies { get; set; }

        public DateTime Timestamp { get; set; }

        public Uri Uri { get; set; }
    }
}
