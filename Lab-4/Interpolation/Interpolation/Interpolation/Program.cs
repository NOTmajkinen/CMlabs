namespace Interpolation
{
    using System;

    internal class Program
    {
        private static void Main(string[] args)
        {
            double[] values = { 0, 2, 1, 4 };

            CommonInterpolator[] interpolators =
            {
            new StepInterpolator(values),
            new LinearInterpolator(values),
            new LangranjInterpolator(values),
            new NewtonInterpolator(values),
            };
            Console.WriteLine("Enter a sample point:");
            double samplePoint = double.Parse(Console.ReadLine());
            Console.WriteLine("Calculating value at sample point: {0}", samplePoint);

            foreach (var interpolator in interpolators)
            {
                Console.WriteLine(
                    "Class {0}: Interpolated value is {1}",
                    interpolator.GetType().Name,
                    interpolator.CalculateValue(samplePoint));
            }
        }
    }
}
