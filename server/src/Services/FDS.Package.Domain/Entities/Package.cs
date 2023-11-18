namespace FDS.Package.Domain.Entities
{
    using System;
    using FDS.Common.DataContext;
    using FDS.Common.DataContext.Enums;

    public class Package : Entity
    {
        protected Package(int id) : base(id) { }

        public Package(int id, string name, string currentVersion, string latestVersion, DateTime createdOn, PackageStatus status)
            : base(id)
        {
            Name = name;
            CurrentVersion = currentVersion;
            LatestVersion = latestVersion;
            CreatedOn = createdOn;
            Status = status;
        }

        public string Name { get; private set; }

        public string CurrentVersion {get; private set;}

        public string LatestVersion { get; private set; }

        public DateTime CreatedOn { get; private set; }

        public PackageStatus Status { get; private set; }

        public void UpdateStatus(PackageStatus status)
        {
            Status = status;
        }
    }
}
