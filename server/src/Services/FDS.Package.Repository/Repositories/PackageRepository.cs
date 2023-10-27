namespace FDS.Package.Repository.Repositories
{
    using AutoMapper;
    using Dapper;
    using FDS.Common.Infrastructure;
    using FDS.Package.Domain.Entities;
    using FDS.Package.Domain.DbResults;
    using FDS.Package.Domain.Repositories;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;
    using FDS.Common.DataContext.Enums;

    public class PackageRepository : BaseDapperRepository, IPackageRepository
    {
        private readonly IMapper mapper;

        public PackageRepository(IDbConnection dbConnection, IMapper mapper)
            : base(dbConnection)
        {
            this.mapper = mapper;
        }

        public async Task<List<Package>> GetAsync()
        {
            string packageQuery = @"
                        SELECT
                            Package.Id AS Id,
                            Package.Name AS Name,
                            Package.Status AS Status,
                            Version.Id As VersionId,
                            Version.Name AS VersionName,
                            Version.CreatedOn AS VersionCreatedOn
                        FROM Package
                        JOIN Version
                            ON Package.VersionId = Version.Id";

            string versionUpdateQuery = @"
                                SELECT TOP 1
                                    Version.Id as Id,
									Version.Name as Name
                                FROM Version
								WHERE Version.Id != @VersionId AND Version.CreatedOn > @VersionCreatedOn AND Version.PackageId = @PackageId
								GROUP BY Version.Id, Version.Name";

            var result = (await dbConnection.QueryAsync<PackageDbResult>(packageQuery)).AsList();

            foreach (var package in result)
            {
                if (package.Status == PackageStatus.Updating)
                {
                    continue;
                }

                var versionResult = await dbConnection.QueryFirstOrDefaultAsync<VersionDbResult>(versionUpdateQuery, new
                {
                    VersionId = package.VersionId,
                    VersionCreatedOn = package.VersionCreatedOn,
                    PackageId = package.Id
                });

                package.VersionUpdate = versionResult;
            }

            return mapper.Map<List<Package>>(result);
        }

        public async Task<Package> GetPackageAsync(int packageId, bool includeVersionUpdate = true, bool latestVersionUpdate = false)
        {
            string query = @"
                        SELECT
                            Package.Id AS Id,
                            Package.Name AS Name,
                            Package.Status AS Status,
                            Version.Id As VersionId,
                            Version.Name AS VersionName,
                            Version.CreatedOn As VersionCreatedOn
                        FROM Package
                        JOIN Version
                            ON Package.VersionId = Version.Id
                        WHERE Package.Id = @Id";

            string nextVersionUpdateQuery = @"
                                SELECT TOP 1
                                    Version.Id as Id,
									Version.Name as Name
                                FROM Version
								WHERE Version.Id != @VersionId AND Version.CreatedOn > @VersionCreatedOn AND Version.PackageId = @PackageId
								GROUP BY Version.Id, Version.Name";

            string latestVersionUpdateQuery = @"
                                SELECT TOP 1
                                    Version.Id as Id,
                                    Version.Name as Name
                                FROM Version
                                WHERE Version.PackageId = @PackageId
                                ORDER BY CreatedOn desc";

            string versionUpdateQuery = latestVersionUpdate ? latestVersionUpdateQuery : nextVersionUpdateQuery;

            var package = await dbConnection.QueryFirstOrDefaultAsync<PackageDbResult>(query, new
            {
                Id = packageId
            });

            if (includeVersionUpdate && package.Status != PackageStatus.Updating)
            {
                var versionResult = await dbConnection.QueryFirstOrDefaultAsync<VersionDbResult>(versionUpdateQuery, new
                {
                    VersionId = package.VersionId,
                    VersionCreatedOn = package.VersionCreatedOn,
                    PackageId = package.Id
                });

                package.VersionUpdate = versionResult;
            }

            return mapper.Map<Package>(package);
        }

        public async Task UpdatePackageAsync(Package package)
        {
            string query = @"
                        UPDATE Package
                        SET Status = @Status
                        WHERE Package.Id = @Id";

            await dbConnection.ExecuteAsync(query, new
            {
                Id = package.Id,
                Status = package.Status
            });
        }

        public async Task UpdatePackagesToInitialStateAsync()
        {
            string query = @"
                        UPDATE Package
                        SET Package.VersionId = @VersionId, Package.VersionName = @VersionName, Status = 1
                        WHERE Package.Id = @PackageId";

            await dbConnection.ExecuteAsync(query, new List<object>
            {
                new { PackageId = 1, VersionId = 2, VersionName = "v.1.2-beta" },
                new { PackageId = 2, VersionId = 7, VersionName = "v.3.0" },
                new { PackageId = 3, VersionId = 9, VersionName = "v.1.0" },
                new { PackageId = 4, VersionId = 13, VersionName = "v.2.0" },
                new { PackageId = 5, VersionId = 16, VersionName = "v.1.1-beta" }
            });
        }
    }
}
