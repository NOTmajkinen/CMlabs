namespace SecretNotebook
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Text;

    public static class EncryptData
    {
        public static string Encrypt(string data, string hash)
        {
            byte[] byteData = UTF8Encoding.UTF8.GetBytes(data);

            using (MD5CryptoServiceProvider mD5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = mD5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));

                using (TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform cryptoTransform = tripleDES.CreateEncryptor();

                    byte[] result = cryptoTransform.TransformFinalBlock(byteData, 0, byteData.Length);

                    return Convert.ToBase64String(result, 0, result.Length);
                }
            }
        }
    }
}
