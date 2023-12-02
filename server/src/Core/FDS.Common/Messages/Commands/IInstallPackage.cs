namespace FDS.Common.Messages.Commands
{
    using FDS.Common.DataContext.Enums;
    using MassTransit;

    public interface IInstallPackage : CorrelatedBy<string>
    {
        public string Name { get; }

        public string Description { get; }

        public string Version { get; }

        public PackageType Type { get; }
    }
}
