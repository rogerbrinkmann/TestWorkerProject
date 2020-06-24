using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TestWorkerProject
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private HttpClient client;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken) 
        {
            client = new HttpClient();
            return base.StartAsync(cancellationToken);
        }
        
        public override Task StopAsync(CancellationToken cancellationToken) 
        {
            client.Dispose();
            _logger.LogInformation("The service has been stopped successfully");
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = await client.GetAsync("https://www.google.de");
                if(result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("The website is up. Status Code {StatusCode}", result.StatusCode);
                }
                else
                {
                    _logger.LogError("The website is down. {StatusCode}", result.StatusCode);
                }
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
