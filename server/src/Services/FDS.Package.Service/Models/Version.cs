namespace FDS.Package.Service.Models
{
    public class Version
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class CreateVersion
    {
        public int PackageId { get; set; }

        public string VersionNumber { get; set; }
    }
}
