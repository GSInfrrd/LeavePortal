using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPP_Utils
{
    public static class CommonMethods
    {

        public static string EncryptDataForLogins(string username, string password)
        {
            try
            {
                string salt = Constants.SALT;
                // merge password and salt together
                string sHashWithSalt = password + username.Trim() + salt;
                // convert this merged value to a byte array
                byte[] saltedHashBytes = Encoding.UTF8.GetBytes(sHashWithSalt);
                // use hash algorithm to compute the hash
                HashAlgorithm algorithm = new SHA256Managed();
                // convert merged bytes to a hash as byte array
                byte[] hash = algorithm.ComputeHash(saltedHashBytes);
                // return the has as a base 64 encoded string
                return Convert.ToBase64String(hash);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string EncryptString(string message)
        {
            string result = null;
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Constants.SALT));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToEncrypt = UTF8.GetBytes(message);
            try
            {
                ICryptoTransform encryptor = TDESAlgorithm.CreateEncryptor();
                Results = encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
                result = Convert.ToBase64String(Results);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return result;
        }

        public static string DecryptString(string Message)
        {
            string result = null;
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Constants.SALT));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            try
            {
                byte[] DataToDecrypt = Convert.FromBase64String(Message);
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
                result = UTF8.GetString(Results);
            }
            catch (Exception ex)
            {
                //Logger.Error("CommonMethods : DecryptString(): Caught an Error " + ex);
                return result;
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return result;
        }

        public static string Description(this Enum enumValue)
        {
            var enumType = enumValue.GetType();
            var field = enumType.GetField(enumValue.ToString());
            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length == 0
                ? enumValue.ToString()
                : ((DescriptionAttribute)attributes[0]).Description;
        }

    }
}
