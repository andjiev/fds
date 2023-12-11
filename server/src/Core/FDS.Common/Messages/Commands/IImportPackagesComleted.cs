namespace FDS.Common.Messages.Commands
{
    using MassTransit;

    public interface IImportPackagesCompleted : CorrelatedBy<string>
    {
    }
}
