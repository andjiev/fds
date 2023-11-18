namespace FDS.Update.Repository.Repositories
{
    using Dapper;
    using FDS.Common.DataContext.Enums;
    using FDS.Common.Infrastructure;
    using FDS.Update.Domain.Repositories;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;
    using Entities = FDS.Common.Entities;

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
                Status = PackageStatus.UpToDate
            });
        }
        
        public async Task InsertPackagesAsync(List<Entities.Package> packages)
        {
            await dbConnection.ExecuteAsync("DELETE FROM Package");

            var sql = @"
                    INSERT INTO [Package]
                    (Name, CurrentVersion, LatestVersion, Status, CreatedOn)
                    VALUES
                    (@Name, @CurrentVersion, @LatestVersion, @Status, getdate())";

            foreach (var package in packages)
            {
                await dbConnection.ExecuteAsync(sql, new
                {
                    Name = package.Name,
                    CurrentVersion = package.CurrentVersion,
                    LatestVersion = package.LatestVersion,
                    Status = package.Status
                });
            }
        }
    }
}
