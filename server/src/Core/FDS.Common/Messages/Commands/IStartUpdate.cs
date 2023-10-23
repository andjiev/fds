namespace FDS.Common.Messages.Commands
{
    using MassTransit;

    public interface IStartUpdate : CorrelatedBy<string>
    {
        int PackageId { get; }
        
        int VersionId { get; }
    }
}
