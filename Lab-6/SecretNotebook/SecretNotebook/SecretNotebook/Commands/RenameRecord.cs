namespace SecretNotebook
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using SecretNotebook.EncryptDecrypt;
    using SecretNotebook.Exceptions;

    public class RenameRecord
    {
        private static List<string> splittedText;

        public RenameRecord(string text, string hash)
        {
            splittedText = text.Split('|').ToList();

            splittedText = splittedText.Select(item => DecryptData.Decrypt(item, hash)).ToList();
        }

        public void Rename(string fileName, string hash)
        {
            while (true)
            {
                Console.WriteLine("\nPlease input record name or 'exit' to exit: \n");

                string recordName = Console.ReadLine();

                if (splittedText.Contains(recordName))
                {
                    int index = splittedText.IndexOf(recordName);

                    Console.WriteLine("\nPlease input a new record name: \n");

                    string newRecordName = Console.ReadLine();

                    if (!string.IsNullOrEmpty(newRecordName))
                    {
                        splittedText[index] = newRecordName;

                        string temp = splittedText[0];

                        splittedText = splittedText.Skip(1).Select(item => EncryptData.Encrypt(item, hash)).ToList();

                        string text = string.Join('|', splittedText);

                        File.Delete(fileName);

                        var notebookFile = File.Create(fileName);
                        notebookFile.Close();

                        File.WriteAllText(fileName, EncryptData.Encrypt(temp , hash) + '|' + '\n' + text);
                    }
                }
                else if (recordName == "exit")
                {
                    Console.WriteLine("\nClose renaming..\n");
                    break;
                }
                else
                {
                    throw new RecordDoesNotExistException();
                }
            }
        }
    }
}
