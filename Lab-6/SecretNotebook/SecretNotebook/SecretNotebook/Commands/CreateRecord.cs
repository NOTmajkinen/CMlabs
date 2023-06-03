namespace SecretNotebook.Commands
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using SecretNotebook.Exceptions;

    public class CreateRecord
    {
        public CreateRecord()
        {
        }

        public void Create(string fileName, string hash)
        {
            while (true)
            {
                Console.WriteLine("\nPlease input record name or 'exit' to exit: \n");

                string encryptedRecordName = Console.ReadLine();

                if (string.IsNullOrEmpty(encryptedRecordName) || (encryptedRecordName == "exit" || encryptedRecordName == "e"))
                {
                    Console.WriteLine("\nClose creating..");
                    break;
                }
                else
                {
                    encryptedRecordName = EncryptData.Encrypt(encryptedRecordName, hash);

                    Console.WriteLine("\nPlease input text to your record: \n");

                    string recordText = EncryptData.Encrypt(Console.ReadLine(), hash);

                    string dateNow = EncryptData.Encrypt(DateTime.Now.ToString(), hash);

                    string completeRecord = $"|{encryptedRecordName}|{recordText}|{dateNow}|";

                    File.AppendAllText(fileName, completeRecord);

                    Console.WriteLine("\nRecord created successfully!");
                }
            }
        }
    }
}
