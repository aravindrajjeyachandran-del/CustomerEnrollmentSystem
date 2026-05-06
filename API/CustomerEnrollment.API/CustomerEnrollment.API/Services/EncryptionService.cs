using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class EncryptionService
{
    private readonly byte[] _key = Encoding.UTF8.GetBytes("12345678901234567890123456789012");
    private readonly byte[] _iv = Encoding.UTF8.GetBytes("1234567890123456");

    public string Decrypt(string base64)
    {
        if (string.IsNullOrWhiteSpace(base64))
            return string.Empty;

        var buffer = Convert.FromBase64String(base64);

        using (var aes = Aes.Create())
        {
            aes.Key = _key;
            aes.IV = _iv;

            using (var ms = new MemoryStream(buffer))
            using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
            using (var sr = new StreamReader(cs))
            {
                return sr.ReadToEnd();
            }
        }
    }
}