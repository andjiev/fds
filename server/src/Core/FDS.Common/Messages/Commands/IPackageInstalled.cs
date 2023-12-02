namespace FDS.Common.Messages.Commands
{
    using MassTransit;

    public interface IPackageInstalled : CorrelatedBy<string>
    {
        int PackageId { get; }
    }
}
