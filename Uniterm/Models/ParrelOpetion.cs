using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Uniterm.Interfaces;

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

        public override Size GetHorizontalSizeOnCavnas(IDrawingCanvas drawingCanvas)
        {
            throw new NotImplementedException();
        }

        public override Size GetVerticalSizeOnCavnas(IDrawingCanvas drawingCanvas)
        {
            throw new NotImplementedException();
        }

        public override void DrawVerticalyAtPostion(
            IDrawingCanvas drawingCanvas,
            DrawingContext dc,
            Point position
        )
        {
            throw new NotImplementedException();
        }

        public override void DrawHorizontalyAtPostion(
            IDrawingCanvas drawingCanvas,
            DrawingContext dc,
            Point position
        )
        {
            throw new NotImplementedException();
        }

        protected override void GetHorizontalSizeParamaterse(
            IDrawingCanvas drawingCanvas,
            out Size FirstEpressionSize,
            out Size SecondEpressionSize,
            out Size SeperatorSize,
            out int GapSize,
            out Size OperatorSize,
            out Size TotalSize
        )
        {
            throw new NotImplementedException();
        }

        protected override void GetVerticalSizeParamaterse(
            IDrawingCanvas drawingCanvas,
            out Size FirstEpressionSize,
            out Size SecondEpressionSize,
            out Size SeperatorSize,
            out int GapSize,
            out Size OperatorSize,
            out Size TotalSize
        )
        {
            throw new NotImplementedException();
        }
    }
}
