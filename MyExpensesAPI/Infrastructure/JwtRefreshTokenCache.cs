using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyExpensesAPI.Infrastructure
{
    public class JwtRefreshTokenCache : IHostedService, IDisposable
    {
        private readonly IJwtAuthManager _jwtAuthManager;
        private Timer _timer;

        public JwtRefreshTokenCache(IJwtAuthManager jwtAuthManager)
        {
            _jwtAuthManager = jwtAuthManager;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            // удаляем истёкшие токены каждую минуту
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _jwtAuthManager.RemoveExpiredRefreshTokens(DateTime.Now);
        }
    }
}
