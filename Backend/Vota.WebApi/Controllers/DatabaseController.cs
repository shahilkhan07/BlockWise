using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vota.EF;
using System;
using System.Threading.Tasks;

namespace Vota.WebApi.Controllers
{
    /// <summary>
    /// Database controller.
    /// </summary>
    [ApiController]
    [Route("/v{version:ApiVersion}/[controller]")]
    public class DatabaseController : Controller
    {
        private readonly VotaDbContext _rhodeSideAssistDbContext;

        /// <summary>
        /// Creates database controller
        /// </summary>
        /// <param name="rhodeSideAssistDbContext">rhodeSideAssistDbContext</param>
        public DatabaseController(VotaDbContext rhodeSideAssistDbContext)
        {
            _rhodeSideAssistDbContext = rhodeSideAssistDbContext;
        }

        /// <summary>
        /// create or update all databases
        /// </summary>
        /// <returns></returns>
        [HttpPost("UpdateDatabase")]
        public async Task<IActionResult> UpdateDatabaseAsync()
        {
            try
            {
                await _rhodeSideAssistDbContext.Database.MigrateAsync();
                return Ok("Database successfully updated.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
