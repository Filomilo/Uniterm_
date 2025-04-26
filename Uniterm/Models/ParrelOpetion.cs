using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniterm.Models
{
    public class ParrarelOpartion : AbstractOperation
    {
        public ParrarelOpartion(object expressionA, object expressionB, string seprator) : base(expressionA, expressionB, seprator)
        {
        }
    }
}
