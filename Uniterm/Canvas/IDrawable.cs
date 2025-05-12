using System.Windows.Media;

namespace Uniterm.Canvas
{
    public interface IDrawable
    {
        void Draw(DrawingContext dc, IDrawingCanvas canvas);
    }
}
