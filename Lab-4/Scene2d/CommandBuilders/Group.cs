namespace Scene2d.CommandBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using Scene2d.Commands;
    using Scene2d.Exceptions;

    internal class Group : ICommandBuilder
    {
        private readonly char[] _separators = { ' ', ',' };

        private List<string> _figures = new List<string>();

        private string _nameOfGroup;

        private bool _isReady;

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
            try
            {
                var lineArray = line.Split(_separators);
                int iterationsCount = 1;

                while (lineArray[iterationsCount] != "as")
                {
                    if (lineArray[iterationsCount] != string.Empty)
                    {
                        _figures.Add(lineArray[iterationsCount]);
                    }

                    iterationsCount++;
                }

                _nameOfGroup = lineArray[iterationsCount + 1];
                _isReady = true;
            }
            catch (IndexOutOfRangeException)
            {
                throw new BadFormatException();
            }
        }

        public ICommand GetCommand()
        {
            return new GroupCommand(_figures, _nameOfGroup);
        }
    }
}
