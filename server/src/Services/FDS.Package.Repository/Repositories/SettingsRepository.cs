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
    using Entities = FDS.Common.Entities;
    using Enums = FDS.Common.DataContext.Enums;
    using FDS.Common.Entities;

    public class SettingsRepository : BaseDapperRepository, ISettingsRepository
    {
        private readonly IMapper mapper;

        public SettingsRepository(IDbConnection dbConnection, IMapper mapper)
            : base(dbConnection)
        {
            this.mapper = mapper;
        }

        public async Task<Entities.Settings> GetAsync()
        {
            string settingsQuery = @"
                        SELECT
                            Id,
                            State
                        FROM Settings";

            return await dbConnection.QueryFirstAsync<Entities.Settings>(settingsQuery);
        }

        public async Task UpdateImportState(ImportState state)
        {
            string query = @"
                        UPDATE Settings
                        SET State = @State";

            await dbConnection.ExecuteAsync(query, new
            {
                State = state
            });
        }
    }
}
