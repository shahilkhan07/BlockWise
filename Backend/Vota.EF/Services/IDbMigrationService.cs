using System.Threading.Tasks;

namespace Vota.EF.Services
{
    /// <summary>
    /// Database migration service.
    /// </summary>
    public interface IDbMigrationService
    {
        /// <summary>
        /// Migrates database.
        /// </summary>
        /// <returns>Task.</returns>
        Task MigrateAsync();
    }
}
