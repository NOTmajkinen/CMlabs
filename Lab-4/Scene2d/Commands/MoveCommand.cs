namespace Scene2d.Commands
{
    using Scene2d.Figures;

    public class MoveCommand : ICommand
    {
        private readonly string _name;

        private readonly ScenePoint _vector;

        public MoveCommand(string name, ScenePoint vector)
        {
            _name = name;
            _vector = vector;
        }

        public string FriendlyResultMessage
        {
            get { return "Moved figure '" + _name + "' by (" + _vector.X + ", " + _vector.Y + ")"; }
        }

        public void Apply(Scene scene)
        {
            if (_name == "scene")
            {
                scene.MoveScene(_vector);
            }
            else
            {
                scene.Move(_name, _vector);
            }
        }
    }
}