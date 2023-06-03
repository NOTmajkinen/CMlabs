namespace Scene2d.CommandBuilders
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Scene2d.Commands;
    using Scene2d.Exceptions;
    using Scene2d.Figures;

    public class AddRectangleCommandBuilder : ICommandBuilder
    {
        private static readonly Regex RecognizeRegex = new Regex(@"^add rectangle\s*\w+\s*\([-]?\d+,\s*[-]?\d+\)\s*\([-]?\d+,\s*[-]?\d+\)\s*");

        /* Should be set in AppendLine method */
        private IFigure _rectangle;

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

            // if it matches select params of rectangle
            if (isMatch)
            {
                // define its name
                var lineArray = line.Split(" ");
                _name = lineArray[2];
                lineArray[2] = string.Empty;
                line = string.Join(" ", lineArray);

                // define coordinates
                var coordinateRegex = new Regex(@"[-]?\d+");
                var matches = coordinateRegex.Matches(line).Select(x => Convert.ToDouble(x.Value)).ToList();
                if (matches[0] == matches[2] && matches[1] == matches[3])
                {
                    throw new BadRectanglePointException();
                }

                _isReady = true;
                _rectangle = new RectangleFigure(new ScenePoint(matches[0], matches[1]), new ScenePoint(matches[2], matches[3]));
            }
            else
            {
                // if it does not match throw BadFormatException
                throw new BadFormatException();
            }
        }

        public ICommand GetCommand() => new AddFigureCommand(_name, _rectangle);
    }
}