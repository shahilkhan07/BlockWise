using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using Vota.WebApi.AIServices;
using Vota.WebApi.Common;
using Vota.WebApi.Models.Blockly;

namespace Vota.WebApi.Controllers
{

    /// <summary>
    /// Blockly controller.
    /// </summary>
    [Route("/v{version:ApiVersion}/[controller]")]
    [ApiController]
    public class BlocklyController : ApiControllerBase
    {

        private readonly IGeminiService _geminiService;

        /// <summary>
        /// Blocklhy controller constructor.
        /// </summary>
        /// <param name="geminiService"></param>
        public BlocklyController(IGeminiService geminiService)
        {
            _geminiService = geminiService;
        }

        /// <summary>
        /// Generate blocks.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("generate-blocks")]
        public async Task<IActionResult> GenerateBlocks([FromBody] PromptRequestViewModel request)
        {
            if (string.IsNullOrWhiteSpace(request.Text))
            {
                return BadRequest("Prompt text is required.");
            }

            try
            {
                string resultXml = await _geminiService.SendPromptAsync(request.Text);
                return Ok(new { xml = resultXml });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to generate Blockly XML.", details = ex.Message });
            }
        }
    }
}
