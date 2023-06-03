namespace DataGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Social.Models;

    public class CreateUsersData
    {
        private static List<string> names;

        private static Random random = new Random();

        private static DateTime start = new DateTime(1930, 1, 1);

        private static DateTime maxLastVisit = new DateTime(2020, 1, 1);

        private static string strNames = "Alvin Emanuel Roger Alfonzo Rudy Ernie Jake Trent Duane Wilfred Gregorio Jasper Boyd Delmer" +
                " Clayton Glen Zane Ollie Jere Shad Octavio Arturo Lamont Daren Ahmad Christian Arden Scot Shelton" +
                " Rupert Carmine Branden Grant Desmond Roy Lloyd Casey Josef Leo Simon Elroy King Landon Johnnie Walker" +
                " Felix Alexander Sonny Lee Melvin Latrice Alisha Rosamond Georgie Hana Teresia Kimberlie Meg Janeen Armida Nakia Roxann Hollie Zoe Spring" +
                " Jazmine Jaye Verdell Lizbeth Kerri Shonda Idalia Nicki Zina Nada Sherley Franchesca Altha Elizebeth Gertha Carri Argelia" +
                " Loraine Paula Brenna Joline Kristie Hettie Betty Nicol Paulita Hazel Fallon Breanna Fonda Margarite Kristy Donella Krystle" +
                " Shenna";

        private static int usersCount = 100;

        public static List<User> CreateData()
        {
            names = strNames.Split(null).ToList();

            int range = (DateTime.Today - start).Days;

            int range2 = (DateTime.Today - maxLastVisit).Days;

            var users = new List<User>();

            for (int i = 0; i < usersCount; i++)
            {
                users.Add(new User(start.AddDays(random.Next(range)), random.Next(2), maxLastVisit.AddDays(random.Next(range2)), names[random.Next(names.Count)], Convert.ToBoolean(random.Next(1)), i));
            }

            return users;
        }
    }
}
