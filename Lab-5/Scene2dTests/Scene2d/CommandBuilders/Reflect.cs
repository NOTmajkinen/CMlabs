namespace Scene2d.CommandBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Scene2d.Commands;
    using Scene2d.Exceptions;

    internal class Reflect : ICommandBuilder
    {
        private static readonly Regex RecognizeRegex = new Regex(@"^reflect\s*\w+\s*\w+");

        private bool _isReady = false;

        private string _name;

        private ReflectOrientation _orientation;

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
                var lineArray = line.Split(" ");
                try
                {
                    // define its name and type of reflection
                    _name = lineArray[2];
                    lineArray[2] = string.Empty;
                }
                catch (IndexOutOfRangeException)
                {
                    throw new BadFormatException();
                }

                if (lineArray[1] == "vertically" | lineArray[1] == "horizontally")
                {
                    if (lineArray[1] == "vertically")
                    {
                        _orientation = ReflectOrientation.Vertical;
                    }
                    else
                    {
                        _orientation = ReflectOrientation.Horizontal;
                    }
                }
                else
                {
                    throw new BadFormatException();
                }

                _isReady = true;
            }
            else
            {
                throw new BadFormatException();
            }
        }

        public ICommand GetCommand()
        {
            return new ReflectCommand(_name, _orientation);
        }
    }
}
