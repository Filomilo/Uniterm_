using System.Windows;
using System.Windows.Media;
using Uniterm.Interfaces;
using Uniterm.Shapes;

namespace Uniterm.Models
{
    public class SequancingOpration : AbstractOperation
    {
        public SequancingOpration(
            object expressionA,
            object expressionB,
            string seprator,
            DirectionEnum direction
        )
            : base(expressionA, expressionB, seprator, direction) { }

        protected override Size GetOperatorSize(Point operatorStart, Point operataorEnd)
        {
            return Beizer.GetBeizerSize(operatorStart, operataorEnd);
        }

        protected override void DrawOpeartor(
            IDrawingCanvas drawingCanvas,
            DrawingContext dc,
            Point startPos,
            Point endPos
        )
        {
            drawingCanvas.DrawBezier(startPos, endPos, dc);
        }
    }
}
