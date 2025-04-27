using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniterm.Models
{
    public class UnitermCollection
    {
        public List<AbstractOperation> VerticalOperations = new List<AbstractOperation>();
        public List<AbstractOperation> HorizontalOperations = new List<AbstractOperation>();

        public UnitermCollection Clone()
        {
            return new UnitermCollection()
            {
                VerticalOperations = new List<AbstractOperation>(VerticalOperations),
                HorizontalOperations = new List<AbstractOperation>(HorizontalOperations),
            };
        }
    }
}
