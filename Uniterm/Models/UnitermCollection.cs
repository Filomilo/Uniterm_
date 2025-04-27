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

        public override string ToString()
        {
            string vertical = string.Join(
                ", ",
                VerticalOperations.Select(op => op?.ToString() ?? "null")
            );
            string horizontal = string.Join(
                ", ",
                HorizontalOperations.Select(op => op?.ToString() ?? "null")
            );

            return $"Vertical: [{vertical}]\nHorizontal: [{horizontal}]";
        }

        public override bool Equals(object obj)
        {
            if (!(obj is UnitermCollection))
                return false;
            UnitermCollection other = (UnitermCollection)obj;
            return Enumerable.SequenceEqual(VerticalOperations, other.VerticalOperations)
                && Enumerable.SequenceEqual(HorizontalOperations, other.HorizontalOperations);
        }
    }
}
