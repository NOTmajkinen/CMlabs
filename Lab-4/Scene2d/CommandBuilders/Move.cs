namespace Scene2d.CommandBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Scene2d.Commands;
    using Scene2d.Exceptions;

    internal class Move : ICommandBuilder
    {
        private static readonly Regex RecognizeRegex = new Regex(@"^move\s*\w+\s*\([-]?\d+,\s*[-]?\d+\)\s*");

        private bool _isReady = false;

        private string _name;

        private ScenePoint _vector;

        public bool IsCommandReady
        {
            get
            {
                if (_isReady)
                {
                    return true;
                }

                return false;
            }
        }

        public void AppendLine(string line)
        {
            var isMatch = RecognizeRegex.IsMatch(line);

            if (isMatch)
            {
                // define its name
                var lineArray = line.Split(" ");
                _name = lineArray[1];
                lineArray[1] = string.Empty;
                line = string.Join(" ", lineArray);

                // define vector
                var coordinateRegex = new Regex(@"[-]?\d+");
                var matches = coordinateRegex.Matches(line).Select(x => Convert.ToDouble(x.Value)).ToList();
                _vector = new ScenePoint(matches[0], matches[1]);
                _isReady = true;
            }
            else
            {
                throw new BadFormatException();
            }
        }

        public ICommand GetCommand()
        {
            return new MoveCommand(_name, _vector);
        }
    }
}
