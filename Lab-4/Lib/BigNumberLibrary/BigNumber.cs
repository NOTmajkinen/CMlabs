namespace BigNumberLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A struct that can work with Big Integer numbers with unlimited bit depth.
    /// </summary>
    public struct BigNumber
    {
        private const int Base = 1000000000;
        private readonly List<int> _number;
        private int _digitsCount;
        private bool _isNegative;

        private BigNumber(List<int> number, bool isNegative, int length)
        {
            _number = number;
            _digitsCount = length;
            _isNegative = isNegative;
        }

        private int this[int index]
        {
            get => _number[index];

            set => _number[index] = value;
        }

        /// <summary>
        /// A method that add two BigNumber numbers.
        /// </summary>
        /// <param name="left">The first argument.</param>
        /// <param name="right">The second argument.</param>
        /// <returns>The sum of these numbers.</returns>
        public static BigNumber operator +(BigNumber left, BigNumber right)
        {
            if (left[0] == 0 && right[0] != 0)
            {
                return right;
            }
            else if (left[0] != 0 && right[0] == 0)
            {
                return left;
            }

            int maxLength = left._digitsCount > right._digitsCount ? left._digitsCount : right._digitsCount;

            if (left._isNegative && !right._isNegative)
            {
                if (maxLength == left._digitsCount)
                {
                    BigNumber output = Substraction(left, right);
                    output._isNegative = true;
                    return output;
                }
                else if (maxLength == right._digitsCount)
                {
                    BigNumber output = Substraction(right, left);
                    output._isNegative = false;
                    return output;
                }
            }
            else if (!left._isNegative && right._isNegative)
            {
                if (maxLength == left._digitsCount)
                {
                    BigNumber output = Substraction(left, right);
                    output._isNegative = false;
                    return output;
                }
                else if (maxLength == right._digitsCount)
                {
                    BigNumber output = Substraction(right, left);
                    output._isNegative = true;
                    return output;
                }
            }
            else if (left._isNegative && right._isNegative)
            {
                BigNumber output = Addition(left, right);
                output._isNegative = true;
                return output;
            }

            return Addition(left, right);
        }

        /// <summary>
        /// A method that substract two BigNumber numbers.
        /// </summary>
        /// <param name="left">The first argument.</param>
        /// <param name="right">The second argument.</param>
        /// <returns>The difference of these numbers.</returns>
        public static BigNumber operator -(BigNumber left, BigNumber right)
        {
            if (left._isNegative && !right._isNegative)
            {
                if (left._digitsCount > right._digitsCount)
                {
                    BigNumber output = Addition(left, right);
                    output._isNegative = true;
                    return output;
                }

                if (left._digitsCount < right._digitsCount)
                {
                    BigNumber output = Substraction(right, left);
                    output._isNegative = false;
                    return output;
                }
            }
            else if (!left._isNegative && right._isNegative)
            {
                if (left._digitsCount > right._digitsCount)
                {
                    BigNumber output = Addition(left, right);
                    output._isNegative = false;
                    return output;
                }

                if (left._digitsCount < right._digitsCount)
                {
                    BigNumber output = Substraction(right, left);
                    output._isNegative = true;
                    return output;
                }
            }
            else if (left._isNegative && right._isNegative)
            {
                if (left._digitsCount > right._digitsCount)
                {
                    BigNumber output = Substraction(left, right);
                    output._isNegative = true;
                    return output;
                }

                if (left._digitsCount < right._digitsCount)
                {
                    BigNumber output = Addition(right, left);
                    output._isNegative = false;
                    return output;
                }
            }

            if (left < right)
            {
                BigNumber output = Substraction(right, left);
                output._isNegative = true;
                return output;
            }

            return Substraction(left, right);
        }

        /// <summary>
        /// A method that multiply two BigNumber numbers.
        /// </summary>
        /// <param name="left">The first argument.</param>
        /// <param name="right">The second argument.</param>
        /// <returns>The product of these numbers.</returns>
        public static BigNumber operator *(BigNumber left, BigNumber right)
        {
            if (left == Parse("0") || right == Parse("0"))
            {
                return Parse("0");
            }

            if ((left[0] == 1 && left._digitsCount == 1) && right[0] != 1)
            {
                if (left._isNegative && right._isNegative)
                {
                    right._isNegative = false;
                }

                if (left._isNegative && !right._isNegative)
                {
                    right._isNegative = true;
                }

                return right;
            }

            if (left[0] != 1 && (right[0] == 1 && right._digitsCount == 1))
            {
                if (left._isNegative && right._isNegative)
                {
                    left._isNegative = false;
                }

                if (!left._isNegative && right._isNegative)
                {
                    left._isNegative = true;
                }

                return left;
            }

            if ((!left._isNegative && right._isNegative) || (left._isNegative && !right._isNegative))
            {
                BigNumber output = Multiplication(left, right);
                output._isNegative = true;
                return output;
            }

            if (left._isNegative && right._isNegative)
            {
                BigNumber output = Multiplication(left, right);
                output._isNegative = false;
                return output;
            }

            return Multiplication(left, right);
        }

        /// <summary>
        /// A method that divide two BigNumber numbers.
        /// </summary>
        /// <param name="left">The first argument.</param>
        /// <param name="right">The second argument.</param>
        /// <returns>The quotient of these numbers.</returns>
        public static BigNumber operator /(BigNumber left, BigNumber right)
        {
            if (left[0] == 0)
            {
                return Parse("0");
            }

            if (right[0] == 0)
            {
                throw new DivideByZeroException();
            }

            if ((left._isNegative && !right._isNegative) || (!left._isNegative && right._isNegative))
            {
                BigNumber output = Division(left, right);
                output._isNegative = true;
                return output;
            }

            if (left._isNegative && right._isNegative)
            {
                BigNumber output = Division(left, right);
                output._isNegative = false;
                return output;
            }

            return Division(left, right);
        }

        /// <summary>
        /// A method that calculate the remainder of the division of two BigNumber numbers.
        /// </summary>
        /// <param name="left">The first argument.</param>
        /// <param name="right">The second argument.</param>
        /// <returns>The remainder of the division fo these numbers.</returns>
        public static BigNumber operator %(BigNumber left, BigNumber right)
        {
            BigNumber result = left - Parse(((left / right) * right).ToString());

            if (result._isNegative)
            {
                result += right;
            }

            return result;
        }

        /// <summary>
        /// A method that performs bitwise shift of the BigNumber number to the left.
        /// </summary>
        /// <param name="left">Argument.</param>
        /// <param name="shift">Shift.</param>
        /// <returns>The result of the bitwise shift to the left.</returns>
        public static BigNumber operator <<(BigNumber left, int shift)
        {
            return left * Power(Parse("2"), shift);
        }

        /// <summary>
        /// A method that performs bitwise shift of the BigNumber number to the right.
        /// </summary>
        /// <param name="left">Argument.</param>
        /// <param name="shift">Shift.</param>
        /// <returns>The result of the bitwise shift to the right.</returns>
        public static BigNumber operator >>(BigNumber left, int shift)
        {
            BigNumber number = Parse("2");
            if (left._isNegative)
            {
                return Parse(((left - Parse("1")) / Power(number, shift)).ToString());
            }
            else
            {
                return Parse((left / Power(number, shift)).ToString());
            }
        }

        /// <summary>
        /// A method that defines if BigNumber numbers are equal.
        /// </summary>
        /// <param name="left">The first argument.</param>
        /// <param name="right">The second argument.</param>
        /// <returns>Is the statement true or false.</returns>
        public static bool operator ==(BigNumber left, BigNumber right)
        {
            if ((left._isNegative && !right._isNegative) || (left._isNegative && !right._isNegative))
            {
                return false;
            }
            else if ((left._digitsCount > right._digitsCount) || (left._digitsCount < right._digitsCount))
            {
                return false;
            }
            else
            {
                for (int i = 0; i < left._number.Count; i++)
                {
                    if (left[i] != right[i])
                    {
                        return false;
                    }
                    else
                    {
                        continue;
                    }
                }

                return true;
            }
        }

        /// <summary>
        /// A method that defines if BigNumber numbers aren't equal.
        /// </summary>
        /// <param name="left">The first argument.</param>
        /// <param name="right">The second argument.</param>
        /// <returns>Is the statement true or false.</returns>
        public static bool operator !=(BigNumber left, BigNumber right)
        {
            return !(left == right);
        }

        /// <summary>
        /// A method that defines if The first argument is larger than The second argument.
        /// </summary>
        /// <param name="left">The first argument.</param>
        /// <param name="right">The second argument.</param>
        /// <returns>Is the statement true or false.</returns>
        public static bool operator >(BigNumber left, BigNumber right)
        {
            if (left != right)
            {
                if (Max(left, right) == left)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// A method that defines if The first argument is less than The second argument.
        /// </summary>
        /// <param name="left">The first argument.</param>
        /// <param name="right">The second argument.</param>
        /// <returns>Is the statement true or false.</returns>
        public static bool operator <(BigNumber left, BigNumber right)
        {
            if (left != right)
            {
                return !(left > right);
            }

            return false;
        }

        /// <summary>
        /// A method that defines if The first argument is equal or larger than The second argument.
        /// </summary>
        /// <param name="left">The first argument.</param>
        /// <param name="right">The second argument.</param>
        /// <returns>Is the statement true or false.</returns>
        public static bool operator >=(BigNumber left, BigNumber right)
        {
            if (left > right || left == right)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// A method that defines if The first argument is equal or less than The second argument.
        /// </summary>
        /// <param name="left">The first argument.</param>
        /// <param name="right">The second argument.</param>
        /// <returns>Is the statement true or false.</returns>
        public static bool operator <=(BigNumber left, BigNumber right)
        {
            if (left < right || left == right)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// A method that defines what BigNumber number is larger.
        /// </summary>
        /// <param name="left">The first argument.</param>
        /// <param name="right">The second argument.</param>
        /// <returns>Larger BigNumber number.</returns>
        public static BigNumber Max(BigNumber left, BigNumber right)
        {
            if (left._isNegative && right._isNegative)
            {
                return left._number.Last() > right._number.Last() ? left : right;
            }

            if (left._isNegative && !right._isNegative)
            {
                return right;
            }
            else if (!left._isNegative && right._isNegative)
            {
                return left;
            }

            if (left._digitsCount > right._digitsCount)
            {
                return left;
            }
            else if (left._digitsCount < right._digitsCount)
            {
                return right;
            }
            else
            {
                return left._number.Last() > right._number.Last() ? left : right;
            }
        }

        /// <summary>
        /// A method that performs exponentation of the number.
        /// </summary>
        /// <param name="number">Argument.</param>
        /// <param name="exponent">Exponent.</param>
        /// <returns>The result of the exponentation.</returns>
        public static BigNumber Power(BigNumber number, int exponent)
        {
            if (exponent == 0)
            {
                return Parse("1");
            }

            BigNumber result = number;
            for (int i = 1; i < exponent; i++)
            {
                result *= number;
            }

            return result;
        }

        /// <summary>
        /// A method that parse a string to BigNumber.
        /// </summary>
        /// <param name="input">Argument.</param>
        /// <returns>BigNumber number.</returns>
        public static BigNumber Parse(string input)
        {
            bool isNegative = input[0] == '-';

            int length = isNegative ? input.Length - 1 : input.Length;

            if (isNegative)
            {
                input = input.Substring(1);
            }

            // if there are zeros before the number it would remove it
            input = RemoveLeadingZeros(input);
            length = input.Length;

            // "long" number represented by int array
            List<int> digitsArray = new List<int>();

            for (int i = length; i > 0; i -= 9)
            {
                if (i < 9)
                {
                    digitsArray.Add(Convert.ToInt32(input.Substring(0, i)));
                }
                else
                {
                    digitsArray.Add(Convert.ToInt32(input.Substring(i - 9, 9)));
                }
            }

            return new BigNumber(digitsArray, isNegative, length);
        }

        /// <summary>
        /// A method that convert a BigNumber number to string.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            string result = string.Empty;

            result += _number.Last();

            for (int i = _number.Count - 2; i >= 0; i--)
            {
                result += _number[i].ToString("000000000");
            }

            result = RemoveLeadingZeros(result);

            if (_isNegative)
            {
                result = '-' + result;
            }

            return result;
        }

        private static string RemoveLeadingZeros(string number)
        {
            if (number[0] == '0')
            {
                number = number.TrimStart('0');
                if (number == string.Empty)
                {
                    number = "0";
                }

                return number;
            }

            return number;
        }

        private static BigNumber Addition(BigNumber left, BigNumber right)
        {
            // auxiliary variables
            int carry = 0;
            BigNumber result = new BigNumber(ArrayCopy(left._number), false, left._digitsCount);

            // calculations
            for (int i = 0; i < Math.Max(result._number.Count, right._number.Count) || carry > 0; i++)
            {
                if (i == result._number.Count)
                {
                    result._number.Add(0);
                }

                result[i] += carry + (i < right._number.Count ? right[i] : 0);
                carry = (byte)(result[i] >= Base ? 1 : 0);
                if (carry > 0)
                {
                    result[i] -= Base;
                }
            }

            return result;
        }

        private static BigNumber Substraction(BigNumber left, BigNumber right)
        {
            // auxiliary variables
            int carry = 0;
            BigNumber result = new BigNumber(ArrayCopy(left._number), false, left._digitsCount);

            // calculations
            for (int i = 0; i < right._number.Count || carry > 0; i++)
            {
                result[i] -= carry + (i < right._number.Count ? right[i] : 0);
                carry = result[i] < 0 ? 1 : 0;
                if (carry > 0)
                {
                    result[i] += Base;
                }
            }

            return result;
        }

        private static BigNumber Multiplication(BigNumber left, BigNumber right)
        {
            // auxiliary variables
            int carry = 0;
            BigNumber result = new BigNumber(ArrayResize(left._number.Count + right._number.Count), false, 0);

            // calculations
            for (int i = 0; i < left._number.Count; i++)
            {
                for (int j = 0; j < right._number.Count || carry > 0; j++)
                {
                    long cur = result[i + j] + (left[i] * 1L * (j < right._number.Count ? right[j] : 0)) + carry;
                    result[i + j] = (int)(cur % Base);
                    carry = (int)(cur / Base);
                }
            }

            result._digitsCount = ToString(result._number).Length;
            return result;
        }

        private static BigNumber Division(BigNumber left, BigNumber right)
        {
            BigNumber result = new BigNumber(ArrayResize(left._number.Count), false, left._number.Count),
                current = new BigNumber(new List<int>() { 0 }, false, 1);

            for (int i = left._number.Count() - 1; i >= 0; i--)
            {
                current[0] = left._number[i];
                current._digitsCount = ToString(current._number).Length;
                int x = 0, l = 0, r = Base;
                while (l <= r)
                {
                    int m = (l + r) / 2;
                    BigNumber t = right * Parse(m.ToString());
                    t = Parse(ToString(t._number));
                    if (t <= current)
                    {
                        x = m;
                        l = m + 1;
                    }
                    else
                    {
                        r = m - 1;
                    }
                }

                result[i] = x;
            }

            return result;
        }

        private static List<int> ArrayResize(int itemsCount)
        {
            List<int> array = new List<int>();

            for (int i = 0; i < itemsCount; i++)
            {
                array.Add(0);
            }

            return array;
        }

        private static List<int> ArrayCopy(List<int> array)
        {
            List<int> copiedArray = new List<int>();

            for (int i = 0; i < array.Count; i++)
            {
                copiedArray.Add(array[i]);
            }

            return copiedArray;
        }

        private static string ToString(List<int> number)
        {
            string result = number.Last().ToString();

            for (int i = number.Count - 2; i >= 0; i--)
            {
                result += number[i].ToString("000000000");
            }

            result = RemoveLeadingZeros(result);

            return result;
        }
    }
}
