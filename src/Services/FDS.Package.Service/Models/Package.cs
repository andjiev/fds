namespace FDS.Package.Service.Models
{
    using FDS.Common.DataContext.Enums;

    public class Package
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Version Version { get; set; }

        public Version VersionUpdate { get; set; }

        public PackageStatus Status { get; set; }
    }
}
