using System;

namespace Casting
{
    class Program
    {
            public enum Weekday : byte
            {
                Monday,
                Tuesday,
                Wednesday,
                Thursday,
                Friday,
                Saturday = 16,
                Sunday
            }

            [Flags]
            public enum BookAttributes : short
            {
                IsNothing = 0x0,
                IsEducational = 0x1,
                IsDetective = 0x2,
                IsHumoros = 0x4,
                IsMedical = 0x8,
                IsPolitical = 0x10,
                IsEconomical = 0x20
            }


            static void Main(string[] args)
            {
                int a = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Weekday conversion\n {0}", (Weekday)a);
                Console.WriteLine("BookAttributes conversion\n {0}", (BookAttributes)a);

            }

        }
    }
