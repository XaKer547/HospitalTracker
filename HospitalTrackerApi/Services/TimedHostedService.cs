namespace HospitalTrackerApi.Services
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<TimedHostedService> _logger;
        private Timer _timer = null!;

        private readonly ITrackerService _trackerService;
        public TimedHostedService(ILogger<TimedHostedService> logger, ITrackerService trackerService)
        {
            _logger = logger;
            _trackerService = trackerService;
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

            await _trackerService.ChangePositions();

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