namespace BisectionMethod
{
    using System;

    class Program
    {
        static double F(double x)
        {
            return x * x - 2;
        }

        static double BisectionMethod(double x1, double x2, double eps, out int iterations)
        {
            iterations = 0;
            double x = (x2 + x1) / 2;
            double fReturn = F(x);
            while (Math.Abs(fReturn) > eps)
            {
                if (fReturn > 0)
                {
                    x2 = x;
                }
                else
                {
                    x1 = x;

                }
                x = (x2 + x1) / 2;
                fReturn = F(x);
                iterations++;
            }
            return x;
        }

        static void Main()
        {
            var a = 0.0;
            var b = 2.0;
            var eps = 0.0001;
            int Iterations;
            var y = BisectionMethod(a, b, eps, out Iterations);

            Console.WriteLine("Корень уравнения равен: " + y.ToString("#.####"));
            Console.WriteLine("Количество итераций равно: " + Iterations);
            Console.ReadKey();
        }
    }
}