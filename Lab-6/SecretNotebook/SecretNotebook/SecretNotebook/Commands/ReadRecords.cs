namespace SecretNotebook
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using SecretNotebook.EncryptDecrypt;
    using SecretNotebook.Exceptions;

    public class ReadRecords
    {
        private static List<string> splittedText;

        public ReadRecords(string text, string hash)
        {
            splittedText = text.Split('|').ToList();

            splittedText = splittedText.Select(item => DecryptData.Decrypt(item, hash)).ToList();
        }

        public void Read()
        {
            while (true)
            {
                Console.WriteLine("\nPlease input record name or 'exit' to exit: ");

                string recordName = Console.ReadLine();

                if (splittedText.Contains(recordName))
                {
                    int index = splittedText.IndexOf(recordName);

                    string text = splittedText[index + 1] + ", " + splittedText[index + 2];

                    Console.ForegroundColor = ConsoleColor.Yellow;

                    Console.WriteLine("\nContent of this record: ");

                    Console.WriteLine('\n' + text);

                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (recordName == "exit")
                {
                    Console.WriteLine("Close reading..");
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
