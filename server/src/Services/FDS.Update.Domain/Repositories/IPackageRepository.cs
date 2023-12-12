namespace FDS.Update.Domain.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities = FDS.Common.Entities;

    public interface IPackageRepository
    {
        Task UpdatePackageVersionAsync(int packageId, string packageVersion);

        Task<int> InsertPackageAsync(Entities.Package package);

        Task InsertPackagesAsync(List<Entities.Package> packages);

        Task DeletePackageAsync(int id);

        Task ResetStatusAsync(int packageId);
    }
}
