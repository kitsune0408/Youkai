namespace YoukaiKingdom.Logic.Models.Inventory
{
    using System.Collections.Generic;

    using YoukaiKingdom.Logic.Models.Characters;
    using YoukaiKingdom.Logic.Models.Items;

    public class TreasureChest
    {
        public TreasureChest(List<Item> items, Location location)
        {
            this.Items = items;
            this.Location = location;
        }

        public List<Item> Items { get; set; }

        public Location Location { get; set; }
    }
}
