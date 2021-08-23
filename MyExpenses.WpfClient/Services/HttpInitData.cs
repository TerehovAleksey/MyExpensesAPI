using MyExpenses.ClientCore.Helpers;

namespace MyExpenses.WpfClient.Services
{
    public class HttpInitData : IHttpInitData
    {
        public string BaseAddress { get; }

        public string Token { get; }

        public HttpInitData(ISettingsService settingsService)
        {
            BaseAddress = settingsService.Current.WebApiUrl;
            Token = settingsService.Current.LoginResult.AccessToken;
        }
    }
}
