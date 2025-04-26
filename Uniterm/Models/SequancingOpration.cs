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
    public class SequancingOpration : AbstractOperation
    {
        public SequancingOpration(
            object expressionA,
            object expressionB,
            string seprator,
            DirectionEnum direction
        )
            : base(expressionA, expressionB, seprator, direction) { }

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
            Point position = new Point(0, 0);
            FirstEpressionSize = GetExpressionSize(this.ExpressionA, drawingCanvas);
            SecondEpressionSize = GetExpressionSize(this.ExpressionB, drawingCanvas);
            SeperatorSize = drawingCanvas.GetSizeOfText(this.Separator);
            GapSize = drawingCanvas.GetFontSize();
            TotalSize = new Size(
                FirstEpressionSize.Width
                    + SecondEpressionSize.Width
                    + SeperatorSize.Width
                    + 2 * GapSize,
                FirstEpressionSize.Height
                    + SecondEpressionSize.Height
                    + SeperatorSize.Height
                    + 2 * GapSize
            );
            position = new Point(position.X + GapSize * 1.5, position.Y + GapSize * 1.5);
            Point CurveStartPostion = new Point(position.X + TotalSize.Width, position.Y);
            Point CurveEndPostion = position;
            Size CurveSize = Beizer.GetBeizerSize(CurveStartPostion, CurveEndPostion);
            OperatorSize = CurveSize;
            TotalSize = new Size(
                TotalSize.Width + CurveSize.Width,
                TotalSize.Height + CurveSize.Height
            );
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
            Point position = new Point(0, 0);
            FirstEpressionSize = GetExpressionSize(this.ExpressionA, drawingCanvas);
            SecondEpressionSize = GetExpressionSize(this.ExpressionB, drawingCanvas);
            SeperatorSize = drawingCanvas.GetSizeOfText(this.Separator);
            GapSize = drawingCanvas.GetFontSize();
            TotalSize = new Size(
                FirstEpressionSize.Width
                    + SecondEpressionSize.Width
                    + SeperatorSize.Width
                    + 2 * GapSize,
                FirstEpressionSize.Height
                    + SecondEpressionSize.Height
                    + SeperatorSize.Height
                    + 2 * GapSize
            );
            position = new Point(position.X + GapSize * 1.5, position.Y + GapSize * 1.5);
            Point CurveStartPostion = position;
            Point CurveEndPostion = new Point(position.X, position.Y + TotalSize.Height);
            Size CurveSize = Beizer.GetBeizerSize(CurveStartPostion, CurveEndPostion);
            OperatorSize = CurveSize;

            TotalSize = new Size(
                TotalSize.Width + CurveSize.Width,
                TotalSize.Height + CurveSize.Height
            );
        }

        public override Size GetHorizontalSizeOnCavnas(IDrawingCanvas drawingCanvas)
        {
            GetHorizontalSizeParamaterse(
                drawingCanvas,
                out Size FirstEpressionSize,
                out Size SecondEpressionSize,
                out Size SeperatorSize,
                out int GapSize,
                out Size CurveSize,
                out Size TotalSize
            );

            return TotalSize;
        }

        public override Size GetVerticalSizeOnCavnas(IDrawingCanvas drawingCanvas)
        {
            GetVerticalSizeParamaterse(
                drawingCanvas,
                out Size FirstEpressionSize,
                out Size SecondEpressionSize,
                out Size SeperatorSize,
                out int GapSize,
                out Size CurveSize,
                out Size TotalSize
            );

            return TotalSize;
        }

        public override void DrawVerticalyAtPostion(
            IDrawingCanvas drawingCanvas,
            DrawingContext dc,
            Point position
        )
        {
            GetVerticalSizeParamaterse(
                drawingCanvas,
                out Size FirstEpressionSize,
                out Size SecondEpressionSize,
                out Size SeperatorSize,
                out int GapSize,
                out Size CurveSize,
                out Size TotalSize
            );

            Point CurveStartPostion = new Point(position.X + CurveSize.Width, position.Y);
            Point CurveEndPostion = new Point(
                position.X + CurveSize.Width,
                position.Y + CurveSize.Height
            );
            drawingCanvas.DrawBezier(CurveStartPostion, CurveEndPostion, dc);
            position = new Point(position.X + CurveSize.Width, position.Y);

            this.DrawExpression(this.ExpressionA, drawingCanvas, dc, position);
            position.Y += 2 * GapSize;
            dc.DrawText(drawingCanvas.GetFormattedText(this.Separator), position);
            position.Y += 2 * GapSize;
            this.DrawExpression(this.ExpressionB, drawingCanvas, dc, position);
        }

        public override void DrawHorizontalyAtPostion(
            IDrawingCanvas drawingCanvas,
            DrawingContext dc,
            Point position
        )
        {
            GetHorizontalSizeParamaterse(
                drawingCanvas,
                out Size FirstEpressionSize,
                out Size SecondEpressionSize,
                out Size SeperatorSize,
                out int GapSize,
                out Size CurveSize,
                out Size TotalSize
            );

            Point CurveEndPostion = new Point(position.X, position.Y + CurveSize.Height);
            Point CurveStartPostion = new Point(
                position.X + CurveSize.Width,
                position.Y + CurveSize.Height
            );
            drawingCanvas.DrawBezier(CurveStartPostion, CurveEndPostion, dc);
            position = new Point(position.X, position.Y + CurveSize.Height);

            this.DrawExpression(this.ExpressionA, drawingCanvas, dc, position);
            position.X += FirstEpressionSize.Width + GapSize;
            dc.DrawText(drawingCanvas.GetFormattedText(this.Separator), position);
            position.X += SeperatorSize.Width + GapSize;
            this.DrawExpression(this.ExpressionB, drawingCanvas, dc, position);
        }
    }
}
