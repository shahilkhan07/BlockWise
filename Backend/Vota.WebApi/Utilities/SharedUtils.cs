using Vota.Domain.Roles;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Vota.WebApi.Utilities
{
    /// <summary>
    /// Crypto utils
    /// </summary>
    public static class SharedUtils
    {
        private static long lastNumber = 0;
        private static readonly string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        /// <summary>
        /// Generate Password pash
        /// </summary>
        /// <param name="password">Password</param>
        /// <returns>(Password hash, Salt)</returns>
        public static (string hash, byte[] salt) GenerateHash(string password)
        {
            byte[] salt = new byte[256 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = GetPasswordHash(password, salt);
            return (hashed, salt);
        }

        /// <summary>
        /// Check Password hash
        /// </summary>
        /// <param name="password">Password</param>
        /// <param name="salt">Salt</param>
        /// <param name="hash">Psword hash</param>
        /// <returns></returns>
        public static bool CheckHash(string password, byte[] salt, string hash)
        {
            string hashed = GetPasswordHash(password, salt);
            return hashed == hash;
        }



        /// <summary>
        /// Get token.
        /// </summary>
        /// <param name="authClaims">Auth claims</param>
        /// <param name="jwtSecret">Jwt secret</param>
        /// <param name="jwtValidIssuer">Jwt valid issuer</param>
        /// <param name="jwtValidAudience">Jwt valid audience</param>
        /// <param name="jwtExpiryDays">Jwt expiry days</param>
        /// <returns>JwtSecurityToken</returns>
        public static JwtSecurityToken GetJWTToken(List<Claim> authClaims, string jwtSecret, string jwtValidIssuer, string jwtValidAudience, string jwtExpiryDays)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));

            var token = new JwtSecurityToken(
                issuer: jwtValidIssuer,
                audience: jwtValidAudience,
                expires: DateTime.Now.AddDays(String.IsNullOrWhiteSpace(jwtExpiryDays) ? 7 : Convert.ToInt64(jwtExpiryDays)),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));
            return token;
        }


        /// <summary>
        /// Get Token Claims
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="userId">User identity</param>
        /// <param name="name">Name</param>
        /// <param name="role">Role</param>
        /// <returns>Claims</returns>
        public static List<Claim> GetTokenClaims(string email, int? userId, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email ?? string.Empty),
                new Claim(ClaimTypes.NameIdentifier, userId?.ToString() ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, role)
            };
            return claims;
        }

        /// <summary>
        /// Generate order number
        /// </summary>
        /// <returns>Order number</returns>
        public static string GenerateOrderNumber()
        {
            long currentNumber = Interlocked.Increment(ref lastNumber);
            string timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            string randomPart = GenerateRandomString(6);
            string orderNumber = $"{timestamp}-{currentNumber:D10}-{randomPart}";
            return orderNumber.Length <= 50 ? orderNumber : orderNumber.Substring(0, 50);
        }

        /// <summary>
        /// Get role identity by name
        /// </summary>
        /// <param name="roleName">Role name</param>
        /// <returns>Role identity</returns>
        public static int GetRoleIdByName(string roleName)
        {
            if (string.IsNullOrEmpty(roleName)) 
                return 0;

            switch (roleName)
            {
                case nameof(RoleConst.Admin):
                    return RoleConst.Admin;

                case nameof(RoleConst.Influencer):
                    return RoleConst.Influencer;
            }
            return RoleConst.User;
        }

        /// <summary>
        /// Get service request code
        /// </summary>
        /// <returns>Service request code</returns>
        public static string GenerateServiceRequestCode()
        {
            string prefix = "SR"; // You can change this prefix if needed
            string timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
            string randomPart = GetRandomString(5);

            return $"{prefix}-{timestamp}-{randomPart}";
        }

        private static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder result = new StringBuilder(length);
            var rng = RandomNumberGenerator.Create();
            {
                byte[] buffer = new byte[length];
                rng.GetBytes(buffer);
                foreach (byte b in buffer)
                {
                    result.Append(chars[b % chars.Length]);
                }
            }
            return result.ToString();
        }

        private static string GenerateRandomString(int length)
        {
            var random = new Random();
            char[] result = new char[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = chars[random.Next(chars.Length)];
            }
            return new string(result);
        }

        private static string GetPasswordHash(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 512 / 8
            ));
        }
    }
}
