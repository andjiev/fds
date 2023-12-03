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

        public async Task UpdatePackageVersionAsync(int packageId, string packageVersion)
        {
            string query = @"
                        UPDATE Package
                        SET 
                            Status = @Status,
                            CurrentVersion = @Version,
                            UpdatedOn = getdate()
                        WHERE Package.Id = @PackageId";

            await dbConnection.ExecuteAsync(query, new
            {
                PackageId = packageId,
                Version = packageVersion,
                Status = PackageStatus.UpToDate
            });
        }

        public async Task<int> InsertPackageAsync(Entities.Package package)
        {
            var sql = @"
                    INSERT INTO [Package]
                    (Name, CurrentVersion, LatestVersion, Status, Score, Url, Description, Type)
                    VALUES
                    (@Name, @CurrentVersion, @LatestVersion, @Status, @Score, @Url, @Description, @Type);
                    SELECT CAST(SCOPE_IDENTITY() as int)";

            return await dbConnection.QuerySingleAsync<int>(sql, new
            {
                Name = package.Name,
                CurrentVersion = package.CurrentVersion,
                LatestVersion = package.LatestVersion,
                Status = package.Status,
                Score = package.Score,
                Url = package.Url,
                Description = package.Description,
                Type = package.Type
            });
        }

        public async Task InsertPackagesAsync(List<Entities.Package> packages)
        {
            await dbConnection.ExecuteAsync("DELETE FROM Package");

            var sql = @"
                    INSERT INTO [Package]
                    (Name, CurrentVersion, LatestVersion, Status, Score, Url, Description, Type)
                    VALUES
                    (@Name, @CurrentVersion, @LatestVersion, @Status, @Score, @Url, @Description, @Type)";

            foreach (var package in packages)
            {
                await dbConnection.ExecuteAsync(sql, new
                {
                    Name = package.Name,
                    CurrentVersion = package.CurrentVersion,
                    LatestVersion = package.LatestVersion,
                    Status = package.Status,
                    Score = package.Score,
                    Url = package.Url,
                    Description = package.Description,
                    Type = package.Type
                });
            }
        }

        public async Task DeletePackageAsync(int id)
        {
            string query = @"
                        DELETE
                        FROM Package
                        WHERE Package.Id = @Id";

            await dbConnection.ExecuteAsync(query, new
            {
                Id = id
            });
        }

        public async Task ResetStatusAsync(int packageId)
        {
            string query = @"
                        UPDATE Package
                        SET Status = @Status
                        WHERE Package.Id = @PackageId";

            await dbConnection.ExecuteAsync(query, new
            {
                PackageId = packageId,
                Status = PackageStatus.UpdateNeeded
            });
        }
    }
}
