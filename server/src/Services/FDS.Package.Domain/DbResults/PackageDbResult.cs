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

        public int? Score { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public PackageStatus Status { get; set; }
    }
}
