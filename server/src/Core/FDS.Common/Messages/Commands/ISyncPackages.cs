namespace FDS.Common.Messages.Commands
{
    using MassTransit;

    public interface ISyncPackages : CorrelatedBy<string>
    {
    }
}
