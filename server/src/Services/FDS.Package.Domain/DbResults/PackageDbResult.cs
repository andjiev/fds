namespace FDS.Package.Domain.DbResults
{
    using FDS.Common.DataContext.Enums;
    using System;

    public class PackageDbResult
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CurrentVersion { get; set; }

        public string LatestVersion { get; set; }

        public DateTime CreatedOn { get; set; }

        public PackageStatus Status { get; set; }
    }
}
