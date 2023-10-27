namespace FDS.Package.Domain.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IVersionRepository
    {
        Task CreateVersionAsync(int packageId, string versionNumber);
    }
}
