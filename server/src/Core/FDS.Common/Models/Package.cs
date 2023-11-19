namespace FDS.Common.Models
{
    using System;
    using FDS.Common.DataContext.Enums;

    public class Package
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

        public PackageType Type { get; set; }
    }
}
