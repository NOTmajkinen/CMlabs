namespace Scene2d.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal class GroupCommand : ICommand
    {
        private readonly IEnumerable<string> _figures;

        private readonly string _nameOfGroup;

        public GroupCommand(List<string> figures, string nameOfGroup)
        {
            _figures = figures;
            _nameOfGroup = nameOfGroup;
        }

        public string FriendlyResultMessage
        {
            get { return "Grouped figures as '" + _nameOfGroup + "'"; }
        }

        public void Apply(Scene scene)
        {
            scene.CreateCompositeFigure(_nameOfGroup, _figures);
        }
    }
}
