namespace MagazineApp
{
    // Represents an item in a magazine
    public class Item
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public int GoofyLevel { get; set; } // Removed the extra semicolon
        public bool IsExplotano { get; set; } // Removed the extra semicolon

        public Item(string name, double Weight, int GoofyLevel, bool IsExplotano)
        {
            Name = name;
            this.Weight = Weight;
            this.GoofyLevel = GoofyLevel;
            this.IsExplotano = IsExplotano;
        }
    }
}
