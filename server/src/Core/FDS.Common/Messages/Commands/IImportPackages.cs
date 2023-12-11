namespace FDS.Common.Messages.Commands
{
    using MassTransit;

    public interface IImportPackages : CorrelatedBy<string>
    {
    }
}
