using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TestCore.Helper
{
    public static class CommonFunctions
    {
        public static bool ValidateUser(byte[] passwordHash, byte[] passwordSalt, string password)
        {
            var hashAlgorithm = new SHA512HashAlgorithm();
            try
            {
                var getByte = GetBytes(password);
                var generatedSaltedHash = hashAlgorithm.GenerateSaltedHash(getByte, passwordSalt);
                var flag = CompareByteArrays(generatedSaltedHash, passwordSalt);
            }
            catch (Exception ex) { }
            var s = 5;
            return CompareByteArrays(passwordHash, hashAlgorithm.GenerateSaltedHash(GetBytes(password), passwordSalt));
        }
        public static bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            return array1.Length == array2.Length && !array1.Where((t, i) => t != array2[i]).Any();
        }
        public static byte[] CreateSalt(int size)
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return buff;
        }
        public static byte[] GetBytes(string text)
        {
            return Encoding.UTF8.GetBytes(text);
        }

        public static string RandCode(int Id)
        {
            string id = Id.ToString();
            var len = id.Length;
            if (len >= 6)
            {
                return id;
            }
            else
            {
                var s = "";
                var l = 6 - len;
                for (var i = 0; i < l; i++)
                {
                    s += "0";
                }
                s += id;
                return s;
            }
        }

    }

    public class SHA512HashAlgorithm
    {
        private readonly SHA512Managed _algorithm = new SHA512Managed();

        public byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        {
            var plainTextWithSaltBytes =
                new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return _algorithm.ComputeHash(plainTextWithSaltBytes);
        }

        public string Type { get { return "SHA512"; } }

    }
}