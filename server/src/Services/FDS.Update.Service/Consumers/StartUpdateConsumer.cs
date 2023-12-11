namespace FDS.Update.Service.Consumers
{
    using FDS.Common.Messages.Commands;
    using FDS.Update.Domain.Repositories;
    using MassTransit;
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    public class StartUpdateConsumer : IConsumer<IStartUpdate>
    {
        private readonly IPackageRepository repository;

        public StartUpdateConsumer(IPackageRepository repository)
        {
            this.repository = repository;
        }

        public async Task Consume(ConsumeContext<IStartUpdate> context)
        {
            try
            {
                var process = new Process();
                process.StartInfo.WorkingDirectory = "../appdata/";
                process.StartInfo.FileName = "/usr/local/bin/npm";
                process.StartInfo.Arguments = "install " + context.Message.PackageName + "@latest";
                process.Start();
                await process.WaitForExitAsync();

                await repository.UpdatePackageVersionAsync(context.Message.PackageId, context.Message.PackageVersion);
                await context.Publish<IPackageUpdated>(new
                {
                    context.Message.CorrelationId,
                    context.Message.PackageId
                });                
            }
            catch (Exception ex)
            {
                await repository.ResetStatusAsync(context.Message.PackageId);
                throw new Exception("Error occured while updating package" + ex.Message);
            }
        }
    }
}
