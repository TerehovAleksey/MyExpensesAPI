namespace MyExpenses.ClientCore.Helpers
{
    public class HttpInitData : IHttpInitData
    {
        public string BaseAddress { get; }

        public string Token { get; }

        public HttpInitData(string baseAddress, string token = null)
        {
            BaseAddress = baseAddress;
            Token = token;
        }
    }
}
