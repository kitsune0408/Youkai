namespace YoukaiKingdom.Logic.Models.Inventory
{
    using System;
    using System.Collections.Generic;

    using YoukaiKingdom.Logic.Models.Items;
    using YoukaiKingdom.Logic.Models.Items.Weapons.OneHanded;

    public class Loot
    {
        private const int DefaultMaxDroppedItemsCount = 5;

        private List<Item> generatedItems;

        private List<Item> itemStore;

        private int currentLevel;

        private Loot(int level)
        {
            this.generatedItems = new List<Item>();
            this.currentLevel = level;
            this.GenerateStore();
        }

        private void GenerateStore()
        {
            
            this.itemStore.AddRange(new List<Item>()
                                       {
                                           new OneHandedSword(100, "Catana's will", this.currentLevel, 2000, 70),
                                           new OneHandedSword(101, "Hell sword", this.currentLevel, 1800, 150),
                                           new OneHandedSword(102, "Iron sword", this.currentLevel, 1500, 60),
                                           new OneHandedDagger(103, "Dagger's might", this.currentLevel, 1400, 70),
                                           new OneHandedDagger(104, "Rusted dagger", this.currentLevel, 1400, 40),
                                           new OneHandedDagger(105, "Widow maker", this.currentLevel, 1200, 60)
                                       });

        }

        public static Loot Create(int level)
        {
            return new Loot(level);
        }

        public IEnumerable<Item> GeneratedItems
        {
            get
            {
                return this.generatedItems;
            }
        }

        public void GenerateItems()
        {
            Random rn = new Random();
            int num = rn.Next(0, 100);

            if (num <= 20)
            {
                return;
            }
            else if (num > 20 && num <= 50)
            {

            }
        }
    }
}
