using System;
using System.Text;

namespace CustomerEnrollment.Core.Services
{
    public class EncryptionService : IEncryptionService
    {
        public string Encrypt(string plain)
        {
            if (plain is null) return string.Empty;
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(plain));
        }

        public string Decrypt(string cipher)
        {
            if (cipher is null) return string.Empty;
            try
            {
                var bytes = Convert.FromBase64String(cipher);
                return Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}