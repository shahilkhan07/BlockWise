using Microsoft.AspNetCore.DataProtection;

namespace Vota.WebApi.Utilities
{
    /// <summary>
    /// Crypto Utils.
    /// </summary>
    public class CryptoUtils
    {
        private readonly IDataProtector _protector;
        /// <summary>
        /// Creates CryptoUtils.
        /// </summary>
        /// <param name="provider">Provider.</param>
        public CryptoUtils(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("CryptoUtils");
        }
        /// <summary>
        /// Get encrypted token.
        /// </summary>
        /// <param name="token">Token.</param>
        /// <returns>Encrypted token.</returns>
        public string GetEncryptedToken(string token)
        {
            return _protector.Protect(token);
        }
        /// <summary>
        /// Get decrypted token.
        /// </summary>
        /// <param name="token">Token.</param>
        /// <returns>Decrypted token.</returns>
        public string GetDecryptedToken(string token)
        {
            return _protector.Unprotect(token);
        }
    }
}
