namespace Scene2d.CommandBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Scene2d.Commands;
    using Scene2d.Exceptions;
    using Scene2d.Figures;

    internal class AddPolygonCommandBuilder : ICommandBuilder
    {
        private static readonly Regex RecognizeRegex = new Regex(@"(^add polygon\s*\w+)|(add point\s*\([-]?\d+,\s*[-]?\d+\))|(end polygon)");

        /* Should be set in AppendLine method */
        private IFigure _polygon;

        /* Should be set in AppendLine method */
        private string _name;

        private List<ScenePoint> _points = new List<ScenePoint>();

        private bool _isReady = false;

        public bool IsCommandReady
        {
            get
            {
                if (_isReady)
                {
                    return true;
                }

                return false;
            }
        }

        public void AppendLine(string line)
        {
            Regex pointsRegex = new Regex(@"[-]?\d+");

            // check if line matches the RecognizeRegex
            var match = RecognizeRegex.Match(line);
            var matchValue = match.Value;

            // if it matches select params of polygon
            if (match.Success)
            {
                // define its name
                if (matchValue.Substring(0, 11) == "add polygon")
                {
                    _name = matchValue.Substring(12);
                }

                // define coordinates
                else if (matchValue.Substring(0, 3) == "end")
                {
                    if (_points.Count < 3)
                    {
                        throw new BadPolygonPointNumberException();
                    }
                    else
                    {
                        _polygon = new PolygonFigure(_points);
                        _isReady = true;
                    }
                }
                else
                {
                    var points = pointsRegex.Matches(matchValue).Select(p => Convert.ToDouble(p.Value)).ToList();
                    var curPoint = new ScenePoint(points[0], points[1]);
                    if (_points.Contains(curPoint))
                    {
                        throw new BadPolygonPointException();
                    }

                    _points.Add(curPoint);
                }
            }
            else
            {
                // if it does not match throw UnexpectedEndOfPolygonException
                throw new UnexpectedEndOfPolygonException();
            }
        }

        public ICommand GetCommand()
        {
            return new AddFigureCommand(_name, _polygon);
        }
    }
}