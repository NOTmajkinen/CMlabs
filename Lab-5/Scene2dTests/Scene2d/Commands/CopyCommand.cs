namespace Scene2d.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal class CopyCommand : ICommand
    {
        private string _originalName;
        private string _copyName;

        public CopyCommand(string originalName, string copyName)
        {
            _originalName = originalName;
            _copyName = copyName;
        }

        public string FriendlyResultMessage
        {
            get { return "Created copy of '" + _originalName + "' named '" + _copyName + "'"; }
        }

        public void Apply(Scene scene)
        {
           if (_originalName == "scene")
            {
                scene.CopyScene(_copyName);
            }
            else
            {
                scene.Copy(_originalName, _copyName);
            }
        }
    }
}
