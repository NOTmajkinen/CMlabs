namespace Scene2d.CommandBuilders
{
    using System;
    using System.Text.RegularExpressions;
    using Scene2d.Commands;
    using Scene2d.Exceptions;

    internal class PrintCircumscribingRectangle : ICommandBuilder
    {
        private string _name;

        private Regex _recognizeRegex = new Regex(@"^print\s*circumscribing\s*rectangle\s*for\s*\w+");

        public bool IsCommandReady
        {
            get
            {
                return true;
            }
        }

        public void AppendLine(string line)
        {
            if (_recognizeRegex.IsMatch(line))
            {
                var lineArray = line.Split(" ");
                _name = lineArray[4];
            }
            else
            {
                throw new BadFormatException();
            }
        }

        public ICommand GetCommand()
        {
            return new PrintCircumscribingRectangleCommand(_name);
        }
    }
}
