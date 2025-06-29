using Vota.EF;
using Vota.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Vota.WebApi.Configuration
{
    /// <summary>
    /// Database configuration provider.
    /// </summary>
    public class DatabaseConfigurationProvider : ConfigurationProvider
    {
        private readonly Action<DbContextOptionsBuilder<VotaDbContext>> _options;

        /// <summary>
        /// Creates database configuration provider.
        /// </summary>
        /// <param name="options">Database context options.</param>
        public DatabaseConfigurationProvider(
            Action<DbContextOptionsBuilder<VotaDbContext>> options)
        {
            _options = options;
        }
    }
}
