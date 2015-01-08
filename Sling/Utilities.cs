using System;
using System.Text;

namespace Sling
{
    public static class Utilities
    {
        #region Fields
        /// <summary>
        /// Encode a Base64 string.
        /// </summary>
        /// <param name="plainText">The unencoded data.</param>
        /// <returns></returns>
        public static string Base64Encode(string plainText) {
            byte[] bytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Decode a Base64 string
        /// </summary>
        /// <param name="data">The base64 encoded string.</param>
        /// <returns>Binary data.</returns>
        public static byte[] Base64Decode(string data) {
            return Convert.FromBase64String(data);
        }
        #endregion
    }
}
