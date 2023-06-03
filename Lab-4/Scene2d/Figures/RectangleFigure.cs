namespace Scene2d.Figures
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    public class RectangleFigure : IFigure
    {
        /* We store four rectangle points because after rotation edges could be not parallel to XY axes. */
        private ScenePoint _p1;
        private ScenePoint _p2;
        private ScenePoint _p3;
        private ScenePoint _p4;

        private ScenePoint _center;

        private List<ScenePoint> _points;

        public RectangleFigure(ScenePoint p1, ScenePoint p2)
        {
            _p1 = p1;
            _p2 = new ScenePoint { X = p2.X, Y = p1.Y };
            _p3 = p2;
            _p4 = new ScenePoint { X = p1.X, Y = p2.Y };

            _points = new List<ScenePoint>() { _p1, _p2, _p3, _p4 };

            _center.X = (_p1.X + _p2.X + _p3.X + _p4.X) / 4;
            _center.Y = (_p1.Y + _p2.Y + _p3.Y + _p4.Y) / 4;
        }

        public object Clone()
        {
            /* Should return new Rectangle with the same points as the current one. */
            var copiedFigure = new RectangleFigure(_p1, _p3);
            copiedFigure.DefineProperties(_points);

            return copiedFigure;
        }

        public SceneRectangle CalculateCircumscribingRectangle()
        {
            /* Should calculate the rectangle that wraps current figure and has edges parallel to X and Y */
            SceneRectangle sceneRectangle = default(SceneRectangle);

            sceneRectangle.Vertex1 = new ScenePoint(_points.Min(p => p.X), _points.Min(p => p.Y));
            sceneRectangle.Vertex2 = new ScenePoint(_points.Max(p => p.X), _points.Max(p => p.Y));

            return sceneRectangle;
        }

        public void Move(ScenePoint vector)
        {
            /* Should move all the points of current rectangle */
            List<ScenePoint> scenePoints = new List<ScenePoint>();

            foreach (var point in _points)
            {
                scenePoints.Add(new ScenePoint(point.X + vector.X, point.Y + vector.Y));
            }

            _points = scenePoints;

            DefineProperties(_points);
        }

        public void Rotate(double angle)
        {
            /* Should rotate current rectangle. Rotation origin point is the rectangle center.*/
            List<ScenePoint> rotatedCoordinates = new List<ScenePoint>();

            double radAngle = (angle * Math.PI) / 180;

            foreach (var point in _points)
            {
                rotatedCoordinates.Add(new ScenePoint(_center.X + ((point.X - _center.X) * Math.Cos(radAngle)) - ((point.Y - _center.Y) * Math.Sin(radAngle)), _center.Y + ((point.Y - _center.Y) * Math.Cos(radAngle)) + ((point.X - _center.X) * Math.Sin(radAngle))));
            }

            _points = rotatedCoordinates;

            DefineProperties(_points);
        }

        public void Reflect(ReflectOrientation orientation)
        {
            /* Should reflect the figure. Reflection edge is the rectangle axis of symmetry (horizontal or vertical). */
            if (orientation == ReflectOrientation.Vertical)
            {
                List<ScenePoint> reflectedCoordinates = new List<ScenePoint>();

                double minYBeforeReflection = _points.Min(p => p.Y);

                foreach (var point in _points)
                {
                    reflectedCoordinates.Add(new ScenePoint(point.X, -point.Y));
                }

                _points = reflectedCoordinates;

                double minYAfterReflection = _points.Min(p => p.Y);

                double difference = _center.Y < 0 ? -Math.Abs(minYBeforeReflection - minYAfterReflection) : Math.Abs(minYBeforeReflection - minYAfterReflection);

                Move(new ScenePoint(0, difference));
            }
            else if (orientation == ReflectOrientation.Horizontal)
            {
                List<ScenePoint> reflectedCoordinates = new List<ScenePoint>();

                double minXBeforeReflection = _points.Min(p => p.X);

                foreach (var point in _points)
                {
                    reflectedCoordinates.Add(new ScenePoint(-point.X, point.Y));
                }

                _points = reflectedCoordinates;

                double minXAfterReflection = _points.Min(p => p.X);

                double difference = _center.X < 0 ? -Math.Abs(minXBeforeReflection - minXAfterReflection) : Math.Abs(minXBeforeReflection - minXAfterReflection);

                Move(new ScenePoint(difference, 0));
            }
        }

        public void Draw(ScenePoint origin, Graphics drawing)
        {
            /* Already implemented */

            using (var pen = new Pen(Color.Blue))
            {
                drawing.DrawLine(
                    pen,
                    (float)(_p1.X - origin.X),
                    (float)(_p1.Y - origin.Y),
                    (float)(_p2.X - origin.X),
                    (float)(_p2.Y - origin.Y));

                drawing.DrawLine(
                    pen,
                    (float)(_p2.X - origin.X),
                    (float)(_p2.Y - origin.Y),
                    (float)(_p3.X - origin.X),
                    (float)(_p3.Y - origin.Y));

                drawing.DrawLine(
                    pen,
                    (float)(_p3.X - origin.X),
                    (float)(_p3.Y - origin.Y),
                    (float)(_p4.X - origin.X),
                    (float)(_p4.Y - origin.Y));

                drawing.DrawLine(
                    pen,
                    (float)(_p4.X - origin.X),
                    (float)(_p4.Y - origin.Y),
                    (float)(_p1.X - origin.X),
                    (float)(_p1.Y - origin.Y));
            }
        }

        private void DefineProperties(List<ScenePoint> points)
        {
            _p1 = points[0];
            _p2 = points[1];
            _p3 = points[2];
            _p4 = points[3];

            _points = points;

            _center.X = (_p1.X + _p2.X + _p3.X + _p4.X) / 4;
            _center.Y = (_p1.Y + _p2.Y + _p3.Y + _p4.Y) / 4;
        }
    }
}
