namespace Scene2d.CommandBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using Scene2d.Commands;
    using Scene2d.Exceptions;

    internal class Rotate : ICommandBuilder
    {
        private static readonly Regex RecognizeRegex = new Regex(@"^rotate\s*\w+\s*\d+");

        private bool _isReady = false;

        private string _name;

        private double _angle;

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
                // define its name, check command and define angle
                var lineArray = line.Split(" ");
                if (lineArray[0] != "rotate")
                {
                    throw new BadFormatException();
                }
                else
                {
                    _name = lineArray[1];
                    _angle = Convert.ToDouble(lineArray[2]);
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
            return new RotateCommand(_name, _angle);
        }
    }
}
