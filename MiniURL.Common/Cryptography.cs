using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MiniURL.Common
{
    public static class Cryptography
    {
        private const string ENCRYPTION_KEY = "A60934D8C1A2AC3A69642A3902198";
        private readonly static byte[] SALT = new byte[] { 99, 52, 2, 24, 51, 67, 22, 88 };

        /// <summary>
        /// Method to convert a url to short hand url with supplied max length
        /// </summary>
        /// <param>originalUrl</param>        
        /// <param>maxLength</param>        
        /// <returns>string</returns>
        public async static Task<string> EncryptUrl(string originalUrl, int maxLength)
        {
            byte[] plainText = Encoding.Unicode.GetBytes(originalUrl);
            var secretKey = new Rfc2898DeriveBytes(ENCRYPTION_KEY, SALT);
            using (RijndaelManaged rijndaelCipher = new RijndaelManaged())
            {
                using (ICryptoTransform encryptor =
                rijndaelCipher.CreateEncryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
                using (var memoryStream = new MemoryStream())
                using (var cryptoStream = new CryptoStream(memoryStream,
                encryptor, CryptoStreamMode.Write))
                {
                    await cryptoStream.WriteAsync(plainText, 0, plainText.Length);
                    cryptoStream.FlushFinalBlock();
                    string encryptedUrl =
                    Convert.ToBase64String(memoryStream.ToArray());
                    encryptedUrl = HttpUtility.UrlEncode(encryptedUrl);
                    if (encryptedUrl.Length > maxLength)
                    {
                        encryptedUrl = encryptedUrl.Substring(0, maxLength);
                    }
                    return encryptedUrl;
                }
            }
        }

        #region "Key Encryption Logic"
        //private static byte[][] GetHashKeys(string key)
        //{
        //    byte[][] result = new byte[2][];
        //    Encoding enc = Encoding.UTF8;

        //    SHA256 sha2 = new SHA256CryptoServiceProvider();

        //    byte[] rawKey = enc.GetBytes(key);
        //    byte[] rawIV = enc.GetBytes(key);

        //    byte[] hashKey = sha2.ComputeHash(rawKey);
        //    byte[] hashIV = sha2.ComputeHash(rawIV);

        //    Array.Resize(ref hashIV, 16);

        //    result[0] = hashKey;
        //    result[1] = hashIV;

        //    return result;
        //}

        #endregion

        #region "Encryption Logic"
        //public async static Task<string> AESEncrypt(string key, string data)
        //{
        //    string encData = null;
        //    byte[][] keys = GetHashKeys(key);

        //    try
        //    {
        //        encData = await EncryptStringToBytes_Aes(data, keys[0], keys[1]);
        //    }
        //    catch (CryptographicException) { }
        //    catch (ArgumentNullException) { }

        //    return encData;
        //}


        //private async static Task<string> EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        //{
        //    if (plainText == null || plainText.Length <= 0)
        //        throw new ArgumentNullException("plainText");
        //    if (Key == null || Key.Length <= 0)
        //        throw new ArgumentNullException("Key");
        //    if (IV == null || IV.Length <= 0)
        //        throw new ArgumentNullException("IV");

        //    byte[] encrypted;

        //    using (AesManaged aesAlg = new AesManaged())
        //    {
        //        aesAlg.Key = Key;
        //        aesAlg.IV = IV;

        //        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        //        using (MemoryStream msEncrypt = new MemoryStream())
        //        {
        //            using (CryptoStream csEncrypt =
        //                    new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        //            {
        //                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
        //                {
        //                    await swEncrypt.WriteAsync(plainText);
        //                }
        //                encrypted = msEncrypt.ToArray();
        //            }
        //        }
        //    }
        //    return Convert.ToBase64String(encrypted);
        //}

        #endregion

        #region "Decryption Logic"
        //public async static Task<string> Decrypt(string key, string data)
        //{
        //    string decData = null;
        //    byte[][] keys = GetHashKeys(key);

        //    try
        //    {
        //        decData = await DecryptStringFromBytes_Aes(data, keys[0], keys[1]);
        //    }
        //    catch (CryptographicException) { }
        //    catch (ArgumentNullException) { }

        //    return decData;
        //}


        //private async static Task<string> DecryptStringFromBytes_Aes(string cipherTextString, byte[] Key, byte[] IV)
        //{
        //    byte[] cipherText = Convert.FromBase64String(cipherTextString);

        //    if (cipherText == null || cipherText.Length <= 0)
        //        throw new ArgumentNullException("cipherText");
        //    if (Key == null || Key.Length <= 0)
        //        throw new ArgumentNullException("Key");
        //    if (IV == null || IV.Length <= 0)
        //        throw new ArgumentNullException("IV");

        //    string plaintext = null;

        //    using (Aes aesAlg = Aes.Create())
        //    {
        //        aesAlg.Key = Key;
        //        aesAlg.IV = IV;

        //        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        //        using (MemoryStream msDecrypt = new MemoryStream(cipherText))
        //        {
        //            using (CryptoStream csDecrypt =
        //                    new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
        //            {
        //                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
        //                {
        //                    plaintext = await srDecrypt.ReadToEndAsync();
        //                }
        //            }
        //        }
        //    }
        //    return plaintext;
        //}

        #endregion
    }
}
