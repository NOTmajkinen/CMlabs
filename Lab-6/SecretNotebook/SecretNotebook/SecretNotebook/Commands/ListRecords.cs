namespace SecretNotebook
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using SecretNotebook.EncryptDecrypt;
    using SecretNotebook.Exceptions;

    public class ListRecords
    {
        private static List<string> splittedText;

        public ListRecords(string text, string hash)
        {
            splittedText = text.Split('|').ToList();

            splittedText = splittedText.Select(item => DecryptData.Decrypt(item, hash)).ToList();
        }

        public void List()
        {
            while (true)
            {
                Console.WriteLine("\nListing records...");

                var count = 0;

                Console.ForegroundColor = ConsoleColor.Yellow;

                foreach (var item in splittedText)
                {
                    var recordData = string.Empty;

                    if (item == string.Empty & splittedText.Count > splittedText.IndexOf(item))
                    {
                         count++;

                         recordData = count.ToString() + ". " + splittedText[splittedText.IndexOf(item) + 1] + "\n" + splittedText[splittedText.IndexOf(item) + 3];
                    }
                    else
                    {
                        break;
                    }

                    Console.WriteLine("\n" + recordData);
                }

                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("\nListing complited :)");

                break;
            }
        }
    }
}
