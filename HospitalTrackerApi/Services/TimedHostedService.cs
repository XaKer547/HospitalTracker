namespace HospitalTrackerApi.Services
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<TimedHostedService> _logger;
        private Timer _timer = null!;

        private readonly IServiceProvider _serviceProvider;
        public TimedHostedService(ILogger<TimedHostedService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(3));

            return Task.CompletedTask;
        }

        private async void DoWork(object? state)
        {
            var count = Interlocked.Increment(ref executionCount);

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                var trackerService = scope.ServiceProvider.GetRequiredService<ITrackerService>();

                await trackerService.ChangePositions();
            }

            _logger.LogInformation("Timed Hosted Service is working. {currentdate} PeopleMoved: {Count} times", DateTime.Now.ToString(), count);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}