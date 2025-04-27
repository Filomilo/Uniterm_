using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniterm.Models
{
    public class UnitermCollectinEntry
    {
        public UnitermCollectinEntry() { }

        public UnitermCollectinEntry(
            string title,
            string description,
            UnitermCollection unitermCollection
        )
        {
            this.Name = title;
            this.Description = description;
            this.Collection = unitermCollection;
        }

        public UnitermCollection Collection { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{Name} -- {Description}";
        }
    }
}
