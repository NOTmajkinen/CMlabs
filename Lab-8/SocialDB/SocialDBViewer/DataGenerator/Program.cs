namespace DataGenerator
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Configuration;
    using Social.Models;

    public class Program
    {
        private static List<User> users;
        private static List<Friend> friends;
        private static List<Message> messages;

        private static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .Build();

            string connectionString = config.GetConnectionString("DefaultConnectionString");

            Console.WriteLine("Generating started...");

            users = CreateUsersData.CreateData();

            Console.WriteLine("\nUsers generated..");

            friends = CreateFriendsData.CreateData(users);

            Console.WriteLine("\nFriends generated..");

            messages = CreateMessagesData.CreateData(users);

            Console.WriteLine("\nMessages generated..");

            Console.WriteLine("\nAdding data to data base..");

            InputToDataBase.AddUsers(users, connectionString);

            Console.WriteLine("\nUsers added..");

            InputToDataBase.AddFriends(friends, connectionString);

            Console.WriteLine("\nFriends added..");

            InputToDataBase.AddNews(messages, users, connectionString);

            Console.WriteLine("\nMessages and Likes added..");

            Console.WriteLine("\nData generated sucessfully!");
        }
    }
}
