namespace HospitalTrackerApi.Services
{
    public class TimedHostedService : BackgroundService
    {
        private readonly ILogger<TimedHostedService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public TimedHostedService(ILogger<TimedHostedService> logger, IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            return base.StartAsync(stoppingToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var trackerService = scope.ServiceProvider.GetRequiredService<ITrackerService>();

            var timeout = TimeSpan.FromSeconds(3);
            while (!stoppingToken.IsCancellationRequested)
            {
                await trackerService.ChangePositions();
                await Task.Delay(timeout);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service stoping.");

            return base.StopAsync(cancellationToken);
        }
    }
}
