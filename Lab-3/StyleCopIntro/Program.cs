namespace StyleCopIntro
{
    using System;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var me = new User("John", "Smith");
            Console.WriteLine(me.GetFullName());
        }
    }
}
