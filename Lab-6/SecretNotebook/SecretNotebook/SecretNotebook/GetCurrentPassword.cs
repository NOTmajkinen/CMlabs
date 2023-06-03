namespace SecretNotebook
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using SecretNotebook.EncryptDecrypt;
    using SecretNotebook.Exceptions;

    public static class GetCurrentPassword
    {
        public static string GetPassword(string fileName, string hash)
        {
            if (File.Exists(fileName))
            {
                var password = File.ReadAllLines(fileName).FirstOrDefault();

                password = password.Substring(0, password.Length - 1);

                return DecryptData.Decrypt(password, hash);
            }
            else
            {
                throw new FileDoesNotExistException();
            }
        }
    }
}
