namespace FDS.Package.Domain.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities = FDS.Common.Entities;

    public interface IPackageRepository
    {
        Task<List<Entities.Package>> GetAsync(List<int> ids = null);

        Task<Entities.Package> GetPackageAsync(int packageId);

        Task UpdatePackageAsync(Entities.Package package);
    }
}
