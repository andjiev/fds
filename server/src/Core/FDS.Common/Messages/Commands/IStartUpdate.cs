namespace FDS.Common.Messages.Commands
{
    using MassTransit;

    public interface IStartUpdate : CorrelatedBy<string>
    {
        int PackageId { get; }
        string PackageName { get; }
        string PackageVersion { get; }
    }
}
