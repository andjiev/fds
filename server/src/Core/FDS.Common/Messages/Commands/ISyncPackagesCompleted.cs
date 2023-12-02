namespace FDS.Common.Messages.Commands
{
    using System.Collections.Generic;
    using MassTransit;

    public interface ISyncPackagesCompleted : CorrelatedBy<string>
    {
    }
}
