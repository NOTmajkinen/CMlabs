namespace Calculator
{
    using System;
    using System.Globalization;

    public class Evaluator
    {
        private readonly ICalculatorEngine _calculatorEngine;

        private readonly IParser _parser;

        public Evaluator(ICalculatorEngine calculatorEngine, IParser parser)
        {
            _calculatorEngine = calculatorEngine;
            _parser = parser;
        }

        public string Calculate(string inputString)
        {
            var operation = _parser.Parse(inputString);

            var result = _calculatorEngine.PerformOperation(operation);

            return Convert.ToString(result);
        }
    }
}
