namespace Scene2d.Figures
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    public class PolygonFigure : IFigure
    {
        private List<ScenePoint> _points;
        private ScenePoint _center;
        private int _pointsCount;

        public PolygonFigure(List<ScenePoint> points)
        {
            _points = points;
            _pointsCount = points.Count;
            _center = DefineTheCenter(points);
        }

        public SceneRectangle CalculateCircumscribingRectangle()
        {
            SceneRectangle sceneRectangle = default(SceneRectangle);

            sceneRectangle.Vertex1 = new ScenePoint(_points.Min(p => p.X), _points.Min(p => p.Y));
            sceneRectangle.Vertex2 = new ScenePoint(_points.Max(p => p.X), _points.Max(p => p.Y));

            return sceneRectangle;
        }

        public object Clone()
        {
            List<ScenePoint> copiedPoints = new List<ScenePoint>();
            foreach (var point in _points)
            {
                copiedPoints.Add(point);
            }

            return new PolygonFigure(copiedPoints);
        }

        public void Draw(ScenePoint origin, Graphics drawing)
        {
            /* Already implemented */

            using (var pen = new Pen(Color.DarkOrchid))
            {
                for (var i = 0; i < _points.Count; i++)
                {
                    ScenePoint firstPoint = _points[i];
                    ScenePoint secondPoint = i >= _points.Count - 1 ? _points.First() : _points[i + 1];

                    drawing.DrawLine(
                        pen,
                        (float)(firstPoint.X - origin.X),
                        (float)(firstPoint.Y - origin.Y),
                        (float)(secondPoint.X - origin.X),
                        (float)(secondPoint.Y - origin.Y));
                }
            }
        }

        public void Move(ScenePoint vector)
        {
            List<ScenePoint> scenePoints = new List<ScenePoint>();

            foreach (var point in _points)
            {
                scenePoints.Add(new ScenePoint(point.X + vector.X, point.Y + vector.Y));
            }

            _points = scenePoints;
            _center = DefineTheCenter(_points);
        }

        public void Reflect(ReflectOrientation orientation)
        {
            if (orientation == ReflectOrientation.Horizontal)
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
            else if (orientation == ReflectOrientation.Vertical)
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
        }

        public void Rotate(double angle)
        {
            List<ScenePoint> rotatedCoordinates = new List<ScenePoint>();

            double radAngle = (angle * Math.PI) / 180;

            foreach (var point in _points)
            {
                rotatedCoordinates.Add(new ScenePoint(_center.X + ((point.X - _center.X) * Math.Cos(radAngle)) - ((point.Y - _center.Y) * Math.Sin(radAngle)), _center.Y + ((point.Y - _center.Y) * Math.Cos(radAngle)) + ((point.X - _center.X) * Math.Sin(radAngle))));
            }

            _points = rotatedCoordinates;
        }

        private ScenePoint DefineTheCenter(List<ScenePoint> points)
        {
            // define the center of the polygon
            double centroidX = 0;
            double centroidY = 0;

            foreach (var point in points)
            {
                centroidX += point.X;
                centroidY += point.Y;
            }

            return new ScenePoint
            {
                X = centroidX / _pointsCount,
                Y = centroidY / _pointsCount,
            };
        }
    }
}