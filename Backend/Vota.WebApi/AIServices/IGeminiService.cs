using System.Threading.Tasks;

namespace Vota.WebApi.AIServices
{
    /// <summary>
    /// Gemini service interface.
    /// </summary>
    public interface IGeminiService
    {

        /// <summary>
        /// Send prompt.
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
        Task<string> SendPromptAsync(string prompt);
    }
}
