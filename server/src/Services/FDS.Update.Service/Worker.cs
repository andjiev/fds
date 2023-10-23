namespace FDS.Update.Service
{
    using MassTransit;
    using Microsoft.Extensions.Hosting;
    using System.Threading;
    using System.Threading.Tasks;

    public class Worker : BackgroundService, IHostedService
    {
        private readonly IBusControl _busControl;

        public Worker(IBusControl busControl)
        {
            _busControl = busControl;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return _busControl.StartAsync();
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return _busControl.StopAsync(cancellationToken);
        }
    }
}
