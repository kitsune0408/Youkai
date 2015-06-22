﻿namespace YoukaiKingdom.Logic
{
    using System.Collections.Generic;

    using YoukaiKingdom.Logic.Models.Characters;
    using YoukaiKingdom.Logic.Models.Characters.Heroes;
    using YoukaiKingdom.Logic.Models.Characters.NPCs;
    using YoukaiKingdom.Logic.Models.Items.Armors;
    using YoukaiKingdom.Logic.Models.Items.Potions;
    using YoukaiKingdom.Logic.Models.Items.Weapons;

    public class GameEngine
    {
        private List<Npc> bosses;

        public GameEngine(Hero heroClass)
        {
            this.Hero = heroClass;
            this.LoadDefaultInfo();
            this.Enemies = new List<Npc>();
            this.LoadBosses();
            this.CurrentLevel = 1;
        }

        public Hero Hero { get; set; }

        public void Start()
        {
            this.LoadEnemiesByLevel(this.CurrentLevel);
        }

        public List<Npc> Enemies { get; set; }

        public int CurrentLevel { get; set; }

        public void LoadEnemiesByLevel(int level)
        {
            this.Enemies.AddRange(new List<Npc>()
            {
                new NpcMage(level, "Evil Mage", new Location(1200, 300)),
                new NpcMage(level, "Evil Mage", new Location(100,100)),
                new NpcMage(level, "Evil Mage", new Location(100,100)),
                new NpcMage(level, "Evil Mage", new Location(100,100)),
                new NpcMage(level, "Evil Mage", new Location(100,100)),
                new NpcRogue(level, "Evil Rogue", new Location(100,100)),
                new NpcRogue(level, "Evil Rogue", new Location(100,100)),
                new NpcRogue(level, "Evil Rogue", new Location(100,100)),
                new NpcRogue(level, "Evil Rogue", new Location(100,100)),
                new NpcWarrior(level, "Evil Warrior", new Location(1200, 800)),
                new NpcWarrior(level, "Evil Warrior", new Location(100,100)),
                new NpcWarrior(level, "Evil Warrior", new Location(100,100)),
                new NpcWarrior(level, "Evil Warrior", new Location(100,100)),
                new NpcWarrior(level, "Evil Warrior", new Location(100,100)),
                new NpcWarrior(level, "Evil Warrior", new Location(100,100)),
                this.bosses[level-1]
            });
        }

        public void LoadBosses()
        {
            this.bosses = new List<Npc>();

            this.bosses.AddRange(new List<Npc>()
                                {
                                    new NpcWarrior(1, "Onu", 600, 100, 250, 200, new Location(100,100)),
                                    new NpcMage(2, "Ogre", 800, 600, 250, 300, new Location(100,100)),
                                    new NpcRogue(3, "Ghost", 850, 100, 300, 300, new Location(100,100))
                                });
        }

        private void LoadDefaultInfo()
        {
            if (this.Hero is Samurai)
            {
                this.Hero.ReplaceMainHand(new OneHandedSword(1, "Iron sword", false));
                this.Hero.ReplaceBodyArmor(new BodyArmor(2, "Iron armor", false));
                this.Hero.Inventory.AddItemToBag(new HealingPotion(3, "Healing potion", 1, 50));
                this.Hero.Inventory.AddItemToBag(new Gloves(4, "Iron gloves"));
                this.Hero.Inventory.AddItemToBag(new HealingPotion(5, "Healing potion", 1, 50));
                this.Hero.Inventory.AddItemToBag(new HealingPotion(6, "Minor healing potion", 1, 100));
                this.Hero.Inventory.AddItemToBag(new HealingPotion(7, "Minor healing potion", 1, 100));
                this.Hero.Inventory.AddItemToBag(new Gloves(8, "Steel gloves", 1, 15, false));
                this.Hero.Inventory.AddItemToBag(new ManaPotion(9, "Mana potion", 1, 50));
            }
            else if (this.Hero is Monk)
            {
                this.Hero.ReplaceMainHand(new TwoHandedStaff(1, "Staff", false));
                this.Hero.ReplaceBodyArmor(new BodyArmor(2, "Woolen robe", false));
                this.Hero.Inventory.AddItemToBag(new HealingPotion(3, "Healing potion", 1, 50));
                this.Hero.Inventory.AddItemToBag(new Gloves(4, "Woolen gloves"));
                this.Hero.Inventory.AddItemToBag(new HealingPotion(5, "Healing potion", 1, 50));
                this.Hero.Inventory.AddItemToBag(new HealingPotion(6, "Minor healing potion", 1, 100));
                this.Hero.Inventory.AddItemToBag(new HealingPotion(7, "Minor healing potion", 1, 100));
                this.Hero.Inventory.AddItemToBag(new Gloves(8, "Woolen gloves", 1, 15, false));
                this.Hero.Inventory.AddItemToBag(new ManaPotion(9, "Mana potion", 1, 50));
            }
            else if (this.Hero is Ninja)
            {
                this.Hero.ReplaceMainHand(new OneHandedDagger(1, "Rusted Dagger", false));
                this.Hero.ReplaceBodyArmor(new BodyArmor(2, "Leather jacket", false));
                this.Hero.Inventory.AddItemToBag(new HealingPotion(3, "Healing potion", 1, 50));
                this.Hero.Inventory.AddItemToBag(new Gloves(4, "Leather gloves"));
                this.Hero.Inventory.AddItemToBag(new HealingPotion(5, "Healing potion", 1, 50));
                this.Hero.Inventory.AddItemToBag(new HealingPotion(6, "Minor healing potion", 1, 100));
                this.Hero.Inventory.AddItemToBag(new HealingPotion(7, "Minor healing potion", 1, 100));
                this.Hero.Inventory.AddItemToBag(new Gloves(8, "Leather gloves", 1, 15, false));
                this.Hero.Inventory.AddItemToBag(new ManaPotion(9, "Mana potion", 1, 50));
            }
        }
    }
}
