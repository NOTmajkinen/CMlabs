namespace SecretNotebook.Commands
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using SecretNotebook.EncryptDecrypt;

    public class DeleteRecord
    {
        private static List<string> splittedText;

        public DeleteRecord(string text, string hash)
        {
            splittedText = text.Split('|').ToList();

            splittedText = splittedText.Select(item => DecryptData.Decrypt(item, hash)).ToList();
        }

        public void Delete(string fileName, string hash)
        {
            while (true)
            {
                Console.WriteLine("\nPlease input record name or 'exit' to exit: ");

                string recordName = Console.ReadLine();

                if (string.IsNullOrEmpty(recordName) || (recordName == "exit"))
                {
                    Console.WriteLine("\nClose deleting...");
                    break;
                }
                else
                {
                    int index = splittedText.IndexOf(recordName);

                    splittedText.RemoveAt(index);
                    splittedText.RemoveAt(index + 1);

                    string temp = splittedText[0];

                    splittedText = splittedText.Skip(1).Select(item => EncryptData.Encrypt(item, hash)).ToList();

                    string text = string.Join('|', splittedText);

                    File.Delete(fileName);

                    var notebookFile = File.Create(fileName);
                    notebookFile.Close();

                    File.WriteAllText(fileName, EncryptData.Encrypt(temp, hash) + '|' + '\n' + text);

                    Console.WriteLine("Record deleted succesfully!");
                }
            }
        }
    }
}
