namespace CustomerEnrollment.Core.Services
{
    public interface IEncryptionService
    {
        string Encrypt(string plain);
        string Decrypt(string cipher);
    }
}