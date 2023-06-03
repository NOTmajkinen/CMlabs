namespace Interpolation
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal class NewtonInterpolator : CommonInterpolator
    {
        public NewtonInterpolator(double[] values)
            : base(values)
        {
        }

        public override double CalculateValue(double x)
        {
            double result = Ci(0, Values);
            for (int i = 1; i < Values.Length; i++)
            {
                double temp = Ci(i, Values);
                for (int j = 0; j < i; j++)
                {
                    temp *= x - j;
                }

                result += temp;
            }

            return result;
        }

        private static double Ci(int i, double[] values)
        {
            if (i == 0)
            {
                return values[0];
            }

            double delta = Delta(i, i, values);
            return delta / Factorial(i);
        }

        private static double Delta(int p, int i, double[] values)
        {
            if (p == 1)
            {
                return values[i] - values[i - 1];
            }

            return Delta(p - 1, i, values) - Delta(p - 1, i - 1, values);
        }

        private static double Factorial(double value)
        {
            if (value == 0)
            {
                return 1;
            }

            return value * Factorial(value - 1);
        }
    }
}
