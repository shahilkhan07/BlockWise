namespace Vota.Domain.Authorization
{
    /// <summary>
    /// Vota claims.
    /// </summary>
    public static class RhodeSideAssistClaims
    {
        /// <summary>
        /// Read settings claim.
        /// </summary>
        public const string SETTINGS_READ = "Vota-settings-read";

        /// <summary>
        /// Add settings claim.
        /// </summary>
        public const string SETTINGS_ADD = "Vota-settings-add";

        /// <summary>
        /// Update settings claim.
        /// </summary>
        public const string SETTINGS_UPDATE = "Vota-settings-update";

        /// <summary>
        /// Delete settings claim.
        /// </summary>
        public const string SETTINGS_DELETE = "Vota-settings-delete";

        /// <summary>
        /// Execute job claim.
        /// </summary>
        public const string JOB_EXECUTE = "Vota-job-execute";
    }
}
