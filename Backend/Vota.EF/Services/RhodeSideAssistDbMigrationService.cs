using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Vota.EF.Services
{
    /// <summary>
    /// Database migration service.
    /// </summary>
    public class RhodeSideAssistDbMigrationService : IDbMigrationService
    {
        private readonly IConfigurationRoot _configurationRoot;
        private readonly VotaDbContext _context;

        /// <summary>
        /// Creates service.
        /// </summary>
        /// <param name="configuration">Configuration root.</param>
        /// <param name="context">Database context.</param>
        public RhodeSideAssistDbMigrationService(
            IConfiguration configuration,
            VotaDbContext context)
        {
            _configurationRoot = (IConfigurationRoot)configuration;
            _context = context;
        }

        /// <summary>
        /// Migrates database.
        /// </summary>
        /// <returns>Task.</returns>
        public async Task MigrateAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();

                _configurationRoot.Reload();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }
    }
}
