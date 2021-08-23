using System;
using System.Net.Http;

namespace MyExpenses.ClientCore.Helpers
{
    /// <summary>
    /// Класс расширений для <see cref="HttpClient"/>
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// добавляет заголовок с токеном к HTTP-запросу
        /// </summary>
        /// <param name="client"></param>
        /// <param name="token"></param>
        public static void AddTokenHeader(this HttpClient client, string token)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
