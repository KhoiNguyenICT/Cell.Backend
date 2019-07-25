using System;
using System.Security.Cryptography;
using System.Text;

namespace Cell.Common.Extensions
{
    public static class StringExtensions
    {
        public static string ToSha256(this string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return hash;
            }
        }

        public static string ToCamelCasing(this string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            return char.ToLowerInvariant(str[0]) + str.Substring(1);
        }

        public static string FirstCharToUpper(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        public static string[] GetDefaultSorts()
        {
            return new[] { "-created" };
        }
    }
}