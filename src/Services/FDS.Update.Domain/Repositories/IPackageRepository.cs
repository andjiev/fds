namespace FDS.Update.Domain.Repositories
{
    using System.Threading.Tasks;

    public interface IPackageRepository
    {
        Task UpdatePackageVersionAsync(int packageId, int versionId);
    }
}
