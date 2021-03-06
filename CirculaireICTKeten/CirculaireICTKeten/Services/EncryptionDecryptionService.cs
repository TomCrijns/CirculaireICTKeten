using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CirculaireICTKeten.Services
{
    public static class EncryptionDecryptionService
    {
        /// <summary>
        /// This method will encrypt a string
        /// WARNING: Do not alter this code without consultation of the developer who worked last on it to prevent encryption failures
        /// </summary>
        /// <param name="textToEncrypt">Text to encrypt</param>
        /// <param name="email">E-mail address of user</param>
        /// <param name="salt">Existing salt from database</param>
        /// <returns>An encrypted string</returns>
        public static string Encrypt(string textToEncrypt, string email, string salt)
        {
            try
            {
                string stringToEncrypt = textToEncrypt;
                string stringToReturn = "";
                var publicKey = email.AsSpan(0, 8);
                string secretKey = salt;

                byte[] secretKeyByte = { };
                byte[] publicKeyByte = { };

                secretKeyByte = System.Text.Encoding.UTF8.GetBytes(secretKey);
                publicKeyByte = System.Text.Encoding.UTF8.GetBytes(publicKey.ToString());
                MemoryStream ms = null;
                CryptoStream cs = null;

                byte[] inputByArray = System.Text.Encoding.UTF8.GetBytes(stringToEncrypt);
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateEncryptor(publicKeyByte, secretKeyByte), CryptoStreamMode.Write);
                    cs.Write(inputByArray, 0, inputByArray.Length);
                    cs.FlushFinalBlock();
                    stringToReturn = Convert.ToBase64String(ms.ToArray());
                }
                return stringToReturn;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// This method will encrypt a string
        /// WARNING: Do not alter this code without consultation of the developer who worked last on it to prevent encryption failures
        /// </summary>
        /// <param name="encryptedString">An encrypted string</param>
        /// <param name="email">E-mail address of user</param>
        /// <param name="salt">Existing salt from database</param>
        /// <returns>An decrypted string</returns>
        public static string Decrypt(string encryptedString, string email, string salt)
        {
            try
            {
                string textToDecrypt = encryptedString;
                string stringToReturn = "";
                var publicKey = email.AsSpan(0, 8);
                string secretKey = salt;

                byte[] secretKeyByte = { };
                byte[] publicKeyByte = { };

                secretKeyByte = System.Text.Encoding.UTF8.GetBytes(secretKey);
                publicKeyByte = System.Text.Encoding.UTF8.GetBytes(publicKey.ToString());
                MemoryStream ms = null;
                CryptoStream cs = null;

                byte[] inputByArray = new byte[textToDecrypt.Replace(" ", "+").Length];
                inputByArray = Convert.FromBase64String(textToDecrypt.Replace(" ", "+"));
                using(DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateDecryptor(publicKeyByte, secretKeyByte), CryptoStreamMode.Write);
                    cs.Write(inputByArray, 0, inputByArray.Length);
                    cs.FlushFinalBlock();
                    Encoding encoding = Encoding.UTF8;
                    stringToReturn = encoding.GetString(ms.ToArray());
                }

                return stringToReturn;
            }
            catch (Exception ex)
            {
                return "Invalid";
            }
        }
    }


}
