namespace SecretNotebook.EncryptDecrypt
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Text;

    public class DecryptData
    {
        public static string Decrypt(string encryptedData, string hash)
        {
            byte[] byteData = Convert.FromBase64String(encryptedData);

            using (MD5CryptoServiceProvider mD5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = mD5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));

                using (TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform cryptoTransform = tripleDES.CreateDecryptor();

                    byte[] result = cryptoTransform.TransformFinalBlock(byteData, 0, byteData.Length);

                    return UTF8Encoding.UTF8.GetString(result);
                }
            }
        }
    }
}
