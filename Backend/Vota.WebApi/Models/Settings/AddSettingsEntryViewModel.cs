using System.ComponentModel.DataAnnotations;

namespace Vota.WebApi.Models.Settings
{
    /// <summary>
    /// Add settings entry view model.
    /// </summary>
    public class AddSettingsEntryViewModel
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        [Required]
        public string SettingsEntryId { get; set; }

        /// <summary>
        /// Value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        public string Description { get; set; }
    }
}
