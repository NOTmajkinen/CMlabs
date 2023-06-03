namespace Social.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Social.Models;

    public static class ListExtension
    {
        public static string ListToString(this List<int> list)
        {
            string output = " ";

            if (list.Count <= 5)
            {
                list.ForEach(item => output += item + " ");
            }
            else
            {
                output += $"{list[0]} {list[1]} {list[2]} {list[3]} {list[5]}...";
            }

            return output;
        }

        public static List<int> GetLikes(this List<User> users)
        {
            Random random = new Random();

            int[] arr = new int[random.Next(users.Count)];

            arr = arr.Select(item => users[random.Next(users.Count)].UserId).ToArray();

            return arr.ToList();
        }
    }
}
