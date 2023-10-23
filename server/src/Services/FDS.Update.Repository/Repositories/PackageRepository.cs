namespace FDS.Update.Repository.Repositories
{
    using Dapper;
    using FDS.Common.DataContext.Enums;
    using FDS.Common.Infrastructure;
    using FDS.Update.Domain.Repositories;
    using System.Data;
    using System.Threading.Tasks;

    public class PackageRepository : BaseDapperRepository, IPackageRepository
    {
        public PackageRepository(IDbConnection dbConnection)
            : base(dbConnection)
        {
        }

        public async Task UpdatePackageVersionAsync(int packageId, int versionId)
        {
            string query = @"
                        UPDATE Package
                        SET VersionId = @VersionId,
                            VersionName = (SELECT Name FROM Version WHERE Id = @VersionId),
                            Status = @Status
                        WHERE Package.Id = @PackageId";

            await dbConnection.ExecuteAsync(query, new
            {
                PackageId = packageId,
                VersionId = versionId,
                Status = PackageStatus.Updated
            });
        }
    }
}
