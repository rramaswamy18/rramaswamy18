using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryUtility
{
    public static class EncryptDecrypt
    {
        #region Decrypt
        public static string DecryptDataAes(string encryptedData, string hashData)
        {
            byte[] rgbIV = new byte[] { 0x79, 0xf1, 10, 1, 0x84, 0x4a, 11, 0x27, 0xff, 0x5b, 0x2d, 0x4e, 14, 0xd3, 0x16, 0x3e };
            byte[] bytes = Encoding.ASCII.GetBytes(hashData);
            byte[] buffer = Convert.FromBase64String(encryptedData);
            RijndaelManaged managed = new RijndaelManaged();
            MemoryStream stream = new MemoryStream(buffer);
            CryptoStream stream2 = new CryptoStream(stream, managed.CreateDecryptor(bytes, rgbIV), CryptoStreamMode.Read);
            byte[] buffer3 = new byte[buffer.Length];
            stream2.Read(buffer3, 0, buffer.Length);
            return Encoding.ASCII.GetString(buffer3).TrimEnd(new char[1]);
        }

        public static string DecryptDataMd5(string encryptedData, string hashData)
        {
            byte[] buffer = new MD5CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(hashData));
            TripleDESCryptoServiceProvider provider2 = new TripleDESCryptoServiceProvider
            {
                Key = buffer,
                Mode = CipherMode.ECB
            };
            byte[] inputBuffer = Convert.FromBase64String(encryptedData);
            return Encoding.ASCII.GetString(provider2.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
        }

        public static string DecryptDataTripleDes(string encryptedData, string hashData)
        {
            byte[] rgbIV = new byte[] { 0x79, 0xf1, 10, 1, 0x84, 0x4a, 11, 0x27, 0xff, 0x5b, 0x2d, 0x4e, 14, 0xd3, 0x16, 0x3e };
            TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider
            {
                Mode = CipherMode.CBC
            };
            byte[] buffer = Convert.FromBase64String(encryptedData);
            MemoryStream stream = new MemoryStream(buffer);
            byte[] bytes = Encoding.ASCII.GetBytes(hashData);
            byte[] buffer3 = new byte[buffer.Length];
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(bytes, rgbIV), CryptoStreamMode.Read);
            stream2.Read(buffer3, 0, buffer.Length);
            stream.Close();
            stream2.Close();
            return Encoding.ASCII.GetString(buffer);
        }
        #endregion

        #region Encrypt
        public static string EncryptDataAes(string decryptedData, string hashData)
        {
            byte[] rgbIV = new byte[] { 0x79, 0xf1, 10, 1, 0x84, 0x4a, 11, 0x27, 0xff, 0x5b, 0x2d, 0x4e, 14, 0xd3, 0x16, 0x3e };
            byte[] bytes = Encoding.ASCII.GetBytes(hashData);
            byte[] buffer = Encoding.ASCII.GetBytes(decryptedData);
            RijndaelManaged managed = new RijndaelManaged();
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, managed.CreateEncryptor(bytes, rgbIV), CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            byte[] inArray = stream.ToArray();
            stream.Close();
            stream2.Close();
            return Convert.ToBase64String(inArray);
        }

        public static string EncryptDataMd5(string decryptedData, string hashData)
        {
            byte[] buffer = new MD5CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(hashData));
            TripleDESCryptoServiceProvider provider2 = new TripleDESCryptoServiceProvider
            {
                Key = buffer,
                Mode = CipherMode.ECB
            };
            byte[] bytes = Encoding.ASCII.GetBytes(decryptedData);
            return Convert.ToBase64String(provider2.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length));
        }

        public static string EncryptDataTripleDes(string decryptedData, string hashData)
        {
            byte[] rgbIV = new byte[] { 0x79, 0xf1, 10, 1, 0x84, 0x4a, 11, 0x27, 0xff, 0x5b, 0x2d, 0x4e, 14, 0xd3, 0x16, 0x3e };
            MemoryStream stream = new MemoryStream();
            TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider
            {
                Mode = CipherMode.CBC
            };
            byte[] bytes = Encoding.ASCII.GetBytes(decryptedData);
            byte[] rgbKey = Encoding.ASCII.GetBytes(hashData);
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            byte[] inArray = stream.ToArray();
            stream.Close();
            stream2.Close();
            return Convert.ToBase64String(inArray);
        }
        #endregion
    }
}
