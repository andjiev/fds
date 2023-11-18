namespace FDS.Update.Service.Consumers
{
    using FDS.Common.Messages.Commands;
    using FDS.Update.Domain.Repositories;
    using MassTransit;
    using System;
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

                // await repository.UpdatePackageVersionAsync(context.Message.PackageName, context.Message.VersionId);

                // await context.Publish<IPackageUpdated>(new
                // {
                //     context.Message.CorrelationId,
                //     context.Message.PackageId
                // });
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while updating package");
            }
        }
    }
}
