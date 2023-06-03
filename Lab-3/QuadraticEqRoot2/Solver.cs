namespace QuadraticEqRoot2
{
    using System;
    using System.Collections.Generic;

    public static class Solver
    {
        public static IList<double> Solve(double a, double b, double c)
        {
            double d = (b * b) - (4d * a * c);

            if (Math.Abs(d) < 0.0001)
            {
                double x = -b / 2d / a;
                return new List<double> { x };
            }

            if (d > 0)
            {
                double x1 = (-b + Math.Sqrt(d)) / 2d / a;
                double x2 = (-b - Math.Sqrt(d)) / 2d / a;
                return new List<double> { x1, x2 };
            }

            return new List<double>();
        }
    }
}