namespace FDS.Common.Messages.Commands
{
    using MassTransit;

    public interface IPackageDeleted : CorrelatedBy<string>
    {
        int PackageId { get; }

        string PackageName { get; }
    }
}
