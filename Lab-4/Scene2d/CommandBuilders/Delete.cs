namespace Scene2d.CommandBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Scene2d.Commands;

    internal class Delete : ICommandBuilder
    {
        private string _name;
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
            var lineArray = line.Split(" ");
            _name = lineArray[1];
            _isReady = true;
        }

        public ICommand GetCommand()
        {
            return new DeleteCommand(_name);
        }
    }
}
