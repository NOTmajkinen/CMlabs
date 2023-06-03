namespace Scene2d.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal class DeleteCommand : ICommand
    {
        private string _name;

        public DeleteCommand(string name)
        {
            _name = name;
        }

        public string FriendlyResultMessage
        {
            get { return "Deleted figure '" + _name + "'"; }
        }

        public void Apply(Scene scene)
        {
            if (_name == "scene")
            {
                scene.DeleteScene();
            }
            else
            {
                scene.Delete(_name);
            }
        }
    }
}
