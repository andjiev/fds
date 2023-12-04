namespace FDS.Update.Service.Consumers
{
    using FDS.Common.Messages.Commands;
    using FDS.Update.Domain.Repositories;
    using MassTransit;
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    public class StartDeleteConsumer : IConsumer<IStartDelete>
    {
        private readonly IPackageRepository repository;

        public StartDeleteConsumer(IPackageRepository repository)
        {
            this.repository = repository;
        }

        public async Task Consume(ConsumeContext<IStartDelete> context)
        {
            try
            {
                var process = new Process();
                process.StartInfo.WorkingDirectory = "../../../../";
                process.StartInfo.FileName = "/usr/local/bin/npm";
                process.StartInfo.Arguments = "uninstall " + context.Message.PackageName;
                process.StartInfo.UseShellExecute = true;
                process.Start();
                await process.WaitForExitAsync();

                await repository.DeletePackageAsync(context.Message.PackageId);
                await context.Publish<IPackageDeleted>(new
                {
                    context.Message.CorrelationId,
                    context.Message.PackageId,
                    context.Message.PackageName
                });                
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while deleting package" + ex.Message);
            }
        }
    }
}
