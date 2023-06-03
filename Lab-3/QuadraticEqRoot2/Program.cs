namespace QuadraticEqRoot2
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using QuadraticEqRoot2;

    internal class Program
    {
        private static void Main()
        {
            const string outputPath = "output.txt";

            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }

            using (var input = File.OpenText("input.txt"))
            using (var output = File.CreateText(outputPath))
            {
                string line;

                while ((line = input.ReadLine()) != null)
                {
                    var lineSplit = line.Split(',').Select(num =>
                    double.Parse(num, CultureInfo.InvariantCulture)).ToArray();

                    double a = lineSplit[0];
                    double b = lineSplit[1];
                    double c = lineSplit[2];

                    if (a == 0)
                    {
                        output.WriteLine("Error: first number cannot be zero!");
                    }
                    else
                    {
                        IList<double> roots = Solver.Solve(a, b, c);

                        string outputLine = string.Empty;

                        if (roots.Count == 2)
                        {
                            output.WriteLine("{0},{1}", roots.Max().ToString(CultureInfo.InvariantCulture), roots.Min().ToString(CultureInfo.InvariantCulture));
                        }
                        else if (roots.Count == 1)
                        {
                            output.WriteLine("{0}", roots[0].ToString(CultureInfo.InvariantCulture));
                        }
                        else
                        {
                            output.WriteLine(outputLine);
                        }
                    }
                }
            }
        }
    }
}
