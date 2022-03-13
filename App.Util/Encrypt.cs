using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace App.Util
{
    public static class Encrypt
    {
        /// <summary>
        /// Genera llave vector para encritar dato
        /// </summary>
        /// <param name="strKey">Llave</param>
        /// <param name="strIv">Vector</param>
        /// <returns></returns>
        private static (byte[], byte[]) GenKeyVector(string strKey, string strIv)
        {
            byte[] key = UTF8Encoding.UTF8.GetBytes(strKey);
            byte[] iv = UTF8Encoding.UTF8.GetBytes(strIv);
            int keySize = 32;
            int ivSize = 16;
            Array.Resize(ref key, keySize);
            Array.Resize(ref iv, ivSize);
            return (key, iv);
        }
        /// <summary>
        /// Metodo para encriptar una cadena
        /// </summary>
        /// <param name="plainMessage">Texto a encriptar</param>
        /// <param name="strKey">Llave para encryptar</param>
        /// <param name="strIv">Vector para encri´tar</param>
        /// <returns>Devulve la cadena encriptada</returns>
        public static string EncryptString(string plainMessage, string strKey, string strIv)
        {
            var tKeyVector = GenKeyVector(strKey, strIv);
            byte[] Key = tKeyVector.Item1;
            byte[] IV = tKeyVector.Item2;
            var RijndaelAlg = Rijndael.Create();
            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, RijndaelAlg.CreateEncryptor(Key, IV), CryptoStreamMode.Write);
            byte[] plainMessageBytes = UTF8Encoding.UTF8.GetBytes(plainMessage);
            cryptoStream.Write(plainMessageBytes, 0, plainMessageBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherMessageBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(cipherMessageBytes);
        }

        /// <summary>
        /// Metodo para desencriptar una cadena a su valor 
        /// </summary>
        /// <param name="encryptedMessage">Texto a desencriptar</param>
        /// <param name="strKey">Llave para encritar</param>
        /// <param name="strIv">Vectro para encriptar</param>
        /// <returns>Devualve la cadena desencriptada</returns>
        public static string DecryptString(string encryptedMessage, string strKey, string strIv)
        {
            var tKeyVector = GenKeyVector(strKey, strIv);
            byte[] Key = tKeyVector.Item1;
            byte[] IV = tKeyVector.Item2;
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedMessage);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            var RijndaelAlg = Rijndael.Create();
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, RijndaelAlg.CreateDecryptor(Key, IV), CryptoStreamMode.Read);
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
    }
}
