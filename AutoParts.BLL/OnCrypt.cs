﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AutoParts.BLL
{
    public class OnCrypt
    {
        static readonly string PasswordHash = "P@@Sw0rd";
        static readonly string SaltKey = "S@LT&KEY";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";

        public static string Encrypt(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)))
            {
                var keyBytes = rfc2898DeriveBytes.GetBytes(256 / 8);
                using (var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros })
                {
                    var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

                    byte[] cipherTextBytes;

                    using (var memoryStream = new MemoryStream())
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                            cryptoStream.FlushFinalBlock();
                            cipherTextBytes = memoryStream.ToArray();
                            cryptoStream.Close();
                        }
                        memoryStream.Close();
                    }
                    return Convert.ToBase64String(cipherTextBytes);
                }
            }
        }

        public static string Decrypt(string encryptedText)
        {
            var cipherTextBytes = Convert.FromBase64String(encryptedText);
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)))
            {
                var keyBytes = rfc2898DeriveBytes.GetBytes(256 / 8);
                using (var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC, Padding = PaddingMode.None })
                {
                    var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
                    var memoryStream = new MemoryStream(cipherTextBytes);
                    using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        var plainTextBytes = new byte[cipherTextBytes.Length];

                        var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                        memoryStream.Close();
                        cryptoStream.Close();
                        return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
                    }
                }
            }
        }
    }
}
