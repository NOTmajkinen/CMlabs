 namespace Calculator
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using Calculator.Exceptions;

    public class Parser : IParser
    {
        public Operation Parse(string inputString)
        {
            int iterator = 1;

            var splittedString = RemoveEmptyItems(inputString.Split(' '));

            var operationSign = IsDigit(splittedString[0]) ? throw new IncorrectParametersException() : splittedString[0];

            var operationParameters = new List<double>();

            while (iterator < splittedString.Length)
            {
                if (IsDigit(splittedString[iterator]))
                {
                    operationParameters.Add(Convert.ToDouble(splittedString[iterator]));
                }

                iterator++;
            }

            return new Operation(operationSign, operationParameters.Count != 0 ? operationParameters.ToArray() : throw new IncorrectParametersException());
        }

        private bool IsDigit(string value)
        {
            try
            {
                Convert.ToDouble(value);
            }
            catch (FormatException)
            {
                return false;
            }

            return true;
        }

        private string[] RemoveEmptyItems(string[] arr)
        {
            var cleanedArray = new List<string>();

            foreach (var item in arr)
            {
                if (item != string.Empty)
                {
                    cleanedArray.Add(item);
                }
            }

            return cleanedArray.ToArray();
        }
    }
}