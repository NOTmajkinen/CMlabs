namespace DataGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Social.Models;

    public class CreateFriendsData
    {
        private static Random random = new Random();

        private static DateTime start = new DateTime(2019, 1, 1);

        private static int range = (DateTime.Today - start).Days;

        public static List<Friend> CreateData(List<User> users)
        {
            var friends = new List<Friend>();

            foreach (var person in users)
            {
                for (int i = 0; i < 10; i++)
                {
                    int toUserId = random.Next(101);

                    friends.Add(new Friend(person.UserId, start.AddDays(random.Next(range)), random.Next(4), toUserId == 0 ? 1 : toUserId));
                }
            }

            return friends;
        }
    }
}
