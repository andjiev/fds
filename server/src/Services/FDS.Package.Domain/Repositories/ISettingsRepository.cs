namespace FDS.Package.Domain.Repositories
{
    using System.Threading.Tasks;
    using Entities = FDS.Common.Entities;
    using Enums = FDS.Common.DataContext.Enums;

    public interface ISettingsRepository
    {
        Task<Entities.Settings> GetAsync();

        Task UpdateImportState(Enums.ImportState state);
    }
}
