namespace Scene2d.CommandBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Scene2d.Commands;
    using Scene2d.Exceptions;

    internal class Copy : ICommandBuilder
    {
        private bool _isReady;
        private string _name;
        private string _copyName;

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
            var lineArray = line.Split(" ");

            if (lineArray[2] != "to")
            {
                throw new BadFormatException();
            }
            else
            {
                _name = lineArray[1];
                _copyName = lineArray[3];
                _isReady = true;
            }
        }

        public ICommand GetCommand()
        {
            return new CopyCommand(_name, _copyName);
        }
    }
}
