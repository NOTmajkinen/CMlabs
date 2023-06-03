namespace Calculator
{
    using System;

    using Calculator.Exceptions;

    public static class Program
    {
        public static void Main()
        {
            ICalculatorEngine calculator = new CalculatorEngine();
            IParser parser = new Parser();

            try
            {
                calculator.DefineOperation("+", (a, b) => a + b);

                calculator.DefineOperation("-", (a, b) => a - b);
                calculator.DefineOperation("-", a => -a);

                calculator.DefineOperation("*", (a, b) => a * b);

                calculator.DefineOperation("/", (a, b) => a / b);

                calculator.DefineOperation("%", (a, b) => a % b);

                calculator.DefineOperation("++", a => a++);

                calculator.DefineOperation("--", a => a--);

                calculator.DefineOperation("^", Math.Pow);

                calculator.DefineOperation("sqrt", Math.Sqrt);

                calculator.DefineOperation("abs", Math.Abs);

                calculator.DefineOperation("cos", Math.Cos);

                calculator.DefineOperation("sin", Math.Sin);

                calculator.DefineOperation("tan", Math.Tan);

                calculator.DefineOperation("round", Math.Round);

                calculator.DefineOperation("max", Math.Max);

                calculator.DefineOperation("min", Math.Min);

                var factorial = new Func<double, double>(x =>
                {
                    double output = 1;

                    for (int i = 2; i <= x; i++)
                    {
                        output *= i;
                    }

                    return output;
                });

                calculator.DefineOperation("factorial", factorial);

                var fibonacci = new Func<double, double>(n =>
                {
                    double[] range = new double[(int)n + 1];

                    range[1] = 1;

                    for (int i = 2; i <= n; i++)
                    {
                        range[i] = range[i - 1] + range[i - 2];
                    }

                    return range[(int)n];
                });

                calculator.DefineOperation("fibonacci", fibonacci);

                var gcd = new Func<double, double, double>((x, y) =>
                {
                    while (y != 0)
                    {
                        double t = y;
                        y = x % y;
                        x = t;
                    }

                    return x;
                });

                calculator.DefineOperation("gcd", gcd);
            }
            catch (AlreadyExistsOperationException)
            {
                Console.WriteLine("This operation already exists in the calculator");
            }

            var evaluator = new Evaluator(calculator, parser);
            Console.WriteLine("Please enter expressions: ");

            while (true)
            {
                string line = Console.ReadLine();
                if (line == null || line.Trim().Length == 0)
                {
                    break;
                }

                try
                {
                    Console.WriteLine(evaluator.Calculate(line));
                }
                catch (NotFoundOperationException)
                {
                    Console.WriteLine("The operation was not found");
                }
                catch (AlreadyExistsOperationException)
                {
                    Console.WriteLine("The operation is already exist");
                }
                catch (IncorrectParametersException)
                {
                    Console.WriteLine("The parameters is incorrect");
                }
                catch (ParametersCountMismatchException)
                {
                    Console.WriteLine("The parameters count mismatch");
                }

                // todo: кажется здесь мы "отловили" только одно
                // исключение NotFoundOperationException,
                // не забудьте отловить оставшиеся
            }
        }
    }
}
