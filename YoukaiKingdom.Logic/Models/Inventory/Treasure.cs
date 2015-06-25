namespace YoukaiKingdom.Logic.Models.Inventory
{
    using System.Collections.Generic;

    using YoukaiKingdom.Logic.Models.Characters;
    using YoukaiKingdom.Logic.Models.Items;

    public class Treasure
    {
        public Treasure(List<Item> items, Location location)
        {
            this.Items = new List<Item>(items);
            this.Location = location;
        }

        public List<Item> Items { get; set; }

        public Location Location { get; set; }
    }
}
