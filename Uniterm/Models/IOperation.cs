using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Uniterm.Models
{
    public abstract class AbstractOperation
    {
        private object _ExpressionA;
        private object _ExpressionB;


        object ExpressionA
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

        object ExpressionB
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
        string Separator { get; }

        public AbstractOperation(object expressionA, object expressionB, string seprator)
        {
            ValidateOperation(expressionA);
            ValidateOperation(expressionB);
            ValidateSeparator(seprator);
            _ExpressionA = expressionA;
            _ExpressionB = expressionB;
            Separator = seprator;
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

        public virtual Rect DrawHorizontaly(DrawingCanvas dc, Point bottomLeft)
        {
            throw new NotImplementedException();
        }

        public virtual Rect DrawVerticaly(DrawingCanvas dc, Point topRight)
        {
            throw new NotImplementedException();
        }
    }
}
