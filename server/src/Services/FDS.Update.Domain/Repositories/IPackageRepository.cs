namespace FDS.Update.Domain.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities = FDS.Common.Entities;

    public interface IPackageRepository
    {
        Task UpdatePackageVersionAsync(int packageId, int versionId);

        Task InsertPackagesAsync(List<Entities.Package> packages);
    }
}
