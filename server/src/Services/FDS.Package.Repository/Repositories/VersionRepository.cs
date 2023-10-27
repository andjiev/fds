using System;
using System.Data;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using FDS.Common.Infrastructure;
using FDS.Package.Domain.Repositories;

namespace FDS.Package.Repository.Repositories
{
	public class VersionRepository : BaseDapperRepository, IVersionRepository
	{
        private readonly IMapper mapper;

        public VersionRepository(IDbConnection dbConnection, IMapper mapper)
            : base(dbConnection)
        {
            this.mapper = mapper;
        }

        public async Task CreateVersionAsync(int packageId, string versionNumber)
        {
            string query = @"
                        INSERT INTO Version
                        (Name, CreatedOn, PackageId)
                        VALUES
                        (@VersionNumber, getdate(), @PackageId)";

            await dbConnection.ExecuteAsync(query, new
            {
                PackageId = packageId,
                VersionNumber = versionNumber
            });
        }
    }
}

