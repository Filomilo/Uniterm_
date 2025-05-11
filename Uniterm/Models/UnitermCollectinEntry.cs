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

        public override bool Equals(object obj)
        {
            if (!(obj is UnitermCollectinEntry))
                return false;
            UnitermCollectinEntry other = (UnitermCollectinEntry)obj;
            return Name == other.Name
                && Description == other.Description
                && Collection.Equals(other.Collection);
        }
    }
}
