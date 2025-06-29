using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Vota.WebApi.Models.Gemini
{

    /// <summary>
    /// Gemini response.
    /// </summary>
    public class GeminiResponse
    {

        /// <summary>
        /// Candidates.
        /// </summary>
        [JsonPropertyName("candidates")]
        public List<Candidate> Candidates { get; set; }
    }

    /// <summary>
    /// Candidate.
    /// </summary>
    public class Candidate
    {

        /// <summary>
        /// Content.
        /// </summary>
        [JsonPropertyName("content")]
        public Content Content { get; set; }
    }

    /// <summary>
    /// Content.
    /// </summary>
    public class Content
    {

        /// <summary>
        /// Parts.
        /// </summary>
        [JsonPropertyName("parts")]
        public List<Part> Parts { get; set; }
    }

    /// <summary>
    /// Part.
    /// </summary>
    public class Part
    {

        /// <summary>
        /// Text.
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}

