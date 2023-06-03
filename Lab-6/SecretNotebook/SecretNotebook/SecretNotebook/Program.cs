namespace SecretNotebook
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using SecretNotebook.Commands;
    using SecretNotebook.Exceptions;

    public class Program
    {
        private const string FileName = "notebookrecords.txt";
        private const string Hash = "n0tb0k";

        private static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    if (File.Exists(FileName))
                    {
                        Console.ForegroundColor = ConsoleColor.White;

                        Console.WriteLine("\nPlease, input your password or 'exit' to exit Notebook: \n");

                        Console.ForegroundColor = ConsoleColor.Magenta;

                        string password = Console.ReadLine();

                        Console.ForegroundColor = ConsoleColor.White;

                        string passwordFromFile = GetCurrentPassword.GetPassword(FileName, Hash);

                        if (password == passwordFromFile)
                        {
                            string dataFromFile = File.ReadAllText(FileName);

                            Console.ForegroundColor = ConsoleColor.Green;

                            Console.WriteLine("\nAccess is allowed. Welcome!");

                            Console.ResetColor();

                            Console.ForegroundColor = ConsoleColor.Yellow;

                            Console.WriteLine("\nEnter your command:\t1. Read your records\t2. Rename your record\t3. Create a new record\t4. Delete record\n");

                            Console.ForegroundColor = ConsoleColor.Magenta;

                            var guess = Convert.ToInt32(Console.ReadLine());

                            Console.ForegroundColor = ConsoleColor.White;

                            switch ((Command.Commands)guess)
                            {
                                case Command.Commands.ReadRecord:
                                    ReadRecords readRecords = new ReadRecords(dataFromFile, Hash);
                                    readRecords.Read();
                                    break;
                                case Command.Commands.RenameRecord:
                                    RenameRecord renameRecord = new RenameRecord(dataFromFile, Hash);
                                    renameRecord.Rename(FileName, Hash);
                                    break;
                                case Command.Commands.CreateRecord:
                                    CreateRecord createRecord = new CreateRecord();
                                    createRecord.Create(FileName, Hash);
                                    break;
                                case Command.Commands.DeleteRecord:
                                    DeleteRecord deleteRecord = new DeleteRecord(dataFromFile, Hash);
                                    deleteRecord.Delete(FileName, Hash);
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (password == "exit" | password == "e")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Your password is incorrect!");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;

                        Console.WriteLine("Hello! Please, enter your new password(you are using Notebook for the first time)\n");

                        Console.ResetColor();

                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("or your records were removed...\n");

                        Console.ForegroundColor = ConsoleColor.White;

                        Console.WriteLine("Anyway, let's do it!\n");

                        Console.Write("Input your password: ");

                        Console.ForegroundColor = ConsoleColor.Magenta;

                        string password = Console.ReadLine();

                        // encrypting a password
                        string encryptedPassword = EncryptData.Encrypt(password, Hash);

                        Console.ForegroundColor = ConsoleColor.White;

                        var notebookFile = File.Create(FileName);

                        notebookFile.Close();

                        File.WriteAllText(FileName, encryptedPassword + '|');

                        File.AppendAllText(FileName, "\n");

                        Console.WriteLine("\nYour Notebook created succesfully!");
                    }
                }
                catch (FileDoesNotExistException)
                {
                    Console.WriteLine("\nSomething went wrong: Notebook has been deleted");
                }
                catch (RecordDoesNotExistException)
                {
                    Console.WriteLine("\nSomething went wrong: This record does not exist!");
                }
            }
        }
    }
}
