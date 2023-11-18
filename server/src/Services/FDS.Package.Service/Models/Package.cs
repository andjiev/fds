namespace FDS.Package.Service.Models
{
    using FDS.Common.DataContext.Enums;

    public class Package
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CurrentVersion { get; set; }

        public string LatestVersion { get; set; }

        public PackageStatus Status { get; set; }
    }

    public class UpdatePackage
    {
        public int VersionId { get; set; }
    }
}
