namespace YoukaiKingdom.Logic.Models.Inventory
{
    using System;
    using System.Collections.Generic;

    using YoukaiKingdom.Logic.Models.Items;
    using YoukaiKingdom.Logic.Models.Items.Weapons.OneHanded;
    using YoukaiKingdom.Logic.Models.Items.Weapons.TwoHanded;

    public class Loot
    {
        private int LastId;

        private List<Item> generatedItems;

        private List<Item> itemStore;

        private int currentLevel;

        private readonly Random random;

        private Loot(int level)
        {
            this.generatedItems = new List<Item>();
            this.currentLevel = level;
            this.random = new Random();
            this.GenerateStore();
        }

        private void GenerateStore()
        {

            this.itemStore.AddRange(new List<Item>()
                                       {
                                           new OneHandedSword(100, "Catana's will", 1, 2000, 70, false),
                                           new OneHandedSword(101, "Hell sword", 1, 1800, 150, false),
                                           new OneHandedSword(102, "Iron sword", 1, 1500, 60, false),
                                           new OneHandedDagger(103, "Dagger's might", 1, 1400, 70, false),
                                           new OneHandedDagger(104, "Rusted dagger", 1, 1400, 40, false),
                                           new OneHandedDagger(105, "Widow maker", 1, 1200, 60, false),
                                           new TwoHandedStaff(106, "Ormu's stick", 1, 3200, 130, false),
                                           new TwoHandedStaff(107, "Mighty staff", 1, 3200, 150, false),
                                           new TwoHandedStaff(108, "Mage's pride", 1, 3000, 140, false)
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
            int num = this.random.Next(0, 100);

            if (num <= 20)
            {
                return;
            }

            if (num > 20 && num <= 50)
            {
                this.Generate(1);
            }
            else if (num > 50 && num <= 70)
            {
                this.Generate(2);
            }
            else if (num > 70 && num <= 85)
            {
                this.Generate(3);
            }
            else if (num > 85 && num <= 95)
            {
                this.Generate(4);
            }
            else
            {
                this.Generate(5);
            }
        }

        private void Generate(int itemsCount)
        {
            for (int i = 0; i < itemsCount; i++)
            {

            }
        }
    }
}
