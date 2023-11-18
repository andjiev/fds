namespace FDS.Package.Domain.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPackageRepository
    {
        Task<List<Entities.Package>> GetAsync();

        Task<Entities.Package> GetPackageAsync(int packageId);

        Task UpdatePackageAsync(Entities.Package package);

        Task InsertPackagesAsync(List<Entities.Package> packages);
    }
}
