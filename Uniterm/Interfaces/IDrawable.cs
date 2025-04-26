using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Uniterm.Interfaces
{
    public interface IDrawable
    {
        void Draw(DrawingContext dc, IDrawingCanvas canvas);
    }
}
