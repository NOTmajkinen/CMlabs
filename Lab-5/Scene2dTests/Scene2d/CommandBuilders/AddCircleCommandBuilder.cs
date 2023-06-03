namespace Scene2d.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Scene2d.CommandBuilders;
    using Scene2d.Exceptions;
    using Scene2d.Figures;

    internal class AddCircleCommandBuilder : ICommandBuilder
    {
        private static readonly Regex RecognizeRegex = new Regex(@"^add circle\s*\w+\s*\([-]?\d+,\s*[-]?\d+\)\s*radius\s*[-]?\d+");

        /* Should be set in AppendLine method */
        private IFigure _circle;

        /* Should be set in AppendLine method */
        private string _name;

        private bool _isReady = false;

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
            // check if line matches the RecognizeRegex
            var isMatch = RecognizeRegex.IsMatch(line);

            // if it matches select params of circle
            if (isMatch)
            {
                // define its name
                var lineArray = line.Split(" ");
                _name = lineArray[2];
                lineArray[2] = string.Empty;
                line = string.Join(" ", lineArray);

                // define coordinates and radius
                var coordinateRegex = new Regex(@"[-]?\d+");
                var matches = coordinateRegex.Matches(line).Select(x => Convert.ToDouble(x.Value)).ToList();
                if (matches[2] <= 0)
                {
                    _isReady = false;
                    throw new BadCircleRadiusException();
                }

                _circle = new CircleFigure(new ScenePoint(matches[0], matches[1]), matches[2]);
                _isReady = true;
            }
            else
            {
                // if it does not match throw BadFormatException
                throw new BadFormatException();
            }
        }

        public ICommand GetCommand() => new AddFigureCommand(_name, _circle);
    }
}
