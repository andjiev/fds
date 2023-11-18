namespace FDS.Common.Messages.Commands
{
    using MassTransit;

    public interface IStartUpdate : CorrelatedBy<string>
    {
        string PackageName { get; }
    }
}
