namespace FDS.Package.Domain.Entities
{
    using FDS.Common.DataContext;
    using FDS.Common.DataContext.Enums;

    public class Package : Entity
    {
        protected Package(int id) : base(id) { }

        public Package(int id, string name, Version version, Version versionUpdate, PackageStatus status)
            : base(id)
        {
            Name = name;
            Version = version;
            VersionUpdate = versionUpdate;
            Status = status;
        }

        public string Name { get; private set; }

        public Version Version { get; private set; }

        public Version VersionUpdate { get; private set; }

        public PackageStatus Status { get; private set; }

        public void UpdateStatus(PackageStatus status)
        {
            Status = status;
        }
    }
}
