using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Vota.WebApi.Models.Gemini;

namespace Vota.WebApi.AIServices
{

    /// <summary>
    /// Gemini service.
    /// </summary>
    public class GeminiService : IGeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Gemini service constructor.
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="configuration"></param>
        public GeminiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        /// <summary>
        /// Send prompt to gemini.
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
        public async Task<string> SendPromptAsync(string prompt)
        {
            string apiKey = _configuration["Gemini:ApiKey"];
            string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}";

            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = $"Convert this instruction to Blockly XML only: {prompt}" }
                        }
                    }
                }
            };

            var requestContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, requestContent);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<GeminiResponse>(responseString);

            return CleanXml(result?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text);
        }
        private string CleanXml(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
                return "";

            return xml
                .Replace("```xml", "")
                .Replace("```", "")
                .Trim();
        }

    }
}