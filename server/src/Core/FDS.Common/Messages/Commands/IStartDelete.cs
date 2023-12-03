namespace FDS.Common.Messages.Commands
{
    using MassTransit;

    public interface IStartDelete : CorrelatedBy<string>
    {
        int PackageId { get; }
        
        string PackageName { get; }
    }
}
