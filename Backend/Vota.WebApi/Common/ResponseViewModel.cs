using System.Net;

namespace Vota.WebApi.Common
{
    /// <summary>
    /// Response view model
    /// </summary>
    public class ResponseViewModel
    {
        /// <summary>
        /// Status
        /// </summary>
        public HttpStatusCode Code { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        public dynamic Data { get; set; }

        /// <summary>
        /// Details
        /// </summary>
        public object Details { get; set; }
    }
}
