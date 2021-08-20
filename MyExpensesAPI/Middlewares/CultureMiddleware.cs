using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MyExpensesAPI.Middlewares
{
    public class CultureMiddleware
    {
        private readonly ILogger<CultureMiddleware> _logger;
        private readonly RequestDelegate _next;

        public CultureMiddleware(RequestDelegate next, ILogger<CultureMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

            var langHeader = httpContext.Request.Headers["Content-Language"];
            if (StringValues.IsNullOrEmpty(langHeader))
            {
                try
                {
                    CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = new CultureInfo("ru-RU");
                }
                catch (CultureNotFoundException ex)
                {
                    _logger.LogError(ex.StackTrace);
                }
            }
            else
            {
                var lang = langHeader.First().Trim().Split(',').First();
                try
                {
                    CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = new CultureInfo(lang);
                }
                catch (CultureNotFoundException ex)
                {
                    _logger.LogError(ex.StackTrace);
                }
            }

            await _next.Invoke(httpContext).ConfigureAwait(false);
        }
    }
}
