namespace YoukaiKingdom.Logic.Models.Inventory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using YoukaiKingdom.Logic.Interfaces;
    using YoukaiKingdom.Logic.Models.Characters;
    using YoukaiKingdom.Logic.Models.Items;
    using YoukaiKingdom.Logic.Models.Items.Armors;
    using YoukaiKingdom.Logic.Models.Items.Potions;
    using YoukaiKingdom.Logic.Models.Items.Weapons;
    using YoukaiKingdom.Logic.Models.Items.Weapons.OneHanded;
    using YoukaiKingdom.Logic.Models.Items.Weapons.TwoHanded;

    public class Loot
    {
        private int lastId;

        private List<Item> generatedItems;

        private List<Treasure> treasureChests;

        private List<Treasure> treasureBags;

        private List<Item> itemStore;

        private int currentLevel;

        private readonly Random random;

        private Loot(int level)
        {
            this.generatedItems = new List<Item>();
            this.treasureBags = new List<Treasure>();
            this.treasureChests = new List<Treasure>();
            this.currentLevel = level;
            this.random = new Random();
            this.itemStore = new List<Item>();
            this.GenerateStore();
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

        public IEnumerable<Treasure> TreasureChests
        {
            get
            {
                return this.treasureChests;
            }
        }

        public IEnumerable<Treasure> TreasureBags
        {
            get
            {
                return this.treasureBags;
            }
        }

        public bool HasLoot
        {
            get
            {
                return this.generatedItems.Count > 0;
            }
        }

        public Treasure Treasure { get; set; }

        public void GenerateTreasureChest(Location location)
        {
            this.GenerateChestItems();
            this.treasureChests.Add(new Treasure(this.generatedItems, location));
        }

        public void GenerateTreasureBag(Location location)
        {
            this.GenerateBagItems();
            if (this.HasLoot)
            {
                this.Treasure = new Treasure(this.generatedItems, location);
                this.treasureBags.Add(this.Treasure);
            }
        }
        //TODO
        private void GenerateStore()
        {
            this.lastId = 0;
            this.itemStore.AddRange(new List<Item>()
                                       {
                                           new OneHandedSword(10, "Wakizashi", 1, 70, 2000, false),
                                           new OneHandedSword(11, "Hell sword", 1, 150, 1800, false),
                                           new OneHandedSword(12, "Iron sword", 1, 60, 1500, false),
                                           new OneHandedDagger(13, "Dagger's might", 1, 70, 1400, false),
                                           new OneHandedDagger(14, "Rusted dagger", 1, 50, 1400, false),
                                           new OneHandedDagger(15, "Widow maker", 1, 160, 1200, false),
                                           new TwoHandedStaff(16, "Ormu's stick", 1, 50, 3200, false),
                                           new TwoHandedStaff(17, "Mighty staff", 1, 110, 3200, false),
                                           new TwoHandedStaff(18, "Mage's pride", 1, 70, 3000, false),
                                           new HealingPotion(50, "Minor healing potion", 1, 20),
                                           new HealingPotion(51, "Healing potion", 2, 50),
                                           new ManaPotion(52, "Minor mana potion", 1, 20),
                                           new ManaPotion(53, "Mana potion", 2, 50),
                                           new BodyArmor(54, "Iron armor", 1, 50, false),
                                           new BodyArmor(55, "Leather jacket", 1, 30, false),
                                           new BodyArmor(56, "Woolen robe", 1, 20, false),
                                           new Gloves(57, "Iron gloves", 1, 10, false),
                                           new Gloves(58, "Leather gloves", 1, 7, false),
                                           new Gloves(59, "Woolen gloves", 1, 5, false)
                                       });

            this.lastId = this.itemStore.LastOrDefault().Id;
        }

        private void GenerateChestItems()
        {
            this.generatedItems.Clear();

            int num = this.random.Next(0, 100);

            if (num <= 50)
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

        private void GenerateBagItems()
        {
            this.generatedItems.Clear();

            int num = this.random.Next(0, 100);

            if (num <= 65)
            {
                return;
            }

            if (num > 65 && num <= 80)
            {
                this.Generate(1);
            }
            else if (num > 80 && num <= 90)
            {
                this.Generate(2);
            }
            else if (num > 90 && num <= 95)
            {
                this.Generate(3);
            }
            else if (num > 95 && num <= 98)
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
                int num = this.random.Next(0, this.itemStore.Count);
                var item = this.itemStore[num];
                this.GenerateRandomItem(item);
            }
        }
        //TODO
        private void GenerateRandomItem(Item baseItem)
        {
            if (baseItem is OneHandedSword)
            {
                OneHandedSword sword = baseItem as OneHandedSword;
                this.generatedItems.Add(new OneHandedSword(++this.lastId, sword.Name, this.currentLevel, sword.AttackPoints, sword.AttackSpeed));
            }
            else if (baseItem is OneHandedDagger)
            {
                OneHandedDagger dagger = baseItem as OneHandedDagger;
                this.generatedItems.Add(new OneHandedDagger(++this.lastId, dagger.Name, this.currentLevel, dagger.AttackPoints, dagger.AttackSpeed));
            }
            else if (baseItem is TwoHandedStaff)
            {
                TwoHandedStaff staff = baseItem as TwoHandedStaff;
                this.generatedItems.Add(new TwoHandedStaff(++this.lastId, staff.Name, this.currentLevel, staff.AttackPoints, staff.AttackSpeed));
            }
            else if (baseItem is HealingPotion)
            {
                HealingPotion potion = baseItem as HealingPotion;
                this.generatedItems.Add(new HealingPotion(++this.lastId, potion.Name, this.currentLevel, potion.HealingPoints));
            }
            else if (baseItem is ManaPotion)
            {
                ManaPotion potion = baseItem as ManaPotion;
                this.generatedItems.Add(new ManaPotion(++this.lastId, potion.Name, this.currentLevel, potion.ManaPoints));
            }
            else if (baseItem is Gloves)
            {
                Gloves gloves = baseItem as Gloves;
                this.generatedItems.Add(new Gloves(++this.lastId, gloves.Name, this.currentLevel, gloves.DefensePoints));
            }
            else if (baseItem is BodyArmor)
            {
                BodyArmor bodyArmor = baseItem as BodyArmor;
                this.generatedItems.Add(new BodyArmor(++this.lastId, bodyArmor.Name, this.currentLevel, bodyArmor.DefensePoints));
            }
        }

        public HealingPotion GetMinorHealingPotion()
        {
            var item = this.itemStore.FirstOrDefault(x => x.Id == 50);
            if (item == null)
            {
                throw new ArgumentNullException("The minor healing potion is missing!");
            }

            var heal = (HealingPotion)item;

            return new HealingPotion(++this.lastId, heal.Name, heal.Level, heal.HealingPoints);
        }

        public HealingPotion GetHealingPotion()
        {
            var item = this.itemStore.FirstOrDefault(x => x.Id == 51);
            if (item == null)
            {
                throw new ArgumentNullException("The healing potion is missing!");
            }

            var heal = (HealingPotion)item;

            return new HealingPotion(++this.lastId, heal.Name, heal.Level, heal.HealingPoints);
        }

        public ManaPotion GetMinorManaPotion()
        {
            var item = this.itemStore.FirstOrDefault(x => x.Id == 52);
            if (item == null)
            {
                throw new ArgumentNullException("The minor mana potion is missing!");
            }

            var mana = (ManaPotion)item;

            return new ManaPotion(++this.lastId, mana.Name, mana.Level, mana.ManaPoints);
        }

        public ManaPotion GetManaPotion()
        {
            var item = this.itemStore.FirstOrDefault(x => x.Id == 53);
            if (item == null)
            {
                throw new ArgumentNullException("The mana potion is missing!");
            }

            var mana = (ManaPotion)item;

            return new ManaPotion(++this.lastId, mana.Name, mana.Level, mana.ManaPoints);
        }

        public OneHandedDagger GetDaggerWeaponById(int weaponId, bool hasAtrributes = false)
        {
            var item = this.itemStore.FirstOrDefault(x => x.Id == weaponId && x is OneHandedDagger);

            if (item == null)
            {
                throw new ArgumentNullException("The dagger is missing!");
            }

            var weapon = (OneHandedDagger)item;

            return new OneHandedDagger(++this.lastId, weapon.Name, weapon.Level, weapon.AttackPoints, weapon.AttackSpeed, hasAtrributes);
        }

        public OneHandedSword GetOneHandedSwordById(int weaponId, bool hasAtrributes = false)
        {
            var item = this.itemStore.FirstOrDefault(x => x.Id == weaponId && x is OneHandedSword);

            if (item == null)
            {
                throw new ArgumentNullException("The sword is missing!");
            }

            var weapon = (OneHandedSword)item;

            return new OneHandedSword(++this.lastId, weapon.Name, weapon.Level, weapon.AttackPoints, weapon.AttackSpeed, hasAtrributes);
        }

        public TwoHandedStaff GetTwoHandedStaffById(int weaponId, bool hasAtrributes = false)
        {
            var item = this.itemStore.FirstOrDefault(x => x.Id == weaponId && x is TwoHandedStaff);

            if (item == null)
            {
                throw new ArgumentNullException("The staff is missing!");
            }

            var weapon = (TwoHandedStaff)item;

            return new TwoHandedStaff(++this.lastId, weapon.Name, weapon.Level, weapon.AttackPoints, weapon.AttackSpeed, hasAtrributes);
        }

        public BodyArmor GetBodyArmorById(int armorId, bool hasAtrributes = false)
        {
            var item = this.itemStore.FirstOrDefault(x => x.Id == armorId && x is BodyArmor);

            if (item == null)
            {
                throw new ArgumentNullException("The BodyArmor is missing!");
            }

            var armor = (BodyArmor)item;

            return new BodyArmor(++this.lastId, armor.Name, armor.Level, armor.DefensePoints, hasAtrributes);
        }

        public Gloves GetGlovesById(int armorId, bool hasAtrributes = false)
        {
            var item = this.itemStore.FirstOrDefault(x => x.Id == armorId && x is Gloves);

            if (item == null)
            {
                throw new ArgumentNullException("The Gloves are missing!");
            }

            var armor = (Gloves)item;

            return new Gloves(++this.lastId, armor.Name, armor.Level, armor.DefensePoints, hasAtrributes);
        }
    }
}
