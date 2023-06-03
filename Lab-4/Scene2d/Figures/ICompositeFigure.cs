namespace Scene2d.Figures
{
    using System.Collections.Generic;
    using System.Drawing;

    /* This interface is not implemented yet */
    public interface ICompositeFigure : IFigure
    {
        IList<IFigure> ChildFigures { get; }
    }
}
