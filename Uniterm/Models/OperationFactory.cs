using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniterm.Models
{
   public enum OperationType
    {
        Parallel, Sequencing
    }
    public class OperationFactory
    {
        public static AbstractOperation CreateOperation(OperationType type, object expressionA, object expressionB, string separator, DirectionEnum direction)
        {
            switch (type)
            {
                case OperationType.Parallel:
                    return new ParrarelOpartion(expressionA, expressionB, separator, direction);
                case OperationType.Sequencing:
                    return new SequancingOpration(expressionA, expressionB, separator, direction);
                default:
                    throw new ArgumentException("Invalid operation type");
            }
        }
    }
}
