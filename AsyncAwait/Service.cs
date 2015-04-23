namespace AsyncAwait
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class Service
    {
        public async Task<string> LoadTextAsync()
        {
            return await Task.Run((Func<string>)LoadText);
        }

        private string LoadText()
        {
            Thread.Sleep(2000);
            return "Hello";
        }
    }
}
