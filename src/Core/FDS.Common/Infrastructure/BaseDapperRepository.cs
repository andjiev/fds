namespace FDS.Common.Infrastructure
{
    using System.Data;

    public abstract class BaseDapperRepository
    {
        protected readonly IDbConnection dbConnection;

        public BaseDapperRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }
    }
}
