using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public class EncryptionDecryptionUsingSymmetricKey
    {
        public static string EncryptString(string key, string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }
            string base64UrlEncoded = Base64UrlEncode(array);
            return base64UrlEncoded;
        }
        public static string Base64UrlEncode(byte[] input)
        {
            return Convert.ToBase64String(input)
           .Replace('+', '-')
           .Replace('/', '_')
           .Replace("=", "");
        }
        public static byte[] Base64UrlDecode(string input)
        {
            string base64 = input.Replace('-', '+').Replace('_', '/');
            base64 += new string('=', (4 - base64.Length % 4) % 4);
            return Convert.FromBase64String(base64);
        }
        public static string DecryptString(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Base64UrlDecode(cipherText); //Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
        public static bool IsSymmetricKey(string key)
        {
            // Check the length of the key
            int keyLength = key.Length * 8;
            return keyLength == 128 || keyLength == 192 || keyLength == 256;
        }      
    }
}
