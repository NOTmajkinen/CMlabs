namespace Scene2d.Figures
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;

    internal class CompositeFigure : ICompositeFigure
    {
        private readonly string _name;
        private IList<IFigure> _childFigures;

        public CompositeFigure(string name, List<IFigure> figures)
        {
            _name = name;
            ChildFigures = figures;
        }

        public IList<IFigure> ChildFigures
        {
            get
            {
                return _childFigures;
            }

            private set
            {
                _childFigures = value;
            }
        }

        public SceneRectangle CalculateCircumscribingRectangle()
        {
            SceneRectangle sceneRectangle = default(SceneRectangle);
            sceneRectangle.Vertex1 = new ScenePoint(ChildFigures.Min(p => p.CalculateCircumscribingRectangle().Vertex1.X), ChildFigures.Min(p => p.CalculateCircumscribingRectangle().Vertex1.Y));
            sceneRectangle.Vertex2 = new ScenePoint(ChildFigures.Max(p => p.CalculateCircumscribingRectangle().Vertex2.X), ChildFigures.Max(p => p.CalculateCircumscribingRectangle().Vertex2.Y));
            return sceneRectangle;
        }

        public object Clone()
        {
            List<IFigure> copiedChildFigures = new List<IFigure>();
            foreach (var figure in ChildFigures)
            {
                copiedChildFigures.Add((IFigure)figure.Clone());
            }

            return new CompositeFigure(_name, copiedChildFigures);
        }

        public void Draw(ScenePoint origin, Graphics drawing)
        {
            foreach (var figure in ChildFigures)
            {
                figure.Draw(origin, drawing);
            }
        }

        public void Move(ScenePoint vector)
        {
            List<IFigure> movedGroup = new List<IFigure>();

            IFigure currentFigure;

            foreach (var figure in ChildFigures)
            {
                currentFigure = figure;
                currentFigure.Move(vector);
                movedGroup.Add(currentFigure);
            }

            ChildFigures = movedGroup;
        }

        public void Reflect(ReflectOrientation orientation)
        {
            List<IFigure> reflectedGroup = new List<IFigure>();

            IFigure currentFigure;

            foreach (var figure in ChildFigures)
            {
                currentFigure = figure;
                currentFigure.Reflect(orientation);
                reflectedGroup.Add(currentFigure);
            }

            ChildFigures = reflectedGroup;
        }

        public void Rotate(double angle)
        {
            List<IFigure> rotatedGroup = new List<IFigure>();

            IFigure currentFigure;

            foreach (var figure in ChildFigures)
            {
                currentFigure = figure;
                currentFigure.Rotate(angle);
                rotatedGroup.Add(currentFigure);
            }

            ChildFigures = rotatedGroup;
        }
    }
}