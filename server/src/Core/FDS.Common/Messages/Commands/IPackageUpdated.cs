namespace FDS.Common.Messages.Commands
{
    using MassTransit;

    public interface IPackageUpdated : CorrelatedBy<string>
    {
        int PackageId { get; }
    }
}
