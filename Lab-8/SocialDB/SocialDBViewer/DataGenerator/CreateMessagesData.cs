namespace DataGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Social.Extensions;
    using Social.Models;

    public class CreateMessagesData
    {
        private static string strText = "Love For All, Hatred For None. Change the world by being yourself. Every moment is a fresh beginning. " +
              "Never regret anything that made you smile.  Die with memories, not dreams. Aspire to inspire before we expire. " +
              "Everything you can imagine is real. Simplicity is the ultimate sophistication. Whatever you do, do it well.  " +
              "Whatever you do, do it well. What we think, we become.  All limitations are self-imposed. Tough times never last but tough people do." +
              " Problems are not stop signs, they are guidelines.  If I’m gonna tell a real story, I’m gonna start with my name.";

        private static DateTime start = new DateTime(2019, 1, 1);

        private static int range = (DateTime.Today - start).Days;

        private static Random random = new Random();

        public static List<Message> CreateData(List<User> users)
        {
            string[] sentences = Regex.Split(strText, @"(?<=[\.!\?])\s+");

            var news = new List<Message>();

            foreach (var person in users)
            {
                for (int i = 0; i < 2; i++)
                {
                    news.Add(new Message(person.UserId, users.GetLikes(), random.Next(users.Count) + i, start.AddDays(random.Next(range)), sentences[random.Next(sentences.Length)]));
                }
            }

            return news;
        }
    }
}
