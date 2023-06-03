namespace BigNumber_Library
{
    using System;
    using BigNumberLibrary;

    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Review on BigNumber Class Library");
            Console.Write("Input the first number: ");
            BigNumber a = BigNumber.Parse(Console.ReadLine());
            Console.Write("Input the second number: ");
            BigNumber b = BigNumber.Parse(Console.ReadLine());
            Console.WriteLine("This library can perform all common operations:\n('+', '-', '*', '/', '%')");
            BigNumberOperations(a, b);
            Console.WriteLine("Also it has Boolean operators('==', '!=', '>', '<', '>=', '<=')");
            BigNumberBoolean(a, b);
            Console.WriteLine("Other functions: Power(), Max(), <<, >>");
            OtherBigNumberOperations(a, b);
        }

        private static void BigNumberOperations(BigNumber left, BigNumber right)
        {
            Console.Read();
            BigNumber add = left + right;
            Console.WriteLine("For example, we want to perform addition: Number1 + Number2 = " + add);
            BigNumber sub = left - right;
            Console.WriteLine("Substraction: Number1 - Number2 = " + sub);
            BigNumber mul = left * right;
            Console.WriteLine("Multiplication: Number1 * Number2 = " + mul);
            BigNumber div = left / right;
            Console.WriteLine("Division: " + div);
            BigNumber rem = left % right;
            Console.WriteLine("Remainder of the division: " + rem);
        }

        private static void BigNumberBoolean(BigNumber left, BigNumber right)
        {
            Console.Read();
            Console.WriteLine("==: " + (left == right));
            Console.WriteLine("!=: " + (left != right));
            Console.WriteLine(">: " + (left > right));
            Console.WriteLine("<: " + (left < right));
            Console.WriteLine(">=: " + (left >= right));
            Console.WriteLine("<=: " + (left <= right));
        }

        private static void OtherBigNumberOperations(BigNumber left, BigNumber right)
        {
            Console.Read();
            BigNumber max = BigNumber.Max(left, right);
            Console.WriteLine("Max(): " + max);
            Console.Read();
            Console.Write("Other operations use integers, so we must change the second number: ");
            int right1 = Convert.ToInt32(Console.ReadLine());
            BigNumber pow = BigNumber.Power(left, right1);
            BigNumber bitwiseLeft = left << right1;
            BigNumber bitwiseRight = left >> right1;
            Console.WriteLine($"Number1^{right1}: " + pow);
            Console.WriteLine("Bitwise shift left: " + bitwiseLeft);
            Console.WriteLine("Bitwise shift right: " + bitwiseRight);
        }
    }
}
