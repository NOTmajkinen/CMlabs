namespace Scene2d.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal class PrintCircumscribingRectangleCommand : ICommand
    {
        private string _name;
        private ScenePoint _theFirstCoordinate;
        private ScenePoint _theSecondCoordinate;

        public PrintCircumscribingRectangleCommand(string name)
        {
            _name = name;
        }

        public string FriendlyResultMessage
        {
            get { return $"Printed circumscribing rectangle for '{_name}': ({_theFirstCoordinate.X}, {_theFirstCoordinate.Y}), ({_theSecondCoordinate.X}, {_theSecondCoordinate.Y})"; }
        }

        public void Apply(Scene scene)
        {
            var circumscribedRectangle = scene.PrintCircumscribingRectangle(_name);
            _theFirstCoordinate = circumscribedRectangle.Vertex1;
            _theSecondCoordinate = circumscribedRectangle.Vertex2;
        }
    }
}
