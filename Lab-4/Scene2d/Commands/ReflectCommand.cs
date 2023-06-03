namespace Scene2d.Commands
{
    using Scene2d.Figures;

    public class ReflectCommand : ICommand
    {
        private readonly string _name;

        private readonly ReflectOrientation _orientation;

        public ReflectCommand(string name, ReflectOrientation orientation)
        {
            _name = name;
            _orientation = orientation;
        }

        public string FriendlyResultMessage
        {
            get { return "Reflected " + (_orientation == ReflectOrientation.Horizontal ? "horizontally" : "vertically") + " figure'" + _name + "'"; }
        }

        public void Apply(Scene scene)
        {
            if (_name == "scene")
            {
                scene.ReflectScene(_orientation);
            }
            else
            {
                scene.Reflect(_name, _orientation);
            }
        }
    }
}
