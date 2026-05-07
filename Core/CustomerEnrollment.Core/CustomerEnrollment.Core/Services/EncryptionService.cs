using System;
using System.Text;

namespace CustomerEnrollment.Core.Services
{
    public class EncryptionService : IEncryptionService
    {
        private readonly byte[] _key;

        public EncryptionService(IConfiguration config)
        {
            var secret = config["Encryption:SharedSecret"];
            using var sha = SHA256.Create();
            _key = sha.ComputeHash(Encoding.UTF8.GetBytes(secret));
        }

        public string Decrypt(string cipher)
        {
            var full = Convert.FromBase64String(cipher);
            var iv = full[..16];
            var data = full[16..];
            using var aes = Aes.Create();
            aes.Key = _key; aes.IV = iv;
            using var dec = aes.CreateDecryptor();
            using var ms = new MemoryStream(data);
            using var cs = new CryptoStream(ms, dec, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            return sr.ReadToEnd();
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