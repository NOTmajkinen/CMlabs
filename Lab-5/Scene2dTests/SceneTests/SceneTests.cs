namespace SceneTests
{
    using NUnit.Framework;
    using Scene2d;
    using Scene2d.CommandBuilders;
    using Scene2d.Exceptions;

    public class SceneTests
    {
        [Test]
        public void Command_CanPrintCicrumscribingRectangle()
        {
            CommandProducer commandProducer = new CommandProducer();
            Scene scene = new Scene();

            var circumscribingRectangle = new SceneRectangle
            {
                Vertex1 = new ScenePoint(-10, 0),
                Vertex2 = new ScenePoint(10, 10),
            };

            string commandLine1 = "add polygon P";
            string commandLine2 = "add point (-10, 0)";
            string commandLine3 = "add point (0, 10)";
            string commandLine4 = "add point (10, 0)";
            string commandLine5 = "end polygon";

            commandProducer.AppendLine(commandLine1);
            commandProducer.AppendLine(commandLine2);
            commandProducer.AppendLine(commandLine3);
            commandProducer.AppendLine(commandLine4);
            commandProducer.AppendLine(commandLine5);

            var command = commandProducer.GetCommand();
            command.Apply(scene);

            var circumscribingRectangle1 = scene.Figures["P"].CalculateCircumscribingRectangle();

            var condition =
                circumscribingRectangle.Vertex1.X == circumscribingRectangle1.Vertex1.X && circumscribingRectangle.Vertex1.Y == circumscribingRectangle1.Vertex1.Y &&
                circumscribingRectangle.Vertex2.X == circumscribingRectangle1.Vertex2.X && circumscribingRectangle.Vertex2.Y == circumscribingRectangle1.Vertex2.Y;

            Assert.That(
                condition,
                Is.True,
                "The circumscribing rectangle wasn't created correctly");
        }

        [Test]
        public void Rectangle_CanAddOrDelete()
        {
            CommandProducer commandProducer = new CommandProducer();
            Scene scene = new Scene();

            string commandLine = "add rectangle R (-20, -10) (20, 10)";

            commandProducer.AppendLine(commandLine);

            var command = commandProducer.GetCommand();
            command.Apply(scene);

            Assert.That(
                scene.Figures.ContainsKey("R"),
                Is.True,
                "The figure was not added");

            commandLine = "delete R";

            commandProducer.AppendLine(commandLine);

            command = commandProducer.GetCommand();
            command.Apply(scene);

            Assert.That(
                scene.Figures.ContainsKey("R"),
                Is.False,
                "The figure was not deleted");
        }

        [Test]
        public void Circle_CanAddOrDelete()
        {
            CommandProducer commandProducer = new CommandProducer();
            Scene scene = new Scene();

            string commandLine = "add circle C (0, 0) radius 100";

            commandProducer.AppendLine(commandLine);

            Assert.True(commandProducer.IsCommandReady);

            var command = commandProducer.GetCommand();
            command.Apply(scene);

            Assert.That(
                scene.Figures.ContainsKey("C"),
                Is.True,
                "The figure was not added");

            commandLine = "delete C";

            commandProducer.AppendLine(commandLine);

            command = commandProducer.GetCommand();
            command.Apply(scene);

            Assert.That(
                scene.Figures.ContainsKey("C"),
                Is.False,
                "The figure was not deleted");
        }

        [Test]
        public void Polygon_CanAddOrDelete()
        {
            CommandProducer commandProducer = new CommandProducer();
            Scene scene = new Scene();

            string commandLine1 = "add polygon P";
            string commandLine2 = "add point (-10, 0)";
            string commandLine3 = "add point (0, 10)";
            string commandLine4 = "add point (10, 0)";
            string commandLine5 = "end polygon";

            commandProducer.AppendLine(commandLine1);
            commandProducer.AppendLine(commandLine2);
            commandProducer.AppendLine(commandLine3);
            commandProducer.AppendLine(commandLine4);
            commandProducer.AppendLine(commandLine5);

            var command = commandProducer.GetCommand();
            command.Apply(scene);

            Assert.That(
                scene.Figures.ContainsKey("P"),
                Is.True,
                "The figure was not added");

            var commandLine = "delete P";

            commandProducer.AppendLine(commandLine);

            command = commandProducer.GetCommand();
            command.Apply(scene);

            Assert.That(
                scene.CompositeFigures.ContainsKey("P"),
                Is.False,
                "The figure was not deleted");
        }

        [Test]
        public void CompositeFigure_CanAddOrDelete()
        {
            CommandProducer commandProducer = new CommandProducer();
            Scene scene = new Scene();

            // add the first figure
            string commandLine = "add rectangle R (-20, -10) (20, 10)";

            commandProducer.AppendLine(commandLine);

            var command = commandProducer.GetCommand();
            command.Apply(scene);

            // add the second figure
            commandLine = "add circle C (0, 0) radius 100";

            commandProducer.AppendLine(commandLine);

            command = commandProducer.GetCommand();
            command.Apply(scene);

            // add a third figure
            string commandLine1 = "add polygon P";
            string commandLine2 = "add point (-10, 0)";
            string commandLine3 = "add point (0, 10)";
            string commandLine4 = "add point (10, 0)";
            string commandLine5 = "end polygon";

            commandProducer.AppendLine(commandLine1);
            commandProducer.AppendLine(commandLine2);
            commandProducer.AppendLine(commandLine3);
            commandProducer.AppendLine(commandLine4);
            commandProducer.AppendLine(commandLine5);

            command = commandProducer.GetCommand();
            command.Apply(scene);

            commandLine = "group P, R, C as PRC";

            commandProducer.AppendLine(commandLine);

            command = commandProducer.GetCommand();
            command.Apply(scene);

            Assert.That(
                scene.CompositeFigures.ContainsKey("PRC"),
                Is.True,
                "The group was not created");

            commandLine = "delete PRC";

            commandProducer.AppendLine(commandLine);

            command = commandProducer.GetCommand();
            command.Apply(scene);

            Assert.That(
                 scene.CompositeFigures.ContainsKey("PRC"),
                 Is.False,
                 "The group was not deleted");
        }

        [Test]
        public void Figure_CanCopy()
        {
            CommandProducer commandProducer = new CommandProducer();
            Scene scene = new Scene();

            string commandLine = "add rectangle R (-20, -10) (20, 10)";

            commandProducer.AppendLine(commandLine);

            var command = commandProducer.GetCommand();
            command.Apply(scene);

            commandLine = "copy R to R1";

            commandProducer.AppendLine(commandLine);

            command = commandProducer.GetCommand();
            command.Apply(scene);

            Assert.That(
                scene.Figures.ContainsKey("R") && scene.Figures.ContainsKey("R1"),
                Is.True,
                "The figure was not copied");
        }

        [Test]
        public void CompositeFigure_CanCopy()
        {
            CommandProducer commandProducer = new CommandProducer();
            Scene scene = new Scene();

            // add the first figure
            string commandLine = "add rectangle R (-20, -10) (20, 10)";

            commandProducer.AppendLine(commandLine);

            var command = commandProducer.GetCommand();
            command.Apply(scene);

            // add the second figure
            commandLine = "add circle C (0, 0) radius 100";

            commandProducer.AppendLine(commandLine);

            command = commandProducer.GetCommand();
            command.Apply(scene);

            // add a third figure
            string commandLine1 = "add polygon P";
            string commandLine2 = "add point (-10, 0)";
            string commandLine3 = "add point (0, 10)";
            string commandLine4 = "add point (10, 0)";
            string commandLine5 = "end polygon";

            commandProducer.AppendLine(commandLine1);
            commandProducer.AppendLine(commandLine2);
            commandProducer.AppendLine(commandLine3);
            commandProducer.AppendLine(commandLine4);
            commandProducer.AppendLine(commandLine5);

            command = commandProducer.GetCommand();
            command.Apply(scene);

            commandLine = "group P, R, C as PRC";

            commandProducer.AppendLine(commandLine);

            command = commandProducer.GetCommand();
            command.Apply(scene);

            Assert.That(
                scene.CompositeFigures.ContainsKey("PRC"),
                Is.True,
                "The group was not created");

            commandLine = "copy PRC to PRJ";

            commandProducer.AppendLine(commandLine);

            command = commandProducer.GetCommand();
            command.Apply(scene);

            Assert.That(
                scene.CompositeFigures.ContainsKey("PRC") && scene.CompositeFigures.ContainsKey("PRJ"),
                Is.True,
                "The group was not copied");
        }

        [Test]
        public void Command_CanThrowBadFormatException()
        {
            CommandProducer commandProducer = new CommandProducer();
            Scene scene = new Scene();

            var commandLine = "some text";

            Assert.Throws<BadFormatException>(
                () => commandProducer.AppendLine(commandLine),
                "The exception was not thrown");
        }

        [Test]
        public void AddFigure_CanThrowBadCircleRadiusException()
        {
            CommandProducer commandProducer = new CommandProducer();
            Scene scene = new Scene();

            var commandLine = "add circle C (0, 0) radius 0";

            Assert.Throws<BadCircleRadiusException>(
                () => commandProducer.AppendLine(commandLine),
                "The exception was not thrown");
        }

        [Test]
        public void AddFigure_CanThrowBadPolygonPointNumberException()
        {
            CommandProducer commandProducer = new CommandProducer();
            Scene scene = new Scene();

            string commandLine1 = "add polygon P";
            string commandLine2 = "add point (-10, 0)";
            string commandLine3 = "add point (10, 0)";
            string commandLine4 = "end polygon";

            commandProducer.AppendLine(commandLine1);
            commandProducer.AppendLine(commandLine2);
            commandProducer.AppendLine(commandLine3);

            Assert.Throws<BadPolygonPointNumberException>(
                () => commandProducer.AppendLine(commandLine4),
                "The exception was not thrown");
        }

        [Test]
        public void AddFigure_CanThrowBadRectanglePointException()
        {
            CommandProducer commandProducer = new CommandProducer();
            Scene scene = new Scene();

            string commandLine = "add rectangle R (0, 0) (0, 0)";

            Assert.Throws<BadRectanglePointException>(
                () => commandProducer.AppendLine(commandLine),
                "The exception was not thrown");
        }

        [Test]
        public void AddFigure_CanThrowNameDoesAlreadyExistException()
        {
            CommandProducer commandProducer = new CommandProducer();
            Scene scene = new Scene();

            string commandLine = "add rectangle R (0, 0) (20, 20)";

            commandProducer.AppendLine(commandLine);

            var command = commandProducer.GetCommand();
            command.Apply(scene);

            commandLine = "add rectangle R (0, 0) (20, 20)";

            commandProducer.AppendLine(commandLine);

            command = commandProducer.GetCommand();

            Assert.Throws<NameDoesAlreadyExistException>(
                () => command.Apply(scene),
                "The exception was not thrown");
        }

        [Test]
        public void Command_CanThrowNameDoesNotExistException()
        {
            CommandProducer commandProducer = new CommandProducer();
            Scene scene = new Scene();

            string commandLine = "add rectangle R (0, 0) (20, 20)";

            commandProducer.AppendLine(commandLine);

            var command = commandProducer.GetCommand();
            command.Apply(scene);

            commandLine = "move C (0, 20)";

            commandProducer.AppendLine(commandLine);

            command = commandProducer.GetCommand();

            Assert.Throws<NameDoesNotExistException>(
                () => command.Apply(scene),
                "The exception was not thrown");
        }

        [Test]
        public void AddFigure_CanThrowUnexpectedEndOfPolygonException()
        {
            CommandProducer commandProducer = new CommandProducer();
            Scene scene = new Scene();

            string commandLine1 = "add polygon P";
            string commandLine2 = "add rectangle R (0, 0) (20, 20)";

            commandProducer.AppendLine(commandLine1);

            Assert.Throws<UnexpectedEndOfPolygonException>(
                () => commandProducer.AppendLine(commandLine2),
                "The exception was not thrown");
        }

        [Test]
        public void Figure_CanMove()
        {
            CommandProducer commandProducer = new CommandProducer();
            Scene scene = new Scene();

            var theFirstCoordinateAfterMoving = new ScenePoint(0, 10);
            var theSecondCoordinateAfterMoving = new ScenePoint(60, 50);

            string commandLine = "add rectangle R (-30, -20) (30, 20)";

            commandProducer.AppendLine(commandLine);

            var command = commandProducer.GetCommand();
            command.Apply(scene);

            commandLine = "move R (30, 30)";

            commandProducer.AppendLine(commandLine);

            command = commandProducer.GetCommand();
            command.Apply(scene);

            var figure = scene.Figures["R"];

            var theCircumscribingRectangle = figure.CalculateCircumscribingRectangle();

            var condition =
                theCircumscribingRectangle.Vertex1.X == theFirstCoordinateAfterMoving.X && theCircumscribingRectangle.Vertex1.Y == theFirstCoordinateAfterMoving.Y &&
                theCircumscribingRectangle.Vertex2.X == theSecondCoordinateAfterMoving.X && theCircumscribingRectangle.Vertex2.Y == theSecondCoordinateAfterMoving.Y;

            Assert.That(
                condition,
                Is.True,
                "The figure was not moved correctly");
        }

        [Test]
        public void CompositeFigure_CanMove()
        {
            CommandProducer commandProducer = new CommandProducer();
            Scene scene = new Scene();

            var theFirstCoordinateAfterMoving = new ScenePoint(100, 100);
            var theSecondCoordinateAfterMoving = new ScenePoint(300, 300);

            string commandLine = "add rectangle P (-30, -30) (30, 30)";

            commandProducer.AppendLine(commandLine);

            var command = commandProducer.GetCommand();
            command.Apply(scene);

            commandLine = "add circle C (0, 0) radius 100";

            commandProducer.AppendLine(commandLine);

            command = commandProducer.GetCommand();
            command.Apply(scene);

            commandLine = "group P, C as PC";

            commandProducer.AppendLine(commandLine);

            command = commandProducer.GetCommand();
            command.Apply(scene);

            commandLine = "move PC (200, 200)";

            commandProducer.AppendLine(commandLine);

            command = commandProducer.GetCommand();
            command.Apply(scene);

            var figure = scene.CompositeFigures["PC"];

            var theCircumscribingRectangle = figure.CalculateCircumscribingRectangle();

            var condition =
                theCircumscribingRectangle.Vertex1.X == theFirstCoordinateAfterMoving.X && theCircumscribingRectangle.Vertex1.Y == theFirstCoordinateAfterMoving.Y &&
                theCircumscribingRectangle.Vertex2.X == theSecondCoordinateAfterMoving.X && theCircumscribingRectangle.Vertex2.Y == theSecondCoordinateAfterMoving.Y;

            Assert.That(
                condition,
                Is.True,
                "The group was not moved correctly");
        }

        [Test]
        public void Rectangle_CanRotate()
        {
            CommandProducer commandProducer = new CommandProducer();
            Scene scene = new Scene();

            var theFirstCoordinateAfterRotating = new ScenePoint(-20, -30);
            var theSecondCoordinateAfterRotating = new ScenePoint(20, 30);

            string commandLine = "add rectangle R (-30, -20) (30, 20)";

            commandProducer.AppendLine(commandLine);

            var command = commandProducer.GetCommand();
            command.Apply(scene);

            commandLine = "rotate R 90";

            commandProducer.AppendLine(commandLine);

            command = commandProducer.GetCommand();
            command.Apply(scene);

            var figure = scene.Figures["R"];

            var theCircumscribingRectangle = figure.CalculateCircumscribingRectangle();

            Assert.That(theCircumscribingRectangle.Vertex1.X, Is.EqualTo(theFirstCoordinateAfterRotating.X).Within(0.0001));
            Assert.That(theCircumscribingRectangle.Vertex1.Y, Is.EqualTo(theFirstCoordinateAfterRotating.Y).Within(0.0001));
            Assert.That(theCircumscribingRectangle.Vertex2.X, Is.EqualTo(theSecondCoordinateAfterRotating.X).Within(0.0001));
            Assert.That(theCircumscribingRectangle.Vertex2.Y, Is.EqualTo(theSecondCoordinateAfterRotating.Y).Within(0.0001));
        }

        [Test]
        public void Polygon_CanRotate()
        {
            CommandProducer commandProducer = new CommandProducer();
            Scene scene = new Scene();

            var theFirstCoordinateAfterRotating = new ScenePoint(-11, -4);
            var theSecondCoordinateAfterRotating = new ScenePoint(31, 17);

            string commandLine1 = "add polygon P";
            string commandLine2 = "add point (0, 0)";
            string commandLine3 = "add point (30, 0)";
            string commandLine4 = "add point (0, 30)";
            string commandLine5 = "end polygon";

            commandProducer.AppendLine(commandLine1);
            commandProducer.AppendLine(commandLine2);
            commandProducer.AppendLine(commandLine3);
            commandProducer.AppendLine(commandLine4);
            commandProducer.AppendLine(commandLine5);

            var command = commandProducer.GetCommand();
            command.Apply(scene);

            string commandLine = "rotate P 45";

            commandProducer.AppendLine(commandLine);

            command = commandProducer.GetCommand();
            command.Apply(scene);

            var figure = scene.Figures["P"];

            var theCircumscribingRectangle = figure.CalculateCircumscribingRectangle();

            Assert.That(theCircumscribingRectangle.Vertex1.X, Is.EqualTo(theFirstCoordinateAfterRotating.X).Within(0.9));
            Assert.That(theCircumscribingRectangle.Vertex1.Y, Is.EqualTo(theFirstCoordinateAfterRotating.Y).Within(0.9));
            Assert.That(theCircumscribingRectangle.Vertex2.X, Is.EqualTo(theSecondCoordinateAfterRotating.X).Within(0.9));
            Assert.That(theCircumscribingRectangle.Vertex2.Y, Is.EqualTo(theSecondCoordinateAfterRotating.Y).Within(0.9));
        }

        [Test]
        public void Figure_CanReflect()
        {
            CommandProducer commandProducer = new CommandProducer();
            Scene scene = new Scene();

            var theFirstCoordinateAfterReflecting = new ScenePoint(0, 0);
            var theSecondCoordinateAfterReflecting = new ScenePoint(30, 30);

            string commandLine1 = "add polygon P";
            string commandLine2 = "add point (0, 0)";
            string commandLine3 = "add point (30, 0)";
            string commandLine4 = "add point (0, 30)";
            string commandLine5 = "end polygon";

            commandProducer.AppendLine(commandLine1);
            commandProducer.AppendLine(commandLine2);
            commandProducer.AppendLine(commandLine3);
            commandProducer.AppendLine(commandLine4);
            commandProducer.AppendLine(commandLine5);

            var command = commandProducer.GetCommand();
            command.Apply(scene);

            string commandLine = "reflect horizontally P 45";

            commandProducer.AppendLine(commandLine);

            command = commandProducer.GetCommand();
            command.Apply(scene);

            var figure = scene.Figures["P"];

            var theCircumscribingRectangle = figure.CalculateCircumscribingRectangle();

            var condition1 =
               theCircumscribingRectangle.Vertex1.X == theFirstCoordinateAfterReflecting.X && theCircumscribingRectangle.Vertex1.Y == theFirstCoordinateAfterReflecting.Y &&
               theCircumscribingRectangle.Vertex2.X == theSecondCoordinateAfterReflecting.X && theCircumscribingRectangle.Vertex2.Y == theSecondCoordinateAfterReflecting.Y;

            commandLine = "reflect horizontally P 45";

            commandProducer.AppendLine(commandLine);

            command = commandProducer.GetCommand();
            command.Apply(scene);

            var condition2 =
               theCircumscribingRectangle.Vertex1.X == theFirstCoordinateAfterReflecting.X && theCircumscribingRectangle.Vertex1.Y == theFirstCoordinateAfterReflecting.Y &&
               theCircumscribingRectangle.Vertex2.X == theSecondCoordinateAfterReflecting.X && theCircumscribingRectangle.Vertex2.Y == theSecondCoordinateAfterReflecting.Y;

            Assert.That(
                condition1 && condition2,
                Is.True,
                "The group was not reflected correctly");
        }
    }
}