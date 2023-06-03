namespace Calculator
{
    using System;
    using System.Collections.Generic;
    using Calculator.Exceptions;

    public class CalculatorEngine : ICalculatorEngine
    {
        private Dictionary<string, Func<double, double>> _operationsWithOneParameter = new Dictionary<string, Func<double, double>>();
        private Dictionary<string, Func<double, double, double>> _operationsWithTwoParameters = new Dictionary<string, Func<double, double, double>>();
        private Dictionary<string, Func<double, double, double, double>> _operationsWithThreeParameters = new Dictionary<string, Func<double, double, double, double>>();

        public double PerformOperation(Operation operation)
        {
            var operationSign = operation.Sign;

            if (_operationsWithOneParameter.ContainsKey(operationSign) || _operationsWithTwoParameters.ContainsKey(operationSign) || _operationsWithThreeParameters.ContainsKey(operationSign))
            {
                switch (operation.Parameters.Length)
                {
                    case 1:
                        return _operationsWithOneParameter.ContainsKey(operationSign) ? _operationsWithOneParameter[operationSign].Invoke(operation.Parameters[0]) : throw new ParametersCountMismatchException();

                    case 2:
                        return _operationsWithTwoParameters.ContainsKey(operationSign) ? _operationsWithTwoParameters[operationSign].Invoke(operation.Parameters[0], operation.Parameters[1]) : throw new ParametersCountMismatchException();

                    case 3:
                        return _operationsWithThreeParameters.ContainsKey(operationSign) ? _operationsWithThreeParameters[operationSign].Invoke(operation.Parameters[0], operation.Parameters[1], operation.Parameters[2]) : throw new ParametersCountMismatchException();

                    default:
                        throw new ParametersCountMismatchException();
                }
            }
            else
            {
                throw new NotFoundOperationException();
            }
        }

        public void DefineOperation(string sign, Func<double, double, double, double> body)
        {
            if (!_operationsWithThreeParameters.ContainsKey(sign))
            {
                _operationsWithThreeParameters.Add(sign, body);
            }
            else
            {
                throw new AlreadyExistsOperationException(); // throw exception (operation with these parameters already exist)
            }
        }

        public void DefineOperation(string sign, Func<double, double, double> body)
        {
            if (!_operationsWithTwoParameters.ContainsKey(sign))
            {
                _operationsWithTwoParameters.Add(sign, body);
            }
            else
            {
                throw new AlreadyExistsOperationException();
            }
        }

        public void DefineOperation(string sign, Func<double, double> body)
        {
            if (!_operationsWithOneParameter.ContainsKey(sign))
            {
                _operationsWithOneParameter.Add(sign, body);
            }
            else
            {
                throw new AlreadyExistsOperationException();
            }
        }
    }
}
