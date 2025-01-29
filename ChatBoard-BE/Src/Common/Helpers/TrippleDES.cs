using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public class TrippleDES
    {
        //public static string Decrypt(string cipher, string key)
        //{
        //    try
        //    {
        //        var encryptedText = cipher;
        //        TripleDESCryptoServiceProvider mDes = new TripleDESCryptoServiceProvider();
        //        mDes.Key = Convert.FromBase64String(key);
        //        mDes.Mode = CipherMode.ECB;
        //        mDes.Padding = PaddingMode.Zeros;
        //        ICryptoTransform mDesEnc = mDes.CreateDecryptor();
        //        byte[] data = Convert.FromBase64String(encryptedText);
        //        var plain = Encoding.ASCII.GetString(mDesEnc.TransformFinalBlock(data, 0, data.Length));

        //        return plain.Replace("\0", "");

        //    }
        //    catch (Exception exception)
        //    {
        //        return "";
        //    }
        //}
        //public static string Encrypt(string clearText, string key)
        //{
        //    try
        //    {
        //        var PlainText = clearText;
        //        TripleDESCryptoServiceProvider mDes = new TripleDESCryptoServiceProvider();
        //        mDes.Key = Convert.FromBase64String(key);
        //        mDes.Mode = CipherMode.ECB;
        //        mDes.Padding = PaddingMode.Zeros;
        //        ICryptoTransform mDesEnc = mDes.CreateEncryptor();
        //        byte[] data = Encoding.UTF8.GetBytes(PlainText);
        //        var crypto = Convert.ToBase64String(mDesEnc.TransformFinalBlock(data, 0, data.Length));
        //        return crypto;

        //    }
        //    catch (Exception exception)
        //    {
        //        return "";
        //    }
        //}

        public static string Encrypt(string TextToEncrypt, string key)
        {
            try
            {
                byte[] MyEncryptedArray = UTF8Encoding.UTF8.GetBytes(TextToEncrypt);
                MD5CryptoServiceProvider MyMD5CryptoService = new MD5CryptoServiceProvider();
                byte[] MysecurityKeyArray = MyMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                MyMD5CryptoService.Clear();
                var MyTripleDESCryptoService = new TripleDESCryptoServiceProvider();
                MyTripleDESCryptoService.Key = MysecurityKeyArray;
                MyTripleDESCryptoService.Mode = CipherMode.ECB;
                MyTripleDESCryptoService.Padding = PaddingMode.PKCS7;
                var MyCrytpoTransform = MyTripleDESCryptoService.CreateEncryptor();
                byte[] MyresultArray = MyCrytpoTransform.TransformFinalBlock(MyEncryptedArray, 0, MyEncryptedArray.Length);
                MyTripleDESCryptoService.Clear();
                return Convert.ToBase64String(MyresultArray, 0, MyresultArray.Length);
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public static string Decrypt(string TextToDecrypt, string key)  //test
        {
            byte[] MyDecryptArray = Convert.FromBase64String
               (TextToDecrypt);

            MD5CryptoServiceProvider MyMD5CryptoService = new
               MD5CryptoServiceProvider();

            byte[] MysecurityKeyArray = MyMD5CryptoService.ComputeHash
               (UTF8Encoding.UTF8.GetBytes(key));

            MyMD5CryptoService.Clear();

            var MyTripleDESCryptoService = new
               TripleDESCryptoServiceProvider();

            MyTripleDESCryptoService.Key = MysecurityKeyArray;

            MyTripleDESCryptoService.Mode = CipherMode.ECB;

            MyTripleDESCryptoService.Padding = PaddingMode.PKCS7;

            var MyCrytpoTransform = MyTripleDESCryptoService
               .CreateDecryptor();

            byte[] MyresultArray = MyCrytpoTransform
               .TransformFinalBlock(MyDecryptArray, 0,
               MyDecryptArray.Length);

            MyTripleDESCryptoService.Clear();

            return UTF8Encoding.UTF8.GetString(MyresultArray);
        }
    }
}
