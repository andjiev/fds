namespace FDS.Package.Domain.DbResults
{
    using FDS.Common.DataContext.Enums;
    using System;

    public class PackageDbResult
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int VersionId { get; set; }

        public string VersionName { get; set; }

        public DateTime VersionCreatedOn { get; set; }

        public VersionDbResult VersionUpdate { get; set; }

        public PackageStatus Status { get; set; }
    }
}
