using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Uniterm.Interfaces;

namespace Uniterm.Models
{
    public enum DirectionEnum
    {
        Horizontal, Vertical
    }

    public abstract class AbstractOperation
    {
        private object _ExpressionA;
        private object _ExpressionB;
        private DirectionEnum _Direction;

        public object ExpressionA
        {
            get
            {
                return _ExpressionA;
            }
            set
            {
                ValidateOperation(value);
                _ExpressionA = value;
            }
        }

        public object ExpressionB
        {
            get
            {
                return _ExpressionB;
            }
            set
            {
                ValidateOperation(value);
                _ExpressionB = value;
            }
        }
        public string Separator { get; }

        DirectionEnum Direction
        {
            get
            {
                return _Direction;
            }
        }

        public AbstractOperation(object expressionA, object expressionB, string seprator, DirectionEnum direction)
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
                    throw new InvalidStringLengthException($"Wyrażenie [[{op}]] nie poinno przekraczać 250 znakó");
                }
            }
        }
        private static void ValidateSeparator(string sep)
        {
            if ((sep as string).Length > 250)
            {
                throw new InvalidStringLengthException($"seprator [[{sep}]] nie poinno przekraczać 250 znakó");
            }
        }

        public abstract Size GetHorizontalSizeOnCavnas(IDrawingCanvas drawingCanvas);
        public abstract Size GetVerticalSizeOnCavnas(IDrawingCanvas drawingCanvas);
        public abstract void DrawVerticalyAtPostion(IDrawingCanvas drawingCanvas, DrawingContext dc, Point position);
        public abstract void DrawHorizontalyAtPostion(IDrawingCanvas drawingCanvas, DrawingContext dc, Point position);

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



        protected void DrawExpression(object expressionA, IDrawingCanvas drawingCanvas, DrawingContext dc, Point position)
        {
           if(expressionA is string)
           {
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

        abstract protected void GetHorizontalSizeParamaterse(IDrawingCanvas drawingCanvas
            , out Size FirstEpressionSize
            , out Size SecondEpressionSize
            , out Size SeperatorSize
            , out int GapSize
            , out Size OperatorSize
            , out Size TotalSize

        );
        abstract protected void GetVerticalSizeParamaterse(IDrawingCanvas drawingCanvas
            , out Size FirstEpressionSize
            , out Size SecondEpressionSize
            , out Size SeperatorSize
            , out int GapSize
            , out Size OperatorSize
            , out Size TotalSize

        );




    }
}
