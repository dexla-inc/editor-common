using System.Security.Cryptography;
using System.Web;
using Dexla.Common.Types.Enums;

namespace Dexla.Common.Utilities;

public static class StringCipher
{
    private const int KeySize = 128;
    private const int KeySizeBytes = KeySize / 8;

    public static string Encrypt(string plainText, string passPhrase, int iterations = 1000, ConversionTypes conversionType = ConversionTypes.BASE64)
    {
        byte[] saltBytes = GenerateRandomBytes();

        using RijndaelManaged rijAlg = new();

        using Rfc2898DeriveBytes password = new(passPhrase, saltBytes, iterations);
        byte[] keyBytes = password.GetBytes(KeySizeBytes);

        // Create an encryptor to perform the stream transform.
        ICryptoTransform encryptor = rijAlg.CreateEncryptor(keyBytes, rijAlg.IV);

        // Create the streams used for encryption.
        using MemoryStream msEncrypt = new();
        using (CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write)) {
            using (StreamWriter swEncrypt = new(csEncrypt))
            {
                swEncrypt.Write(plainText);
            }
        }
            
        byte[] encrypted = msEncrypt.ToArray();
        byte[] cipherTextBytes = rijAlg.IV;
        cipherTextBytes = cipherTextBytes.Concat(saltBytes).ToArray();
        cipherTextBytes = cipherTextBytes.Concat(encrypted).ToArray();

        return conversionType switch
        {
            ConversionTypes.HEX => Convert.ToHexString(cipherTextBytes),
            _ => Convert.ToBase64String(cipherTextBytes)
        };
    }

    public static string EncryptForUrl(string plainText, string passPhrase, int iterations = 1000, ConversionTypes conversionType = ConversionTypes.BASE64)
    {
        string cipherText = Encrypt(plainText, passPhrase, iterations, conversionType);
        return HttpUtility.UrlEncode(cipherText);
    }

    public static string Decrypt(string cipherText, string passPhrase, int iterations = 1000, ConversionTypes conversionType = ConversionTypes.BASE64)
    {
        byte[] cipherTextBytesWithIv = conversionType switch
        {
            ConversionTypes.HEX => Convert.FromHexString(cipherText),
            _ => Convert.FromBase64String(cipherText)
        };
        
        byte[] ivStringBytes = cipherTextBytesWithIv.Take(KeySizeBytes).ToArray();
        byte[] saltBytes = cipherTextBytesWithIv.Skip(KeySizeBytes).Take(KeySizeBytes).ToArray();
        byte[] cipherTextBytes = cipherTextBytesWithIv.Skip(KeySizeBytes * 2).Take(cipherTextBytesWithIv.Length - KeySizeBytes * 2).ToArray();

        using Rfc2898DeriveBytes password = new(passPhrase, saltBytes, iterations);
        byte[] keyBytes = password.GetBytes(KeySizeBytes);
        
        using RijndaelManaged rijAlg = new();

        rijAlg.IV = ivStringBytes;

        ICryptoTransform decryptor = rijAlg.CreateDecryptor(keyBytes, rijAlg.IV);

        using MemoryStream msDecrypt = new(cipherTextBytes);
        using CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read);
        using StreamReader srDecrypt = new(csDecrypt);

        string plaintext = srDecrypt.ReadToEnd();

        return plaintext;
    }
        
    private static byte[] GenerateRandomBytes()
    {
        using RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();

        byte[] randomBytes = new byte[KeySizeBytes];
        randomNumberGenerator.GetBytes(randomBytes);
        return randomBytes;
    }
}