using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Uniterm.Interfaces;
using Uniterm.Shapes;

namespace Uniterm.Models
{
    public enum DirectionEnum
    {
        Horizontal,
        Vertical,
    }

    public abstract class AbstractOperation
    {
        private object _ExpressionA;
        private object _ExpressionB;
        private DirectionEnum _Direction;

        public object ExpressionA
        {
            get { return _ExpressionA; }
            set
            {
                ValidateOperation(value);
                _ExpressionA = value;
            }
        }

        public object ExpressionB
        {
            get { return _ExpressionB; }
            set
            {
                ValidateOperation(value);
                _ExpressionB = value;
            }
        }
        public string Separator { get; }

        public DirectionEnum Direction
        {
            get { return _Direction; }
        }

        public AbstractOperation(
            object expressionA,
            object expressionB,
            string seprator,
            DirectionEnum direction
        )
        {
            ValidateOperation(expressionA);
            ValidateOperation(expressionB);
            ValidateSeparator(seprator);
            _ExpressionA = expressionA;
            _ExpressionB = expressionB;
            Separator = seprator;
            _Direction = direction;
        }

        private static void ValidateOperation(object op)
        {
            if (!(op is AbstractOperation || op is string))
            {
                throw new ArgumentException("Invalid operation type");
            }

            if (op is string)
            {
                if ((op as string).Length > 250)
                {
                    throw new InvalidStringLengthException(
                        $"Wyrażenie [[{op}]] nie poinno przekraczać 250 znakó"
                    );
                }
            }
        }

        private static void ValidateSeparator(string sep)
        {
            if ((sep as string).Length > 250)
            {
                throw new InvalidStringLengthException(
                    $"seprator [[{sep}]] nie poinno przekraczać 250 znakó"
                );
            }
        }

        public Size GetSizeOnCavnas(IDrawingCanvas drawingCanvas)
        {
            switch (Direction)
            {
                case DirectionEnum.Horizontal:
                    return GetHorizontalSizeOnCavnas(drawingCanvas);
                    break;
                case DirectionEnum.Vertical:
                    return GetVerticalSizeOnCavnas(drawingCanvas);
                    break;
            }
            throw new NotImplementedException("Direction not implemented");
        }

        public void DrawAtPostion(IDrawingCanvas drawingCanvas, DrawingContext dc, Point position)
        {
            switch (Direction)
            {
                case DirectionEnum.Horizontal:
                    DrawHorizontalyAtPostion(drawingCanvas, dc, position);
                    break;
                case DirectionEnum.Vertical:
                    DrawVerticalyAtPostion(drawingCanvas, dc, position);
                    break;
            }
        }

        protected static Size GetExpressionSize(object expression, IDrawingCanvas drawingCanvas)
        {
            if (expression is string)
                return drawingCanvas.GetSizeOfText((string)expression);
            if (expression is AbstractOperation)
                return ((AbstractOperation)expression).GetSizeOnCavnas(drawingCanvas);
            throw new ArgumentException();
        }

        protected void DrawExpression(
            object expressionA,
            IDrawingCanvas drawingCanvas,
            DrawingContext dc,
            Point position
        )
        {
            if (expressionA is string)
            {
                drawingCanvas.DrawText(position, expressionA as string, dc);
                dc.DrawText(drawingCanvas.GetFormattedText(expressionA as string), position);
            }
            else if (expressionA is AbstractOperation)
            {
                ((AbstractOperation)expressionA).DrawAtPostion(drawingCanvas, dc, position);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        protected abstract Size GetOperatorSize(Point operatorStart, Point operataorEnd);

        protected void GetHorizontalSizeParamaterse(
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
                FirstEpressionSize.Height + GapSize
            );
            position = new Point(position.X + GapSize * 1.5, position.Y + GapSize * 1.5);
            Point CurveStartPostion = new Point(position.X + TotalSize.Width, position.Y);
            Point CurveEndPostion = position;
            Size CurveSize = GetOperatorSize(CurveStartPostion, CurveEndPostion); //Beizer.GetBeizerSize(CurveStartPostion, CurveEndPostion);
            OperatorSize = CurveSize;
            TotalSize = new Size(CurveSize.Width, TotalSize.Height + CurveSize.Height);
        }

        protected void GetVerticalSizeParamaterse(
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
                    + 3 * GapSize,
                FirstEpressionSize.Height
                    + SecondEpressionSize.Height
                    + SeperatorSize.Height
                    + 3 * GapSize
            );
            position = new Point(position.X + GapSize * 1.5, position.Y + GapSize * 1.5);
            Point CurveStartPostion = position;
            Point CurveEndPostion = new Point(position.X, position.Y + TotalSize.Height);
            Size CurveSize = GetOperatorSize(CurveStartPostion, CurveEndPostion); //
            OperatorSize = CurveSize;

            TotalSize = new Size(TotalSize.Width + CurveSize.Width, CurveSize.Height);
        }

        protected abstract void DrawOpeartor(
            IDrawingCanvas drawingCanvas,
            DrawingContext dc,
            Point startPos,
            Point endPos
        );

        public void DrawVerticalyAtPostion(
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
            DrawOpeartor(drawingCanvas, dc, CurveStartPostion, CurveEndPostion);
            //drawingCanvas.DrawBezier(CurveStartPostion, CurveEndPostion, dc);
            position = new Point(position.X + CurveSize.Width, position.Y + GapSize);

            this.DrawExpression(this.ExpressionA, drawingCanvas, dc, position);
            position.Y += 2 * GapSize;
            dc.DrawText(drawingCanvas.GetFormattedText(this.Separator), position);
            position.Y += 2 * GapSize;
            this.DrawExpression(this.ExpressionB, drawingCanvas, dc, position);
        }

        public void DrawHorizontalyAtPostion(
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
            DrawOpeartor(drawingCanvas, dc, CurveStartPostion, CurveEndPostion);
            position = new Point(position.X, position.Y + CurveSize.Height);

            this.DrawExpression(this.ExpressionA, drawingCanvas, dc, position);
            position.X += FirstEpressionSize.Width + GapSize;
            dc.DrawText(drawingCanvas.GetFormattedText(this.Separator), position);
            position.X += SeperatorSize.Width + GapSize;
            this.DrawExpression(this.ExpressionB, drawingCanvas, dc, position);
        }

        public Size GetHorizontalSizeOnCavnas(IDrawingCanvas drawingCanvas)
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

        public Size GetVerticalSizeOnCavnas(IDrawingCanvas drawingCanvas)
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

        public override string ToString()
        {
            return $"[{this.ExpressionA}] {this.Separator} [{this.ExpressionB}]";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (AbstractOperation)obj;

            return EqualsExpression(this.ExpressionA, other.ExpressionA)
                && EqualsExpression(this.ExpressionB, other.ExpressionB)
                && string.Equals(this.Separator, other.Separator)
                && this.Direction == other.Direction;
        }

        private bool EqualsExpression(object a, object b)
        {
            if (a == null && b == null)
                return true;
            if (a == null || b == null)
                return false;

            if (a is string strA && b is string strB)
                return strA == strB;

            if (a is AbstractOperation opA && b is AbstractOperation opB)
                return opA.Equals(opB);

            return false;
        }
    }
}
