using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Uniterm.Interfaces;
using Uniterm.Shapes;

namespace Uniterm.Models
{
    public class ParrarelOpartion : AbstractOperation
    {
        public ParrarelOpartion(
            object expressionA,
            object expressionB,
            string seprator,
            DirectionEnum direction
        )
            : base(expressionA, expressionB, seprator, direction) { }

        protected override void DrawOpeartor(
            IDrawingCanvas drawingCanvas,
            DrawingContext dc,
            Point startPos,
            Point endPos
        )
        {
            drawingCanvas.DrawRectBrackets(startPos, endPos, dc);
        }

        protected override Size GetOperatorSize(Point operatorStart, Point operataorEnd)
        {
            return RectangularBrackets.GetSize(operatorStart, operataorEnd);
        }
    }
}
