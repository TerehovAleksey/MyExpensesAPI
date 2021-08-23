using MyExpenses.ClientCore.Exceptions;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyExpenses.ClientCore.Helpers
{
    public class HttpHelper
    {
        private readonly string _baseUrl;
        private readonly string _token;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="baseUrl">Адрес WebApi (до контроллера, т.е., например, @"http://example.com/api/")</param>
        /// <param name="token">JWT токен, возможен NULL или EMPTY, если токен в запросах не требуется</param>
        public HttpHelper(IHttpInitData httpData)
        {
            if (httpData == null)
            {
                throw new ArgumentNullException(nameof(httpData));
            }
            _baseUrl = httpData.BaseAddress;
            _token = httpData.Token;
        }

        /// <summary>
        /// GET-запрос
        /// </summary>
        /// <typeparam name="TResult">Тип ответа</typeparam>
        /// <param name="controller">Контроллер веб-сервера</param>
        /// <returns></returns>
        public async Task<TResult> GetAsync<TResult>(string controller)
        {
            using var client = BaseClient();
            try
            {
                return await client.GetFromJsonAsync<TResult>(controller).ConfigureAwait(false);
            }
            #region Catch exception
            //когда статус код 200-209
            catch (HttpRequestException ex)
            {
                throw new MyExpensesException("httpError", ex);
            }
            catch (NotSupportedException ex)
            {
                //content type is not supported
                throw new MyExpensesException("content type is not supported", ex);
            }
            catch (JsonException ex)
            {
                //invalid JSON
                throw new MyExpensesException("invalid JSON", ex);
            }
            catch (Exception ex)
            {
                // other error
                throw new MyExpensesException("unknown error", ex);
            }
            #endregion
        }

        /// <summary>
        /// POST-запрос
        /// </summary>
        /// <typeparam name="TRequest">Тип запроса</typeparam>
        /// <typeparam name="TResult">Тип ответа</typeparam>
        /// <param name="controller">Контроллер веб-сервера</param>
        /// <param name="body">Тело запроса</param>
        /// <returns></returns>
        public async Task<TResult> PostAsync<TRequest, TResult>(string controller, TRequest body)
        {
            using var client = BaseClient();
            try
            {
                var http_response = await client.PostAsJsonAsync(controller, body).ConfigureAwait(false);
                http_response.EnsureSuccessStatusCode();
                var jsonStream = await http_response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                var result = await JsonSerializer.DeserializeAsync<TResult>(jsonStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).ConfigureAwait(false);
                return result;
            }
            #region Catch exception
            //когда статус код 200-209
            catch (HttpRequestException ex)
            {
                throw new MyExpensesException("httpError", ex);
            }
            catch (NotSupportedException ex)
            {
                //content type is not supported
                throw new MyExpensesException("content type is not supported", ex);
            }
            catch (JsonException ex)
            {
                //invalid JSON
                throw new MyExpensesException("invalid JSON", ex);
            }
            catch (Exception ex)
            {
                // other error
                throw new MyExpensesException("unknown error", ex);
            }
            #endregion
        }

        /// <summary>
        /// PUT-запрос
        /// </summary>
        /// <typeparam name="TRequest">Тип запроса</typeparam>
        /// <typeparam name="TResult">Тип ответа</typeparam>
        /// <param name="controller">Контроллер веб-сервера</param>
        /// <param name="body">Тело запроса</param>
        /// <returns></returns>
        public async Task<TResult> UpdateAsync<TRequest, TResult>(string controller, TRequest body)
        {
            using var client = BaseClient();
            try
            {
                var http_response = await client.PutAsJsonAsync(controller, body).ConfigureAwait(false);
                http_response.EnsureSuccessStatusCode();
                var json = await http_response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var jsonStream = await http_response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                var result = await JsonSerializer.DeserializeAsync<TResult>(jsonStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).ConfigureAwait(false);
                return result;
            }
            #region Catch exception
            //когда статус код 200-209
            catch (HttpRequestException ex)
            {
                throw new MyExpensesException("httpError", ex);
            }
            catch (NotSupportedException ex)
            {
                //content type is not supported
                throw new MyExpensesException("content type is not supported", ex);
            }
            catch (JsonException ex)
            {
                //invalid JSON
                throw new MyExpensesException("invalid JSON", ex);
            }
            catch (Exception ex)
            {
                // other error
                throw new MyExpensesException("unknown error", ex);
            }
            #endregion
        }

        /// <summary>
        /// DELETE-запрос
        /// </summary>
        /// <param name="controller">Контроллер веб-сервера</param>
        /// <returns></returns>
        public async Task<TResult> DeleteAsync<TResult>(string controller)
        {
            using var client = BaseClient();
            try
            {
                var http_response = await client.DeleteAsync(controller).ConfigureAwait(false);
                http_response.EnsureSuccessStatusCode();
                var jsonStream = await http_response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                var result = await JsonSerializer.DeserializeAsync<TResult>(jsonStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).ConfigureAwait(false);
                return result;
            }
            #region Catch exception
            //когда статус код 200-209
            catch (HttpRequestException ex)
            {
                throw new MyExpensesException("httpError", ex);
            }
            catch (NotSupportedException ex)
            {
                //content type is not supported
                throw new MyExpensesException("content type is not supported", ex);
            }
            catch (JsonException ex)
            {
                //invalid JSON
                throw new MyExpensesException("invalid JSON", ex);
            }
            catch (Exception ex)
            {
                // other error
                throw new MyExpensesException("unknown error", ex);
            }
            #endregion
        }

        private HttpClient BaseClient()
        {
            // создаём http-клиент
            var client = new HttpClient { BaseAddress = new Uri(_baseUrl) };

            // добавляем токен в заголовок
            client.AddTokenHeader(_token);

            return client;
        }
    }
}
