using System;
using System.Text;
using System.Security.Cryptography;
using JetBrains.Annotations;

namespace kamafi.auth.data.extensions
{
    public static class AuthExtensions
    {
        public static int? ToNullableInt(this string s)
        {
            int i;

            return int.TryParse(s, out i) 
                ? i
                : null;
        }

        public static Guid? ToNullableGuid(this string s)
        {
            Guid i;

            return Guid.TryParse(s, out i)
                ? i
                : null;
        }

        public static string Base64Encode([NotNull] this string text)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode([NotNull] this string base64EncodedString)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedString);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string GenerateApiKey(int length = 32)
        {
            var key = new byte[length];
            using (var generator = RandomNumberGenerator.Create())
                generator.GetBytes(key);
            return Convert.ToBase64String(key);
        }

        public static string DecodeKey([NotNull] this string apiKey)
        {
            return apiKey.Split('.')
                .First()
                .Base64Decode();
        }
    }
}
