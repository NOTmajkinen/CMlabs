namespace DataGenerator
{
    using System;
    using System.Collections.Generic;
    using Social.Models;

    public class Program
    {
        private const string PathDirectory = @"C:\Users\cpyte\Desktop\DevCourse\nikita_atmaykin\Lab-6\Social\Social\Data";
        private const string PathUsers = PathDirectory + @"\users.json";
        private const string PathFriends = PathDirectory + @"\friends.json";
        private const string PathMessages = PathDirectory + @"\messages.json";

        private static List<User> users;
        private static List<Friend> friends;
        private static List<Message> messages;

        private static void Main(string[] args)
        {
            users = CreateUsersData.CreateData();

            friends = CreateFriendsData.CreateData(users);

            messages = CreateMessagesData.CreateData(users);

            // serializing data
            try
            {
                var dataSerializer = new DataSerialize(PathUsers, PathFriends, PathMessages, users, friends, messages);
                Console.WriteLine("Serializing completed successfully!");
            }
            catch (Exception)
            {
                Console.WriteLine("Serializing wasn't completed successfully");
            }
        }
    }
}
