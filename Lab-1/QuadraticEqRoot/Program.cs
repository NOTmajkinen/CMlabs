namespace QuadraticEqRoot
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please set the quadratic equation");

            Console.Write("a: ");
            double a = Convert.ToDouble(Console.ReadLine());

            Console.Write("b: ");
            double b = Convert.ToDouble(Console.ReadLine());

            Console.Write("c: ");
            double c = Convert.ToDouble(Console.ReadLine());

            double d = b * b - 4 * a * c;
            if (d > 0)
            {
                double x1 = ((-b - Math.Sqrt(d)) / (2 * a));
                double x2 = ((-b + Math.Sqrt(d)) / (2 * a));
                Console.WriteLine($"The first root is: {x1}");
                Console.WriteLine($"The second root is: {x2}");
            }
            else if (d == 0)
            {
                double x = (-b / (2 * a));
                Console.WriteLine($"The root is: {x}");
            }
            else
            {
                Console.WriteLine("There are no existing roots!");
            }
        }
    }
}
