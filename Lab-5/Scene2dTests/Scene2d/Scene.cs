namespace Scene2d
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Scene2d.Exceptions;
    using Scene2d.Figures;

    public class Scene
    {
        // keeps figures
        private Dictionary<string, IFigure> _figures = new Dictionary<string, IFigure>();

        // keeps composite figures
        private Dictionary<string, ICompositeFigure> _compositeFigures = new Dictionary<string, ICompositeFigure>();

        public Dictionary<string, IFigure> Figures
        {
            get
            {
                return _figures;
            }
        }

        public Dictionary<string, ICompositeFigure> CompositeFigures
        {
            get
            {
                return _compositeFigures;
            }
        }

        public void AddFigure(string name, IFigure figure)
        {
            if (!_figures.ContainsKey(name))
            {
                _figures[name] = figure;
            }
            else
            {
                throw new NameDoesAlreadyExistException();
            }
        }

        public SceneRectangle CalculateSceneCircumscribingRectangle()
        {
            var allFigures = ListDrawableFigures()
                .Select(f => f.CalculateCircumscribingRectangle())
                .SelectMany(a => new[] { a.Vertex1, a.Vertex2 })
                .ToList();

            if (allFigures.Count == 0)
            {
                return default;
            }

            return new SceneRectangle
            {
                Vertex1 = new ScenePoint(allFigures.Min(p => p.X), allFigures.Min(p => p.Y)),
                Vertex2 = new ScenePoint(allFigures.Max(p => p.X), allFigures.Max(p => p.Y)),
            };
        }

        public void CreateCompositeFigure(string name, IEnumerable<string> childFigures)
        {
            /* Should create a group figure. */
            List<IFigure> groupFigures = new List<IFigure>();

            foreach (var identificator in childFigures)
            {
                if (_figures.ContainsKey(identificator))
                {
                    groupFigures.Add(_figures[identificator]);
                }
                else
                {
                    throw new NameDoesNotExistException();
                }
            }

            if (!_compositeFigures.ContainsKey(name))
            {
                _compositeFigures.Add(name, new CompositeFigure(name, groupFigures));
            }
            else
            {
                throw new NameDoesAlreadyExistException();
            }
        }

        public SceneRectangle CalculateCircumscribingRectangle(string name)
        {
            /* Should calculate the rectangle that wraps figure or group 'name' */
            SceneRectangle sceneRectangle = default(SceneRectangle);

            if (_figures.ContainsKey(name))
            {
                var figure = _figures[name];
                sceneRectangle.Vertex1 = figure.CalculateCircumscribingRectangle().Vertex1;
                sceneRectangle.Vertex2 = figure.CalculateCircumscribingRectangle().Vertex2;
            }
            else if (_compositeFigures.ContainsKey(name))
            {
                var figure = _compositeFigures[name];
                sceneRectangle.Vertex1 = figure.CalculateCircumscribingRectangle().Vertex1;
                sceneRectangle.Vertex2 = figure.CalculateCircumscribingRectangle().Vertex2;
            }
            else
            {
                throw new NameDoesNotExistException();
            }

            return sceneRectangle;
        }

        public SceneRectangle PrintCircumscribingRectangle(string name)
        {
            if (name == "scene")
            {
                var theFirstCoordinate = new ScenePoint(
                    _figures.Min(figure => figure.Value.CalculateCircumscribingRectangle().Vertex1.X),
                    _figures.Min(figure => figure.Value.CalculateCircumscribingRectangle().Vertex1.Y));
                var theSecondCoordinate = new ScenePoint(
                    _figures.Max(figure => figure.Value.CalculateCircumscribingRectangle().Vertex2.X),
                    _figures.Max(figure => figure.Value.CalculateCircumscribingRectangle().Vertex2.Y));

                SceneRectangle sceneRectangle = default(SceneRectangle);

                sceneRectangle.Vertex1 = theFirstCoordinate;
                sceneRectangle.Vertex2 = theSecondCoordinate;

                return sceneRectangle;
            }
            else if (_figures.ContainsKey(name))
            {
                var figure = _figures[name];

                var theFirstCordinate = figure.CalculateCircumscribingRectangle().Vertex1;
                var theSecondCoordinate = figure.CalculateCircumscribingRectangle().Vertex2;

                SceneRectangle sceneRectangle = default(SceneRectangle);

                sceneRectangle.Vertex1 = theFirstCordinate;
                sceneRectangle.Vertex2 = theSecondCoordinate;

                return sceneRectangle;
            }
            else if (_compositeFigures.ContainsKey(name))
            {
                var compositeFigure = _compositeFigures[name];

                var theFirstCordinate = compositeFigure.CalculateCircumscribingRectangle().Vertex1;
                var theSecondCoordinate = compositeFigure.CalculateCircumscribingRectangle().Vertex2;

                SceneRectangle sceneRectangle = default(SceneRectangle);

                sceneRectangle.Vertex1 = theFirstCordinate;
                sceneRectangle.Vertex2 = theSecondCoordinate;

                return sceneRectangle;
            }
            else
            {
                throw new NameDoesNotExistException();
            }
        }

        public void MoveScene(ScenePoint vector)
        {
            /* Should move all the figures and groups in the scene by 'vector' */
            Dictionary<string, IFigure> currentFigures = new Dictionary<string, IFigure>();
            Dictionary<string, ICompositeFigure> currentGroups = new Dictionary<string, ICompositeFigure>();

            foreach (var figure in _figures)
            {
                figure.Value.Move(vector);
                currentFigures.Add(figure.Key, figure.Value);
            }

            foreach (var group in _compositeFigures)
            {
                group.Value.Move(vector);
                currentGroups.Add(group.Key, group.Value);
            }

            _figures = currentFigures;
            _compositeFigures = currentGroups;
        }

        public void Move(string name, ScenePoint vector)
        {
            /* Should move figure or group 'name' by 'vector' */
            if (_figures.ContainsKey(name))
            {
                var figure = _figures[name];
                figure.Move(vector);
                _figures[name] = figure;
            }
            else if (_compositeFigures.ContainsKey(name))
            {
                var group = _compositeFigures[name];
                group.Move(vector);
                _compositeFigures[name] = group;
            }
            else
            {
                throw new NameDoesNotExistException();
            }
        }

        public void RotateScene(double angle)
        {
            /* Should rotate all figures and groups in the scene by 'angle' */
            Dictionary<string, IFigure> currentFigures = new Dictionary<string, IFigure>();
            Dictionary<string, ICompositeFigure> currentGroups = new Dictionary<string, ICompositeFigure>();

            foreach (var figure in _figures)
            {
                figure.Value.Rotate(angle);
                currentFigures.Add(figure.Key, figure.Value);
            }

            foreach (var group in _compositeFigures)
            {
                group.Value.Rotate(angle);
                currentGroups.Add(group.Key, group.Value);
            }

            _figures = currentFigures;
            _compositeFigures = currentGroups;
        }

        public void Rotate(string name, double angle)
        {
            /* Should rotate figure or group 'name' by 'angle' */
            if (_figures.ContainsKey(name))
            {
                var figure = _figures[name];
                figure.Rotate(angle);
                _figures[name] = figure;
            }
            else if (_compositeFigures.ContainsKey(name))
            {
                var group = _compositeFigures[name];
                group.Rotate(angle);
                _compositeFigures[name] = group;
            }
            else
            {
                throw new NameDoesNotExistException();
            }
        }

        public IEnumerable<IFigure> ListDrawableFigures()
        {
            return _figures
                .Values
                .Concat(_compositeFigures.SelectMany(x => x.Value.ChildFigures))
                .Distinct();
        }

        public void CopyScene(string copyName)
        {
            /* Should copy the entire scene to a group named 'copyName' */
            List<IFigure> figures = new List<IFigure>();

            foreach (var figure in _figures)
            {
                figures.Add((IFigure)figure.Value.Clone());
            }

            _compositeFigures.Add(copyName, new CompositeFigure(copyName, figures));
        }

        public void Copy(string originalName, string copyName)
        {
            if (_figures.ContainsKey(originalName))
            {
                var figure = _figures[originalName];
                if (!_figures.ContainsKey(copyName))
                {
                    _figures.Add(copyName, (IFigure)figure.Clone());
                }
                else
                {
                    throw new NameDoesAlreadyExistException();
                }
            }
            else if (_compositeFigures.ContainsKey(originalName))
            {
                var group = _compositeFigures[originalName];
                _compositeFigures.Add(copyName, (ICompositeFigure)group.Clone());
            }
            else
            {
                throw new NameDoesNotExistException();
            }
        }

        public void DeleteScene()
        {
            /* Should delete all the figures and groups from the scene */
            _figures.Clear();
            _compositeFigures.Clear();
        }

        public void Delete(string name)
        {
            /* Should delete figure or group named 'name' */
            if (_figures.ContainsKey(name))
            {
                _figures.Remove(name);
            }
            else if (_compositeFigures.ContainsKey(name))
            {
                _compositeFigures.Remove(name);
            }
            else
            {
                throw new NameDoesNotExistException();
            }
        }

        public void ReflectScene(ReflectOrientation reflectOrientation)
        {
            /* Should reflect all the figures and groups in the scene */
            Dictionary<string, IFigure> currentFigures = new Dictionary<string, IFigure>();
            Dictionary<string, ICompositeFigure> currentGroups = new Dictionary<string, ICompositeFigure>();

            foreach (var figure in _figures)
            {
                figure.Value.Reflect(reflectOrientation);
                currentFigures.Add(figure.Key, figure.Value);
            }

            foreach (var group in _compositeFigures)
            {
                group.Value.Reflect(reflectOrientation);
                currentGroups.Add(group.Key, group.Value);
            }

            _figures = currentFigures;
            _compositeFigures = currentGroups;
        }

        public void Reflect(string name, ReflectOrientation reflectOrientation)
        {
            /* Should reflect figure or group 'name' */
            if (_figures.ContainsKey(name))
            {
                var figure = _figures[name];
                figure.Reflect(reflectOrientation);
                _figures[name] = figure;
            }
            else if (_compositeFigures.ContainsKey(name))
            {
                var group = _compositeFigures[name];
                group.Reflect(reflectOrientation);
                _compositeFigures[name] = group;
            }
            else
            {
                throw new NameDoesNotExistException();
            }
        }
    }
}
