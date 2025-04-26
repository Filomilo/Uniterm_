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
        public static AbstractOperation CreateOperation(OperationType type, object expressionA, object expressionB, string separator)
        {
            switch (type)
            {
                case OperationType.Parallel:
                    return new ParrarelOpartion(expressionA, expressionB, separator);
                case OperationType.Sequencing:
                    throw new NotImplementedException("Sequencing operation is not implemented yet.");
                default:
                    throw new ArgumentException("Invalid operation type");
            }
        }
    }
}
