using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Signing.System.Tcc.Helpers
{
    public static class Helpers
    {
        public static string SigningSystemScheme => "SigningSystem";

        public static string SigningSystemCookieName => "SigningSystemCookie";

        public static string RemoveAccents(this string text)
        {
            var sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (var letter in arrayText)
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            return sbReturn.ToString();
        }

        public static string RetrieveNumbersOnly(this string text)
        {
            return Regex.Replace(text, "[^0-9]", "");
        }

        public static void RemovePluralizingTableNameConvention(this ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
            {
                if (entity.ClrType == null)
                    continue;

                entity.Relational().TableName = entity.ClrType.Name;
            }
        }

        public static string GenerateHashSHA256(string inputData)
        {
            if (string.IsNullOrEmpty(inputData))
                return string.Empty;

            // Create a SHA256   
            using (var sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(inputData));

                // Convert byte array to a string   
                var builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
