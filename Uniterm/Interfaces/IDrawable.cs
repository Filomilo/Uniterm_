using System.Windows.Media;

namespace Uniterm.Interfaces
{
    public interface IDrawable
    {
        void Draw(DrawingContext dc, IDrawingCanvas canvas);
    }
}
