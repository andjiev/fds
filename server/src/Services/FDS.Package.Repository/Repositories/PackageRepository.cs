namespace FDS.Package.Repository.Repositories
{
    using AutoMapper;
    using Dapper;
    using FDS.Common.Infrastructure;
    using FDS.Package.Domain.DbResults;
    using FDS.Package.Domain.Repositories;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;
    using FDS.Common.DataContext.Enums;
    using System;
    using System.Linq;
    using Entities = FDS.Common.Entities;

    public class PackageRepository : BaseDapperRepository, IPackageRepository
    {
        private readonly IMapper mapper;

        public PackageRepository(IDbConnection dbConnection, IMapper mapper)
            : base(dbConnection)
        {
            this.mapper = mapper;
        }

        public async Task<List<Entities.Package>> GetAsync()
        {
            string packageQuery = @"
                        SELECT
                            Package.Id AS Id,
                            Package.Name AS Name,
                            Package.CurrentVersion AS CurrentVersion,
                            Package.LatestVersion AS LatestVersion,
                            Package.Score as Score,
                            Package.Url as Url,
                            Package.Description as Description,
                            Package.UpdatedOn AS UpdatedOn,
                            Package.Status AS Status
                        FROM Package";

            var result = (await dbConnection.QueryAsync<PackageDbResult>(packageQuery)).AsList();
            return mapper.Map<List<Entities.Package>>(result);
        }

        public async Task<Entities.Package> GetPackageAsync(int packageId)
        {
            string query = @"
                        SELECT
                            Package.Id AS Id,
                            Package.Name AS Name,
                            Package.CurrentVersion AS CurrentVersion,
                            Package.LatestVersion AS LatestVersion,
                            Package.Score as Score,
                            Package.Url as Url,
                            Package.Description as Description,
                            Package.UpdatedOn AS UpdatedOn,
                            Package.Status AS Status
                        FROM Package
                        WHERE Package.Id = @Id";

            var package = await dbConnection.QueryFirstOrDefaultAsync<PackageDbResult>(query, new
            {
                Id = packageId
            });

            return mapper.Map<Entities.Package>(package);
        }

        public async Task UpdatePackageAsync(Entities.Package package)
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
    }
}
